using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using BotLibrary;

namespace BotLibrary
{
    public class Board
    {
        public int XSize { get; private set; }
        public int YSize { get; private set; }

        public Board(int x, int y)
        {
            XSize = x;
            YSize = y;
        }

        public bool IsValidMove(int x, int y)
        {
            return (x >= 0 && x < XSize) && (y >= 0 && y < YSize);
        }
    }
}
