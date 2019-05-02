using System;
using Grpc.Core;
using AlgorithmCommunication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpProject
{
    class Program
    {
        static bool isRun = false;

        static void Main(string[] args)
        {
            string _host = "192.170.0.28";
            int _port = 2121;

            Channel channel = new Channel(_host + ":" + _port, ChannelCredentials.Insecure);
            DiagnosticService.DiagnosticServiceClient client = new DiagnosticService.DiagnosticServiceClient(channel);

            new Task(async () => { isRun = true; await streamStatusAsync(client); }).Start();
            //getStatus(client);

            System.Console.WriteLine("Press any key for exit...");
            System.Console.ReadKey();
            isRun = false;
        }

        static void getStatus(DiagnosticService.DiagnosticServiceClient client)
        {
            try
            {
                DiagnosticInfo info = client.GetDiagnosticStatus(new DiagnosticQ(), deadline: DateTime.UtcNow.AddSeconds(2));
                System.Console.WriteLine(info.ToString());
            }
            catch (Grpc.Core.RpcException e) { System.Console.WriteLine(e.Message); }
        }

        static async System.Threading.Tasks.Task streamStatusAsync(DiagnosticService.DiagnosticServiceClient client)
        {
            try
            {
                using (var call = client.GetDiagnosticStatusStream(new DiagnosticQ()))
                {
                    while (isRun && await call.ResponseStream.MoveNext())
                    {
                        DiagnosticInfo info = call.ResponseStream.Current;
                        Console.WriteLine("Received: " + info.ToString());
                    }
                }
            }
            catch (Grpc.Core.RpcException e) { System.Console.WriteLine(e.Message); }
        }
    }
}
