using System;
using System.Net.Http;
using System.Threading.Tasks;
using dartServer;
using Grpc.Net.Client;

namespace simpleConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  new Greeter.GreeterClient(channel);
            var reply = client.GetThrowResult(
                    new ThrowRequest { XCoord = 0.5, YCoord = 0.2});
            Console.WriteLine(
            "XCoord:" + reply.XCoord +
            "YCoord:" + reply.YCoord +
            "DistanceToCenter:" + reply.DistanceToCenter +
            "HitTheBoard:" + reply.HitTheBoard +
            "Multiplicator:" + reply.Multiplicator +
            "Angle:" + reply.Angle +
            "PointSlot:" + reply.PointSlot +
            "Points:" + reply.Points );
            Console.WriteLine("Press any to exit");
            Console.ReadKey();


        }


    }
}
