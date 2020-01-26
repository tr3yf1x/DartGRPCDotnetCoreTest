using dartServer;
using System;
using Newtonsoft.Json;

namespace terminalGuiTest
{
    class DartThrow
    {
        public double xCoord;
        public double yCoord;
        public double distanceToCenter;
        public bool hitTheBoard;
        public int multiplicator;
        public double angle;
        public int pointSlot;
        public int points;

        public DartThrow(ThrowReply _reply)
        {
            xCoord = _reply.XCoord;
            yCoord = _reply.YCoord;
            distanceToCenter = _reply.DistanceToCenter;
            hitTheBoard = _reply.HitTheBoard;
            multiplicator = _reply.Multiplicator;
            angle = _reply.Angle;
            pointSlot = _reply.PointSlot;
            points = _reply.Points;
        }

        public String ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}



