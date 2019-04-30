all:
	dotnet restore
	$(HOME)/.nuget/packages/grpc.tools/1.15.0/tools/linux_x64/protoc -I ./ --csharp_out ./ --grpc_out ./ --plugin=protoc-gen-grpc=$(HOME)/.nuget/packages/grpc.tools/1.15.0/tools/linux_x64/grpc_csharp_plugin *.proto
	dotnet restore
	dotnet build
	echo "finish"
clean:
	dotnet clean
	rm AlgorithmCommunication.cs
	rm AlgorithmCommunicationGrpc.cs
	rm -rf bin obj
run:
	dotnet run