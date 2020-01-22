using System;
using System.Linq;
using System.Threading.Tasks;
using dartServer;
using Grpc.Net.Client;

namespace BlazorFrontend.Data
{
    public class DartService
    {
        public DartThrow GetDartThrow(double _xCoord, double _yCoord)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  new Greeter.GreeterClient(channel);
            var reply = client.GetThrowResult(
                    new ThrowRequest { XCoord = 0.5, YCoord = 0.2});

            var retVal = new DartThrow();


            retVal.xCoord  =  reply.XCoord;
            retVal.yCoord  =  reply.YCoord;
            retVal.distanceToCenter  =  reply.DistanceToCenter;
            retVal.hitTheBoard  =  reply.HitTheBoard;
            retVal.multiplicator  =  reply.Multiplicator;
            retVal.angle  =  reply.Angle;
            retVal.pointSlot  =  reply.PointSlot;
            retVal.points  =  reply.Points ;

            return retVal;


        }
    }
}
