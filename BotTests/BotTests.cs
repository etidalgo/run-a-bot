using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotLibrary;
using NUnit.Framework;

namespace BotTests
{

    public class BotTest : Bot
    {
        public BotTest(Board board) :
            base(board)
        {

        }
        public void _SetCoordinates(int x, int y)
        {
            SetCoordinates(x, y);
        }

        public void _SetOrientation(Direction.DirectionType orientation)
        {
            SetOrientation(orientation);
        }
    }

    [TestFixture]
    public class BotTestFixture
    {
        [SetUp]
        public void SetupContext()
        {

        }

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
        }

        [Test, TestCaseSource(typeof(TestDataClass), "CommandTestCases")]
        public void RecognizeCommands(string cmd, bool isRecognized)
        {
            BotAction action = CommandProcessor.GenerateAction(cmd);
            Assert.IsTrue(isRecognized ? (action != null) : (action == null));
        }

        [Test, TestCaseSource(typeof(TestDataClass), "MoveActionTestCases")]
        public void VerifyMoveAction(Tuple<int,int> start, Direction.DirectionType orientation, Tuple<int, int> end)
        {
            Board board = new Board(5, 5);
            BotTest bot = new BotTest(board);
            bot._SetCoordinates(start.Item1, start.Item2);
            bot._SetOrientation(orientation);
            BotActionMove action = new BotActionMove();
            action.Apply(bot);
            Assert.AreEqual(end.Item1, bot.X);
            Assert.AreEqual(end.Item2, bot.Y);
        }

        [Test, TestCaseSource(typeof(TestDataClass), "LeftRightTestCases")]
        public void VerifyLeftRightAction(Direction.DirectionType startOrientation, BotAction turnAction, Direction.DirectionType endOrientation)
        {
            Board board = new Board(5, 5);
            Bot bot = new Bot(board);
            BotActionPlace placeAction = new BotActionPlace(2, 2, startOrientation);
            placeAction.Apply(bot);
            turnAction.Apply(bot);
            Assert.AreEqual(endOrientation, bot.Orientation);
        }
    }

    public class TestDataClass
    {

        public static IEnumerable CommandTestCases
        {
            get
            {
                yield return new TestCaseData("Report", true);
                yield return new TestCaseData("Warp 10", false);
                yield return new TestCaseData("Move", true);
                yield return new TestCaseData("Open Pod Bay Doors", false);
                yield return new TestCaseData("Place 2,3,South", true);
                yield return new TestCaseData("Left", true);
                yield return new TestCaseData("  Report", true);
                yield return new TestCaseData("// Report", false);
            }
        }

        // assumes 5x5 board, move 1, use specified initial direction
        public static IEnumerable MoveActionTestCases
        {
            get
            { // start coords, start direction, end coords
                yield return new TestCaseData(Tuple.Create(3, 3), Direction.DirectionType.North, Tuple.Create(3, 4));
                yield return new TestCaseData(Tuple.Create(3, 3), Direction.DirectionType.East , Tuple.Create(4, 3));
                yield return new TestCaseData(Tuple.Create(3, 3), Direction.DirectionType.South, Tuple.Create(3, 2));
                yield return new TestCaseData(Tuple.Create(3, 3), Direction.DirectionType.West , Tuple.Create(2, 3));
                yield return new TestCaseData(Tuple.Create(2, 4), Direction.DirectionType.North, Tuple.Create(2, 4));
                yield return new TestCaseData(Tuple.Create(4, 2), Direction.DirectionType.East , Tuple.Create(4, 2));
                yield return new TestCaseData(Tuple.Create(2, 0), Direction.DirectionType.South, Tuple.Create(2, 0));
                yield return new TestCaseData(Tuple.Create(0, 2), Direction.DirectionType.West , Tuple.Create(0, 2));
            }
        }

        // assume start at 2,2
        public static IEnumerable LeftRightTestCases
        {
            get
            { // start direction, action, end direction
                yield return new TestCaseData(Direction.DirectionType.North, new BotActionLeft(), Direction.DirectionType.West);
                yield return new TestCaseData(Direction.DirectionType.East , new BotActionLeft(), Direction.DirectionType.North);
                yield return new TestCaseData(Direction.DirectionType.South, new BotActionLeft(), Direction.DirectionType.East);
                yield return new TestCaseData(Direction.DirectionType.West , new BotActionLeft(), Direction.DirectionType.South);
                yield return new TestCaseData(Direction.DirectionType.North, new BotActionRight(), Direction.DirectionType.East);
                yield return new TestCaseData(Direction.DirectionType.East , new BotActionRight(), Direction.DirectionType.South);
                yield return new TestCaseData(Direction.DirectionType.South, new BotActionRight(), Direction.DirectionType.West);
                yield return new TestCaseData(Direction.DirectionType.West , new BotActionRight(), Direction.DirectionType.North);
            }
        }

    }

}
