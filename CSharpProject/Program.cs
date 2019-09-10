using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace CSharpProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { Utest.DeviceService.BindService(new UtestImpl()) },
                Ports = { new ServerPort("0.0.0.0", 60151, ServerCredentials.Insecure) }
            };
            server.Start();
            System.Console.WriteLine("Start server: 0.0.0.0:60151");
            Thread.Sleep(Timeout.Infinite);
        }
    }

    class UtestImpl : Utest.DeviceService.DeviceServiceBase
    {
        public override System.Threading.Tasks.Task<Utest.RealTimeValues> GetRealTimeValues(Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"GetRealTimeValues: {context.Peer}");

            return Task.FromResult(new Utest.RealTimeValues()
            {
                Displacement = (float)new Random().NextDouble(),
                Load = (float)new Random().NextDouble(),
                DisplacementExt = (float)new Random().NextDouble(),
                Strain = (float)new Random().NextDouble(),
                Stress = (float)new Random().NextDouble()
            });
        }

        public override global::System.Threading.Tasks.Task<global::Utest.CommandResult> Start(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"Start: {context.Peer}");
            return Task.FromResult(new Utest.CommandResult() { Err = 0 });
        }

        public override global::System.Threading.Tasks.Task<global::Utest.CommandResult> Stop(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"Stop: {context.Peer}");
            return Task.FromResult(new Utest.CommandResult() { Err = 0 });
        }

        public override global::System.Threading.Tasks.Task<global::Utest.CommandResult> Pause(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"Pause: {context.Peer}");
            return Task.FromResult(new Utest.CommandResult() { Err = 0 });
        }

        public override global::System.Threading.Tasks.Task<global::Utest.CommandResult> Unload(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"Unload: {context.Peer}");
            return Task.FromResult(new Utest.CommandResult() { Err = 0 });
        }

        public override global::System.Threading.Tasks.Task<global::Utest.GetChannelsResponse> GetChannels(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"GetChannels: {context.Peer}");
            throw new RpcException(new Status(StatusCode.Unimplemented, "GetChannels method"));
        }

        public override global::System.Threading.Tasks.Task<global::Utest.CommandResult> SetChannel(global::Utest.SetChannelRequest request, ServerCallContext context)
        {
            System.Console.WriteLine($"SetChannel: {context.Peer}");
            return Task.FromResult(new Utest.CommandResult() { Err = 0 });
        }

        public override global::System.Threading.Tasks.Task<global::Utest.IOStatusResponse> GetIOStatus(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"GetIOStatus: {context.Peer}");
            throw new RpcException(new Status(StatusCode.Unimplemented, "GetIOStatus method"));
        }

        public override global::System.Threading.Tasks.Task<global::Utest.CommandResult> SetIOStatus(global::Utest.IOStatusResponse request, ServerCallContext context)
        {
            System.Console.WriteLine($"SetIOStatus: {context.Peer}");
            return Task.FromResult(new Utest.CommandResult() { Err = 0 });
        }

        public override global::System.Threading.Tasks.Task<global::Utest.RealTimeValues> GetLogValues(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"GetLogValues: {context.Peer}");
            throw new RpcException(new Status(StatusCode.Unimplemented, "GetLogValues method"));
        }

        public override global::System.Threading.Tasks.Task<global::Utest.GetStatusResponse> GetStatus(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"GetStatus: {context.Peer}");
            throw new RpcException(new Status(StatusCode.Unimplemented, "GetStatus method"));
        }

        public override global::System.Threading.Tasks.Task<global::Utest.GetDiagnosticsResponse> GetDiagnostics(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"GetDiagnostics: {context.Peer}");
            throw new RpcException(new Status(StatusCode.Unimplemented, "GetDiagnostics method"));
        }

        public override global::System.Threading.Tasks.Task<global::Utest.GetFirmwareResponse> GetFirmware(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            System.Console.WriteLine($"GetFirmware: {context.Peer}");
            return Task.FromResult(new Utest.GetFirmwareResponse() { ProgramFw = "1.1.2", CommlibFw = "2.2.2", DeltaFw = "4.4.4" });
        }

        public override global::System.Threading.Tasks.Task<global::Utest.CustomCommandResponse> SendCustomCommand(global::Utest.CustomCommandRequest request, ServerCallContext context)
        {
            System.Console.WriteLine($"SendCustomCommand: {context.Peer}");
            throw new RpcException(new Status(StatusCode.Unimplemented, "SendCustomCommand method"));
        }

        public override global::System.Threading.Tasks.Task<global::Utest.CommandResult> WriteControllerRegister(global::Utest.RegisterWriteRequest request, ServerCallContext context)
        {
            System.Console.WriteLine($"WriteControllerRegister: {context.Peer}");
            return Task.FromResult(new Utest.CommandResult() { Err = 0 });
        }
    }
}
