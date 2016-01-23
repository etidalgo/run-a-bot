using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using BotLibrary;

namespace BotLibrary
{
    public class Coords
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Board
    {
        public Coords Dimensions { get; set; }

        public Board(int x, int y)
        {
            Dimensions = new Coords(x, y);
        }

    }
}
