using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using BotLibrary;

namespace RunABot
{
    public class CommandLoop
    {
        public static void Run(List<string> commands)
        {
            Board board = new Board(5, 5);
            Bot bot = new Bot(board);
            char[] delimiterChars = { ' ', ',' };

            foreach (var cmd in commands)
            {
                // resolve and apply commands
                BotAction action = CommandProcessor.GenerateAction(cmd);
                if (action != null)
                {
                    Debug.Write(String.Format("Attempting command: {0}", cmd));
                    string keyword = CommandProcessor.GetKeyword(cmd);
                    if (!bot.IsPlaced && !string.Equals(keyword, "Place", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Debug.Write(String.Format("Bot not placed, ignoring command: {0}", cmd));
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Applying command: {0}", cmd);
                        action.Apply(bot);
                    }
                }
                else
                {
                    Debug.Write(String.Format("Command not recognized: {0}", cmd));
                }
            }
        }
    }
}
