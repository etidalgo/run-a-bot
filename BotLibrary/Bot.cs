using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *     public interface SparkTest<T>
    {

    }

    public interface ITestPatientDataAccess : SparkTest<IPatientDataAccess>
    {
        string getPatientCode(PatientDemographic patDem);
    }

 */
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
        public int X { get; set; }
        public int Y { get; set; }
        public Direction.DirectionType Orientation { get; set; }
        public bool IsPlaced { get; set; }

        public Board Board { get;  set; }

        public Bot()
        {
            IsPlaced = false;
        }

        public Bot(Board board)
        {
            IsPlaced = false;
            Board = board;
        }

        public void Report()
        {
            // Need more generic way to output
            Console.WriteLine("{0}, {1}, {2}", X, Y, Orientation.ToString());
        }
    }
}
