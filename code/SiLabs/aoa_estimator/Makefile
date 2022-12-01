all: build_dir build/angle_calc

build_dir:
	mkdir -p build

# Create objects from C SRC files
build/%.o: %.cpp
	@echo "Building file: $<"
	g++ -fopenmp -c -O2 -o $@ $<

# Link
build/angle_calc: build/main.o build/angle_calc.o
	@echo "Linking target: $@"
	g++ $^ -fopenmp -lmosquitto -lpthread -o $@
