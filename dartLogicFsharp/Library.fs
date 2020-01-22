namespace dartLogicFsharp

open System

module DartLogic =
    type NormalizedPoint = {xCoord: double; yCoord: double}
    type DistanceMultiplicatorPair = {distance: double; multiplicator: int}
    type AnglePointsPair = {points:int; angle:double}
    type DistancePoints = abstract member points : int -> int
    let random = System.Random()
    let randomDoubleProvider() : double = random.NextDouble()
    let randomCoord() : double = randomDoubleProvider() - randomDoubleProvider()
    let randomPointProvider() : NormalizedPoint = {xCoord = randomCoord(); yCoord = randomCoord()}
    let fixedPointProvider() : NormalizedPoint = {xCoord = 0.3; yCoord = 0.8}
    let distanceToCenter(_point : NormalizedPoint) : double = sqrt ( (_point.xCoord ** 2.0) + (_point.yCoord ** 2.0) )
    let zeroDegreeHorizonVector  : NormalizedPoint  = {xCoord = 1.0; yCoord = 0.0}
    
    let hitTheBoard(_point : NormalizedPoint) : bool = distanceToCenter(_point) < 1.0
    let getDistanceMultiplicatorPair() : List<DistanceMultiplicatorPair> =
        let doubleTripleSize = 8.0
        let center = {distance = 0.0; multiplicator = 50}
        let outerBoundDoubleBull = {distance = 12.7; multiplicator = 25}
        let outerBoundSingleBull = {distance = 31.8; multiplicator = 1}
        let outerBoundDouble = {distance = 170.0; multiplicator = 0 }
        let innerBoundDouble = {distance = outerBoundDouble.distance - doubleTripleSize; multiplicator = 2}
        let outerBoundTriple = {distance = 107.0; multiplicator = 1}
        let innerBoundTriple = {distance = outerBoundTriple.distance - doubleTripleSize; multiplicator = 3}
        let missed = {distance = 999.99; multiplicator = 0}
    
        [ center; outerBoundDoubleBull; outerBoundSingleBull; outerBoundDouble; innerBoundDouble; outerBoundTriple; innerBoundTriple; missed]
    
    let getPossibleDistanceMultiplicators(_distance : double) : List<DistanceMultiplicatorPair> =
        let absDistance = abs _distance * 140.0
        List.filter (fun(pair : DistanceMultiplicatorPair) -> pair.distance > absDistance) (getDistanceMultiplicatorPair())
    
    let getDistanceMultiplicator(_distance : double) : DistanceMultiplicatorPair =
        (List.sortBy( fun(_pair : DistanceMultiplicatorPair) -> _pair.distance) (getPossibleDistanceMultiplicators(_distance))).Head
    
    let getSin (_point : NormalizedPoint) : double  =
        zeroDegreeHorizonVector.xCoord * _point.yCoord - _point.xCoord * zeroDegreeHorizonVector.yCoord; 
    
    let getCos (_point : NormalizedPoint) : double =
        zeroDegreeHorizonVector.xCoord * _point.xCoord + zeroDegreeHorizonVector.yCoord * _point.yCoord
    
    let getAtanInRads (_point : NormalizedPoint) : double =
        atan2 (getSin _point) (getCos _point)
    
    let getAtanInDegrees (_point : NormalizedPoint) : double =
        (getAtanInRads _point) * (180.0 / System.Math.PI)
    
    let getAngle(_point : NormalizedPoint) : float =
        let preVal = getAtanInDegrees _point
        let retVal = if preVal < 0.0 then 360.0 + preVal else preVal
        retVal
    
    let AnglePointsMap : List<AnglePointsPair> =
        [{points=6;angle=9.0};{points=10;angle=26.0}; {points=15;angle=44.0};{points=2;angle=62.0};
        {points=17;angle=80.0}; {points=3;angle=98.0}; {points=19;angle=116.0}; {points=7;angle=134.0};
        {points=16;angle=152.0}; {points=8;angle=170.0}; {points=11;angle=188.0}; {points=14;angle=206.0};
        {points=9;angle=224.0}; {points=12;angle=242.0}; {points=5;angle=260.0}; {points=20;angle=278.0};
        {points=1;angle=296.0}; {points=18;angle=314.0}; {points=4;angle=332.0}; {points=13;angle=350.0};
        {points=6;angle=360.0}]
    
    let getPossbilePointsForAngle(_angle: double) : List<AnglePointsPair> = List.filter( fun(_pair : AnglePointsPair) -> _pair.angle >= _angle) AnglePointsMap
    
    let getPointsForAngle(_angle: double) : AnglePointsPair =
        (List.sortBy( fun(_pair : AnglePointsPair) -> _pair.angle) (getPossbilePointsForAngle(_angle))).Head
    
    let getPoints(_point : NormalizedPoint) : int =
        let multiplicator = (getDistanceMultiplicator (distanceToCenter _point)).multiplicator
        match multiplicator with
        | 50 -> multiplicator
        | 25 -> multiplicator
        | _ ->  multiplicator * (getPointsForAngle(getAngle(_point))).points
    
    type ThrowSummary = {   xCoord : double;
                            yCoord : double;
                            distanceToCenter : double;
                            hitTheBoard : bool;
                            multiplicator : int;
                            angle : double;
                            pointSlot : int;
                            points : int    }
    
    let buildThrow(_point : NormalizedPoint) : ThrowSummary =
        {xCoord = _point.xCoord; yCoord = _point.yCoord; distanceToCenter = (distanceToCenter _point);
                                    hitTheBoard = (hitTheBoard _point); multiplicator = (getDistanceMultiplicator( distanceToCenter _point)).multiplicator;
                                    angle = (getAngle _point); pointSlot = getPointsForAngle(getAngle(_point)).points; points = (getPoints _point)}
    
    let summary(_throw : ThrowSummary) =
        printfn "xCoord: %A yCoord: %A" _throw.xCoord _throw.yCoord
        printfn "Distance to Center: %A" _throw.distanceToCenter
        printfn "Hit the Board: %A" _throw.hitTheBoard
        printfn "Multiplicator: %i" _throw.multiplicator
        printfn "Angle: %A" _throw.angle
        printfn "PointSlot: %i (Angle: %A)" _throw.pointSlot _throw.angle
        printfn "Points: %i" _throw.points
        printfn "-----------"
