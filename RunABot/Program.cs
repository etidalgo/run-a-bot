﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotLibrary;

namespace RunABot
{
    class Program
    {
        static void Main(string[] args)
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
                "Left",
                "Move",
                "Exterminate"
            };

            Board board = new Board(5, 5);
            Bot bot = new Bot(board);
            char[] delimiterChars = { ' ', ','};

            foreach (var cmd in commands)
            {
                // if bot exists, resolve and apply commands
                BotAction action = CommandProcessor.GenerateAction(cmd);
                if (action != null)
                {
                    Console.WriteLine("Attempting command: {0}", cmd);
                    string keyword = CommandProcessor.GetKeyword(cmd);
                    if (!bot.IsPlaced && !string.Equals(keyword, "Place", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Console.WriteLine("Bot not placed, ignoring command: {0}", cmd);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Applying command: {0}", cmd);
                        //action.Apply(bot);
                    }
                }
                else
                {
                    Console.WriteLine("Command not recognized: {0}", cmd);
                }


            }
        }
    }
}
