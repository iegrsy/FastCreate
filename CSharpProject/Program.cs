using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using Grpc.Core;

namespace CSharpProject
{
    class Program
    {
        public class Options
        {
            [Option('h', "host", Required = true, HelpText = "Set server host.")]
            public string Host { get; set; }

            [Option('p', "port", Required = false, Default = 2222, HelpText = "Set server port.")]
            public int Port { get; set; }
        }

        public class RemoteHelperClient : IDisposable
        {
            private Channel channel;
            private Remotehelper.ServerService.ServerServiceClient client;
            private IClientStreamWriter<Remotehelper.CommandRunRequest> requestStream;

            public RemoteHelperClient(string host, int port)
            {
                try
                {
                    channel = new Channel(host + ":" + port, ChannelCredentials.Insecure);
                    client = new Remotehelper.ServerService.ServerServiceClient(channel);
                    new Task(async () => await RunCommandAsync()).Start();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e);
                    Dispose();
                }
            }

            private async Task RunCommandAsync()
            {
                using (var call = client.RunCommand())
                {
                    var responseStream = call.ResponseStream;
                    requestStream = call.RequestStream;
                    while (await responseStream.MoveNext())
                    {
                        var c = responseStream.Current;
                        Console.WriteLine($"({c.ErrCode}, {c.Stderr})==> {c.Stdout}");
                    }
                }
            }

            public void SendCommand(string cmd)
            {
                if (requestStream == null)
                {
                    System.Console.WriteLine("Not connected");
                    return;
                }

                requestStream.WriteAsync(new Remotehelper.CommandRunRequest()
                {
                    Pwd = "/",
                    CommandName = cmd,
                    Arguments = { "" }
                });
            }

            public void Dispose()
            {
                if (channel != null)
                {
                    try { channel.ShutdownAsync(); } catch { }
                    System.Console.WriteLine("Shutdown client.");
                }
            }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
           .WithParsed<Options>(o => RunWithOpts(o))
           .WithNotParsed<Options>(e => ErrorHandle(e));
        }

        static void ErrorHandle(IEnumerable<Error> e)
        {
            foreach (var item in e)
                System.Console.WriteLine(item.StopsProcessing);
        }

        static void RunWithOpts(Options o)
        {
            using (var client = new RemoteHelperClient(o.Host, o.Port))
            {
                while (true)
                {
                    System.Console.Write("$: ");
                    var cmd = Console.ReadLine();

                    if (cmd.Equals("q") || cmd.Equals("Q"))
                        break;

                    client.SendCommand(cmd);
                }
            }
        }
    }
}
