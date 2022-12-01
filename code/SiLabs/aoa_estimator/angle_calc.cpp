#include "angle_calc.h"
#include <iostream>
#include <cmath>
#define eigen_assert(X) do { if(!(X)) throw std::runtime_error(#X); } while(false);
#include <eigen3/Eigen/Eigen>
#include <random>
#include <complex.h>
#include <vector>

#define DISTANCE_RELATIVE   0.3202215933

#define IQ_GET_PHASE(i, q) atan2(-(q), (i)) //does not need to be divided by 127
#define GET_PHASE_SHIFT(phase1, phase2) atan2(sin((phase2)-(phase1)), cos((phase2)-(phase1)))

double get_phase(int i, int q) {
    return atan2(q, i);
}
double get_amplitude(int i, int q) {
    return sqrt(pow(q,2) + pow(i,2));
}

double calculate_phase_shift(int I1, int Q1, int I2, int Q2) {
    double ph1, ph2;
    ph1 = IQ_GET_PHASE(I1, Q1);
    ph2 = IQ_GET_PHASE(I2, Q2);
    double diff = GET_PHASE_SHIFT(IQ_GET_PHASE(I1, Q1), IQ_GET_PHASE(I2, Q2));
    return diff;
}

double calculate_ref_phase_shift(int *ref_samples, int len) {
    double phase = 0;

    for (int i=0; i<len-3; i+=2) {
        phase += fabs(calculate_phase_shift(ref_samples[i], ref_samples[i+1], ref_samples[i+2], ref_samples[i+3]));
    }
    return 2*(phase / (len/2 - 1));
}

template<typename Scalar> struct MakeComplexOp {
  EIGEN_EMPTY_STRUCT_CTOR(MakeComplexOp)
  typedef std::complex<Scalar> result_type;
  std::complex<Scalar> operator()(const Scalar& a, const Scalar& b) const { return std::complex<Scalar>(a,b); }
};

Eigen::VectorXcd iq_samples_array_to_complex_vector(double *iq_samples, int len, int antennas_n) {
    Eigen::Map<Eigen::VectorXd, 0, Eigen::Stride<1, 2>> reals(iq_samples, len/2);
    Eigen::Map<Eigen::VectorXd, 0, Eigen::Stride<1, 2>> im(iq_samples+1, len/2);
    Eigen::VectorXcd res = reals.binaryExpr(im, MakeComplexOp<double>());

    return (Eigen::VectorXcd(res.size() + antennas_n-((res.size())%antennas_n)) << res, Eigen::VectorXcd::Zero(antennas_n-((res.size())%antennas_n))).finished();
}

Eigen::MatrixXcd get_signal_matrix_from_antennas(Eigen::MatrixXcd antennas, std::vector<int> rows) {
    Eigen::MatrixXcd signal(rows.size(), antennas.cols());

    int out_col_n = antennas.cols();

    for(int i=0; i<rows.size(); ++i) {
        signal.row(i) = antennas.row(rows[i]);
    }

    while(out_col_n > 1 && (signal.col(out_col_n-1).array() == Eigen::dcomplex(0, 0)).any()){
        --out_col_n;
    }

    //std::cout << out_col_n;
    return signal.block(0, 0, rows.size(), out_col_n);
}

Eigen::VectorXcd gen_steering_vector(Eigen::VectorXd xd, double angle_in_rad) {
    return (Eigen::dcomplex(0,1)*(2*M_PI*DISTANCE_RELATIVE*(xd*std::sin(angle_in_rad)))).transpose().array().exp();
}

Eigen::MatrixXcd spatial_smoothing(Eigen::MatrixXcd R, int l, bool fb=false) {
    int m = R.rows();
    if(l < 1 || l > m)
        throw new std::runtime_error("l must be between 1 and m");
    Eigen::MatrixXcd Rf = R.block(0, 0, m-l+1, m-l+1);

    for(int i=1; i<l; ++i) {
        Rf += R.block(i, i, m-l+1, m-l+1);
    }
    Rf /= l;

    if(fb) {
        Rf = 0.5 * (Rf + Rf.reverse().conjugate());
    }

    return Rf;
}

double music(Eigen::MatrixXcd antennas, std::vector<int> rows, int sub_matrix_M, int sub_matrix_N, int scanning_precision) {
    Eigen::MatrixXcd signal = get_signal_matrix_from_antennas(antennas, rows);
    Eigen::MatrixXcd conj_sig = signal.conjugate();

    int rows_n = signal.rows(); //actually the number of antennas
    int cols_n = signal.cols(); //number of samples for each antenna

    if(cols_n == 0) return 0;

    Eigen::MatrixXcd exch_m = Eigen::MatrixXcd::Identity(rows_n, rows_n).rowwise().reverse();

    Eigen::MatrixXcd Rxx = spatial_smoothing((signal * signal.adjoint()) + (exch_m * conj_sig * conj_sig.adjoint() * exch_m), 1, true);

    Eigen::ComplexEigenSolver<Eigen::MatrixXcd> esolver;
    esolver.compute(Rxx, true);

    Eigen::MatrixXcd E = esolver.eigenvectors().topLeftCorner(Rxx.rows(), Rxx.rows()-1);

    Eigen::VectorXd xd = Eigen::VectorXd::LinSpaced(sub_matrix_N, 0, (sub_matrix_N-1));
    
    double maxAngleRad = M_PI/3;
    double maxAngleDeg = maxAngleRad*180/M_PI;

    Eigen::VectorXd i_angles = Eigen::VectorXd::LinSpaced(2*maxAngleDeg*scanning_precision+1, -maxAngleRad, maxAngleRad);
    double max = -1;
    int maxI = -1;

    for(int i=0; i<i_angles.size(); ++i) {
        Eigen::VectorXcd S_theta = gen_steering_vector(xd, i_angles(i)); //this supposes that antennas are evenly spaced
            
        double res = 1/((S_theta.adjoint() * E * E.adjoint() * S_theta)(0, 0)).real();
        if(res > max) {
            maxI = i;
            max = res;
        }
    }

    return (double)maxI/scanning_precision - maxAngleDeg;
}

double calc_angle(double *samples, int len, int M, int N) {
    int antennas_n = M*N;

    Eigen::VectorXcd complexes = iq_samples_array_to_complex_vector(samples, len, antennas_n);
    
    Eigen::MatrixXcd antennas;
    antennas = complexes.reshaped(antennas_n, Eigen::AutoSize);

    double acc = 0;
    std::vector<int> rows(N);

    for(int i=0; i<antennas.rows()-M+1; i+=N) {
        for(int j=0; j<N; ++j)
            rows[j] = (i+j);

        acc += music(antennas, rows, 1, 4, 1);
    }

    acc /= antennas.rows() / N;

    return acc;
}

double calc_distance(int rssi, double one_meter_rssi) {
    return std::pow(10, (one_meter_rssi - rssi)/20);
}

void transform_data(int *samples, int len, double ref_phase_shift, double *out) {
    for(uint8_t k=0; k<len-1; k+=2) {
        int s1, s2;
        s1 = samples[k];
        s2 = samples[k+1];
        double th = IQ_GET_PHASE(s1, s2) - ref_phase_shift*k/2;
        double r = get_amplitude(s1, s2);

        out[k] = r*std::cos(th);
        out[k+1] = r*std::sin(th);
    }
}
