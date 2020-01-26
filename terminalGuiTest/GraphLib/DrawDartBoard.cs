using System;
using System.Collections.Generic;

namespace terminalGuiTest
{

    class DrawDartBoard
    {
        int width;
        int height;
        String barHorizBorder; 
        String barVertBorder;
        String placeholder;

        public DrawDartBoard(int _width, int _height, String _barHorizBorder = "-", String _barVertBorder = "|", String _placeholder = "x")
        {
            width = _width;
            height = _height;
            barHorizBorder = _barHorizBorder;
            barVertBorder = _barVertBorder;
            placeholder = _placeholder;
        }

        public void Draw(DartThrow _dartThrow, bool clear = true)
        {
            if(clear) Console.Clear();
            this.StatusBar(_dartThrow);
        }

        void StatusBar(DartThrow _dartThrow)
        {
            this.StatusBarBoarder(_dartThrow);
            this.StatusBarItems(_dartThrow);
            this.StatusBarBoarder(_dartThrow);
        }


        List<KeyValuePair<string, string>> ThrowStatusItems(DartThrow _dartThrow)
        {
            var retVal = new List<KeyValuePair<string, string>>();
            retVal.Add(new KeyValuePair<string, string>("X", _dartThrow.xCoord.ToString()));
            retVal.Add(new KeyValuePair<string, string>("Y", _dartThrow.yCoord.ToString()));
            retVal.Add(new KeyValuePair<string, string>("Hit", _dartThrow.hitTheBoard.ToString()));
            retVal.Add(new KeyValuePair<string, string>("Points", _dartThrow.points.ToString()));
            retVal.Add(new KeyValuePair<string, string>("Multi", _dartThrow.multiplicator.ToString()));
            retVal.Add(new KeyValuePair<string, string>("Slot", _dartThrow.pointSlot.ToString()));
            return retVal;
        }

        void StatusBarItems(DartThrow _dartThrow)
        {
            var tsi = ThrowStatusItems(_dartThrow);
            String line = barVertBorder;

            tsi.ForEach(item => line += item.Key + ":" + item.Value + barVertBorder);
            line += RepeatString(placeholder, (width- line.Length));

            Console.WriteLine(line);
        }

        void StatusBarBoarder(DartThrow _dartThrow)
        {
            Console.WriteLine(RepeatString(barHorizBorder, width));
        }

        string RepeatString(String _String, int _repeats)
        {
            string line = "";
            for(int i = 0; i <_repeats; i++)
            {
                line += _String;
            }
            return line;
        }
    }
}
