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
            { (int)Keyword.Place , (p) => (new BotAction()) }, 
            { (int)Keyword.Report, (p) => (new BotAction()) }, 
            { (int)Keyword.Left  , (p) => (new BotAction()) }, 
            { (int)Keyword.Right , (p) => (new BotAction()) }, 
            { (int)Keyword.Move  , (p) => (new BotAction()) }, 
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

        // differs to read parameters
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

    public class BotAction
    {
        virtual public void Apply(Bot bot)
        {

        }
    }

    public class BotActionPlace : BotAction
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction.DirectionType Orientation { get; set; }

        public BotActionPlace(int x, int y, Direction.DirectionType orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }
        override public void Apply(Bot bot)
        {
            bot.X = X;
            bot.Y = Y;
            bot.Orientation = Orientation;
            bot.IsPlaced = true;
        }
    }

    public class BotActionMove : BotAction
    {
        private Dictionary<Direction.DirectionType, Func<Tuple<int, int>, Tuple<int, int>>> newCoords = new Dictionary<Direction.DirectionType, Func<Tuple<int, int>, Tuple<int, int> > >
        {                                   
            {Direction.DirectionType.North, (coords) => Tuple.Create( coords.Item1, coords.Item2) },
            {Direction.DirectionType.East , (coords) => Tuple.Create( coords.Item1, coords.Item2) },
            {Direction.DirectionType.South, (coords) => Tuple.Create( coords.Item1, coords.Item2) },
            {Direction.DirectionType.West , (coords) => Tuple.Create( coords.Item1, coords.Item2) },
        };

        override public void Apply(Bot bot)
        {
            var newXY = newCoords[bot.Orientation](Tuple.Create(bot.X, bot.Y));
            int newX = newXY.Item1;
            int newY = newXY.Item2;
            bot.X = newX;
            bot.Y = newY;
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
            bot.Orientation = newDirection[bot.Orientation];
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
            bot.Orientation = newDirection[bot.Orientation];
        }
    }

}
