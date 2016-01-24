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
        public enum DirectionType
        {
            Unknown = -1,
            North = 1,
            East = 2,
            South = 3,
            West = 4
        };

        static public DirectionType GetCode(string str)
        {
            DirectionType directionValue;
            if (Enum.TryParse(str, true, out directionValue))
            {
                if (Enum.IsDefined(typeof(DirectionType), directionValue))
                {
                    return directionValue;
                }
            }
            return DirectionType.Unknown;
        }
    }

    public class Bot
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Direction.DirectionType Orientation { get; protected set; }
        public bool IsPlaced { get; protected set; }

        public Board Board { get;  protected set; }

        public Bot()
        {
            IsPlaced = false;
        }

        public Bot(Board board)
        {
            IsPlaced = false;
            Board = board;
        }

        protected internal void SetCoordinates(int x, int y)
        {
            X = x;
            Y = y;
            IsPlaced = true;
        }

        protected internal void SetOrientation(Direction.DirectionType orientation)
        {
            Orientation = orientation;
        }
    }
}
