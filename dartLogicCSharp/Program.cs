using System;
using System.Collections.Generic;
using System.Linq;
using DartLogic;
using System.ComponentModel;

namespace dartLogicCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ThrowSummary summary = new ThrowAnalyzer().GetThrowSummary(0.0, 0.0);

            new ThrowAnalyzer().PrintSummary(summary);

            summary = new ThrowAnalyzer().GetThrowSummary(0.3, 0.3);
            new ThrowAnalyzer().PrintSummary(summary);

        }

    }
}

namespace DartLogic
{
    public struct NormalizedPoint { public double xCoord; public double yCoord;}
    public class DistanceMultiplicatorPair {
        public double distance; 
        public int multiplicator;
        public DistanceMultiplicatorPair(double _distance, int _multiplicator)
        {
            this.distance = _distance;
            this.multiplicator = _multiplicator;
        }
    }

    public class AnglePointsPair {
        public int points; 
        public double angle;
        public AnglePointsPair(int _points, double _angle)
        {
            this.points = _points;
            this.angle = _angle;
        }
    }

    public class ThrowSummary
    {
        public double xCoord;
        public double yCoord;
        public double distanceToCenter;
        public bool hitTheBoard;
        public int multiplicator;
        public double angle;
        public int pointSlot;
        public int points;

        public ThrowSummary( double _xCoord, double _yCoord, double _distanceToCenter, bool _hitTheBoard, 
                int _multiplicator, double _angle, int _pointSlot, int _points)
        {
            xCoord = _xCoord;
            yCoord = _yCoord;
            distanceToCenter = _distanceToCenter;
            hitTheBoard = _hitTheBoard;
            multiplicator = _multiplicator;
            angle = _angle;
            pointSlot = _pointSlot;
            points = _points;
        }
    }

    class ThrowAnalyzer
    {


        public double DistanceToCenter(NormalizedPoint _point)
        {
            return Math.Sqrt(( _point.xCoord * _point.xCoord) + (_point.yCoord * _point.yCoord));
        }

        public NormalizedPoint ZeroDegreeHorizonVector(){
            NormalizedPoint retVal;
            retVal.xCoord =  1.0;
            retVal.yCoord = 0.0;
            return retVal;
        }

        public bool HitTheBoard(NormalizedPoint _point)
        {
            return DistanceToCenter(_point) < 1.0;
        }

        public List<DistanceMultiplicatorPair> GetDistanceMultiplicatorPairs()
        {
            var doubleTripleSize = 8.0;
            var center = new DistanceMultiplicatorPair(0.0, 50);
            var outerBoundDoubleBull = new DistanceMultiplicatorPair(12.7, 25);
            var outerBoundSingleBull = new DistanceMultiplicatorPair(31.8, 1);
            var outerBoundDouble = new DistanceMultiplicatorPair(170.0, 0 );
            var innerBoundDouble = new DistanceMultiplicatorPair(outerBoundDouble.distance - doubleTripleSize, 2);
            var outerBoundTriple = new DistanceMultiplicatorPair(107.0, 1);
            var innerBoundTriple = new DistanceMultiplicatorPair(outerBoundTriple.distance - doubleTripleSize, 3);
            var missed = new DistanceMultiplicatorPair(999.99, 0);

            return new List<DistanceMultiplicatorPair>{ center, outerBoundDoubleBull, outerBoundSingleBull, 
                outerBoundDouble, innerBoundDouble, outerBoundTriple,
                innerBoundTriple, missed};
        }

        public List<DistanceMultiplicatorPair> GetPossibleDistanceMultiplicators(double _distance) 
        {
            var absDistance = Math.Abs(_distance * 140.0);
            return GetDistanceMultiplicatorPairs().FindAll(item => item.distance >= absDistance);
        }

        public DistanceMultiplicatorPair GetDistanceMultiplicator(double _distance)
        {
            return GetPossibleDistanceMultiplicators(_distance).OrderBy(item => item.distance).ToList().First();
        }

        public double GetSin(NormalizedPoint _point)
        {
            return ZeroDegreeHorizonVector().xCoord * _point.yCoord - _point.xCoord * ZeroDegreeHorizonVector().yCoord;
        }

        public double GetCos(NormalizedPoint _point)
        {
            return ZeroDegreeHorizonVector().xCoord + _point.xCoord + ZeroDegreeHorizonVector().yCoord + _point.yCoord;
        }

        public double GetAtanInRads(NormalizedPoint _point)
        {
            return Math.Atan2(GetSin(_point), GetCos(_point));
        }

        public double GetAtanInDegrees(NormalizedPoint _point) 
        {
            return GetAtanInRads(_point) * (180.0 / System.Math.PI);
        }

        public double GetAngle(NormalizedPoint _point)
        {
            var preVal = GetAtanInDegrees(_point);
            return  preVal < 0.0 ? 360 + preVal : preVal;
        }

        public List<AnglePointsPair> AnglePointsMap()
        {
            return new List<AnglePointsPair> {  new AnglePointsPair(6,9.0), new AnglePointsPair(10,26.0), new AnglePointsPair(15,44.0), 
                new AnglePointsPair(2,62.0), new AnglePointsPair(17,80.0), new AnglePointsPair(3,98.0), 
                new AnglePointsPair(19,116.0), new AnglePointsPair(7,134.0), new AnglePointsPair(16,152.0),
                new AnglePointsPair(8,170.0), new AnglePointsPair(11,188.0), new AnglePointsPair(14,206.0), 
                new AnglePointsPair(9,224.0), new AnglePointsPair(12,242.0), new AnglePointsPair(5,260.0),
                new AnglePointsPair(20,278.0), new AnglePointsPair(1,296.0), new AnglePointsPair(18,314.0),
                new AnglePointsPair(4,332.0), new AnglePointsPair(13,350.0), new AnglePointsPair(6,360.0)};
        }

        public List<AnglePointsPair> GetPossiblePointsForAngle(double _angle)
        {
            return AnglePointsMap().FindAll(item => item.angle >= _angle);
        }

        public AnglePointsPair GetPointsForAngle(double _angle)
        {
            return GetPossiblePointsForAngle(_angle).OrderBy(item => item.angle).First();
        }

        public int GetPoints(NormalizedPoint _point)
        {
            var multiplicator = GetDistanceMultiplicator(DistanceToCenter(_point)).multiplicator;
            return multiplicator >= 25 ? multiplicator : multiplicator * GetPointsForAngle(GetAngle(_point)).points;
        }

        public ThrowSummary GetThrowSummary(NormalizedPoint _point)
        {
            return new ThrowSummary(   _point.xCoord, _point.yCoord, DistanceToCenter(_point), HitTheBoard(_point), 
                    GetDistanceMultiplicator(DistanceToCenter(_point)).multiplicator,
                    GetAngle(_point), GetPointsForAngle(GetAngle(_point)).points, GetPoints(_point));
        }

        public ThrowSummary GetThrowSummary(double _xCoord, double _yCoord)
        {
            var point = new NormalizedPoint();
            point.xCoord = _xCoord;
            point.yCoord = _yCoord;
            return GetThrowSummary(point);
        }

        public void PrintSummary(ThrowSummary _throw)
        {
            Console.WriteLine("xCoord: " +  _throw.xCoord);
            Console.WriteLine("yCoord: " + _throw.yCoord);
            Console.WriteLine("Distance to Center: " +  _throw.distanceToCenter);
            Console.WriteLine("Hit the Board: " +  _throw.hitTheBoard);
            Console.WriteLine("Multiplicator: " +  _throw.multiplicator);
            Console.WriteLine("Angle: " +  _throw.angle);
            Console.WriteLine("PointSlot: " + _throw.pointSlot);
            Console.WriteLine("Angle: " +  _throw.angle);
            Console.WriteLine("Points: " +  _throw.points);
        }


    }
}
