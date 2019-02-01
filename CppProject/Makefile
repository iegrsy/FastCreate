DIRS=build

all:
		@protoc -I . --cpp_out=. --grpc_out=. --plugin=protoc-gen-grpc=`which grpc_cpp_plugin` *.proto
		$(shell mkdir -p $(DIRS))
		cd build && qmake ../CppProject.pro && make
clean:
		rm -rf build
		rm *.pb.*
