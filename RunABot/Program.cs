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
            CommandEnvironment commandEnv = new CommandEnvironment();
            commandEnv.Run(commands.ToList());
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

            CommandEnvironment commandEnv = new CommandEnvironment();
            commandEnv.Run(lineContents);
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
            switch( args[0].ToLower()) {
                case "/f":
                    RunFile(args[1]); // /f <filenam>
                    break;

                case "/h":
                case "/?":
                    ShowHelp();
                    break;
            }
        }
    }
}
