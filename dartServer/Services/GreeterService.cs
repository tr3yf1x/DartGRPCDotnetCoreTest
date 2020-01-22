using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using DartLogic;

namespace dartServer
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<ThrowReply> GetThrowResult(ThrowRequest request, ServerCallContext context)
        {
            ThrowSummary summary = new ThrowAnalyzer().GetThrowSummary(request.XCoord, request.YCoord);
            return Task.FromResult(new ThrowReply
                    {
XCoord = summary.xCoord,
YCoord = summary.yCoord,
DistanceToCenter = summary.distanceToCenter,
HitTheBoard = summary.hitTheBoard,
Multiplicator = summary.multiplicator,
Angle = summary.angle,
PointSlot = summary.pointSlot,
Points = summary.points
                    });

        }
    }
}
