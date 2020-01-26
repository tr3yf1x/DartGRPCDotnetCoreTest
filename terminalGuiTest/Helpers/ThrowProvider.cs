namespace terminalGuiTest
{
    class Throw
    {
        public double XCoord; 
        public double YCoord; 
    }

    class ThrowProvider
    {
        public static Throw Random()
        {
            return new Throw(){

                XCoord = RandomCoord(), 
                       YCoord = RandomCoord(),
            };

        }

        public static double RandomCoord()
        {
            var ran =  new System.Random();
            return ran.NextDouble() - ran.NextDouble();
        }

    }
}
