using Grpc.Net.Client;

using System.Net.Http;
using System.Threading.Tasks;
using System;
using dartServer;

namespace terminalGuiTest
{
    class Program
    {
        static async Task Main(string[] args)
        {

            for(int i = 0; i< 10; i++)
            {
                var ranThrow = ThrowProvider.Random();
                CheckThrow(ranThrow);
            }

        }


        static void CheckThrow(Throw _throw)
        {

            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  new Greeter.GreeterClient(channel);

            var reply = client.GetThrowResult(
                    new ThrowRequest { XCoord = _throw.XCoord, YCoord = _throw.YCoord});

            var w = 80;
            var h = 20;
            // new DrawDartBoard(w, h).Draw(new DartThrow(reply) , false  );
            Console.WriteLine(
                    new DartThrow(reply).ToJson()
                    );

        }


    }
}

