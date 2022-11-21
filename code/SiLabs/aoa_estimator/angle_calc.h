#include <cstdint>
#include <cinttypes>

#define REFERENCE_PERIOD    8U

double get_phase(int8_t I, int8_t Q);

double calculate_ref_phase_shift(int *ref_samples, int len);
double calculate_phase_shift(int I1, int Q1, int I2, int Q2);

void transform_data(int *samples, int len, double ref_phase_shift, double *out);

double calc_angle(double *samples, int len, int M, int N);
double calc_distance(int rssi, double one_meter_rssi);