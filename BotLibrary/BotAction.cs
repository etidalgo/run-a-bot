using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLibrary
{
    public class CommandProcessor
    {
        public enum Keyword
        {
            Unknown = -1,
            Place = 1,
            Report = 2,
            Left = 3,
            Right = 4,
            Move = 5
        };

        static private Dictionary<int, Func<List<string>, BotAction>> mapToActions = new Dictionary<int, Func<List<string>, BotAction>>
        {                                   
            { (int)Keyword.Place , (p) => (CreatePlaceAction(p)) }, 
            { (int)Keyword.Report, (p) => (new BotActionReport()) }, 
            { (int)Keyword.Left  , (p) => (new BotActionLeft()) }, 
            { (int)Keyword.Right , (p) => (new BotActionRight()) }, 
            { (int)Keyword.Move  , (p) => (new BotActionMove()) }, 
        };
        static public BotAction GenerateAction(string command)
        {
            string[] tokens = TokenizeLine(command);
            if (tokens.Count() >= 1) {
                int commandCode;
                if ((commandCode = (int)GetActionCode(tokens[0])) > 0)
                {
                    var tokList = tokens.ToList();
                    tokList.RemoveAt(0);
                    return mapToActions[commandCode](tokList);
                }
            } 
            return null;
        }

        static public string GetKeyword(string str)
        {
            string[] tokens = TokenizeLine(str);
            return (tokens.Count() > 0) ? tokens[0] : "";
        }

        static public string[] TokenizeLine(string str)
        {
            char[] delimiterChars = { ' ', ','};
            return str.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
        }

        // Enum.TryParse(TEnum) Method (String, Boolean, TEnum) (System) <https://msdn.microsoft.com/en-us/library/dd991317(v=vs.110).aspx>
        static public Keyword GetActionCode(string str)
        {
            Keyword keywordValue;
            if (Enum.TryParse(str, true, out keywordValue))
            {
                if (Enum.IsDefined(typeof(Keyword), keywordValue))
                {
                    return keywordValue;
                }
            }
            return Keyword.Unknown;
        }

        // Different from others as it has to read parameters
        static public BotAction CreatePlaceAction(List<string> cmdParms) {
            // params = x, y, direction
            int x, y;
            Direction.DirectionType direction;
            x = Int32.Parse(cmdParms[0]);
            y = Int32.Parse(cmdParms[1]);
            direction = Direction.GetCode(cmdParms[2]);

            return new BotActionPlace( x, y, direction);
        }
    }

    abstract public class BotAction
    {
        abstract public void Apply(Bot bot);
    }

    public class BotActionReport : BotAction
    {
        override public void Apply(Bot bot)
        {
            // Need more generic way to output
            Console.WriteLine("{0}, {1}, {2}", bot.X, bot.Y, bot.Orientation.ToString());
        }
    }

    public class BotActionPlace : BotAction
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction.DirectionType Orientation { get; private set; }

        public BotActionPlace(int x, int y, Direction.DirectionType orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }
        override public void Apply(Bot bot)
        {
            bot.SetCoordinates(X, Y);
            bot.SetOrientation(Orientation);
        }
    }

    public class BotActionMove : BotAction
    {
        private Dictionary<Direction.DirectionType, Func<Tuple<int, int>, Tuple<int, int>>> newCoords = new Dictionary<Direction.DirectionType, Func<Tuple<int, int>, Tuple<int, int> > >
        {                                   
            {Direction.DirectionType.North, (coords) => Tuple.Create( coords.Item1  , coords.Item2+1) },
            {Direction.DirectionType.East , (coords) => Tuple.Create( coords.Item1+1, coords.Item2) },
            {Direction.DirectionType.South, (coords) => Tuple.Create( coords.Item1  , coords.Item2-1) },
            {Direction.DirectionType.West , (coords) => Tuple.Create( coords.Item1-1, coords.Item2) },
        };

        override public void Apply(Bot bot)
        {
            var newXY = newCoords[bot.Orientation](Tuple.Create(bot.X, bot.Y));
            int newX = newXY.Item1;
            int newY = newXY.Item2;

            if (bot.Board.IsValidMove(newX, newY))
            {
                bot.SetCoordinates(newX, newY);
            }
        }
    }

    public class BotActionLeft : BotAction
    {
        private Dictionary<Direction.DirectionType, Direction.DirectionType> newDirection = new Dictionary<Direction.DirectionType, Direction.DirectionType>
        {                                   
            {Direction.DirectionType.North, Direction.DirectionType.West },
            {Direction.DirectionType.East , Direction.DirectionType.North},
            {Direction.DirectionType.South, Direction.DirectionType.East },
            {Direction.DirectionType.West , Direction.DirectionType.South}
        };
        override public void Apply(Bot bot)
        {
            bot.SetOrientation(newDirection[bot.Orientation]);
        }
    }

    public class BotActionRight : BotAction
    {
        private Dictionary<Direction.DirectionType, Direction.DirectionType> newDirection = new Dictionary<Direction.DirectionType, Direction.DirectionType>
        {                                   
            {Direction.DirectionType.North, Direction.DirectionType.East },
            {Direction.DirectionType.East , Direction.DirectionType.South},
            {Direction.DirectionType.South, Direction.DirectionType.West },
            {Direction.DirectionType.West , Direction.DirectionType.North}
        };                                                          
        override public void Apply(Bot bot)                         
        {
            bot.SetOrientation(newDirection[bot.Orientation]);
        }
    }

}
