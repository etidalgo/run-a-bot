using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RunABot
{
    class Program
    {
        static void RunSample()
        {
            string[] commands =
            {
                "Report",
                "Place 2,2,West",
                "Report",
                "Move",
                "Open Pod Bay Doors",
                "Report",
                "Place 3, 3, South",
                "Report",
                "Left",
                "Move",
                "Exterminate",
                "Report"
            };
            CommandLoop.Run(commands.ToList());
        }

        public static void RunFile(string fileName)
        {
            FileStream fileStream = new FileStream(fileName,
                                           FileMode.OpenOrCreate,
                                           FileAccess.Read,
                                           FileShare.Read);

            StreamReader streamReader = new StreamReader(fileStream);
            List<string> lineContents = new List<string>();
            string currLine;
            while ((currLine = streamReader.ReadLine()) != null) {
                lineContents.Add(currLine);
            }
            streamReader.Close( );
            CommandLoop.Run(lineContents); 
        }
        public static void ShowHelp()
        {
            Console.WriteLine("RunABot");
            Console.WriteLine("\tA simple robot simulator - opensourced January 2016");
            Console.WriteLine("\t/f <filename> : Load and run commands from file");
            Console.WriteLine("\t/h            : Show this help screen");
        }
        static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                RunSample();
            }
            else if (string.Equals(args[0], "/f", StringComparison.CurrentCultureIgnoreCase))
            {
                RunFile(args[1]);
                    // run file args[1]
            }
            else if (string.Equals(args[0], "/h", StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(args[0], "/?", StringComparison.CurrentCultureIgnoreCase) )
            {
                ShowHelp();
            }
        }
    }
}
