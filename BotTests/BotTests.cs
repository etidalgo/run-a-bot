using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotLibrary;
using NUnit.Framework;

namespace BotTests
{
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
            }
        }
    }

}
