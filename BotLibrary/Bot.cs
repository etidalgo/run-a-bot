using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLibrary
{
    public class Direction
    {
        enum DirectionType
        {
            North,
            East,
            South,
            West
        }
    }

    public class BotPosition
    {
        public Coords Position { get; set; }
        public Direction Orientation { get; set; }
    }

    class Bot
    {
    }
}
