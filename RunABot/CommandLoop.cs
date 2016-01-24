using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using BotLibrary;

namespace RunABot
{
    public class CommandEnvironment
    {
        public Board Board { get; protected set; }
        public Bot Bot { get; protected set; }

        public CommandEnvironment()
        {
            Board = new Board(5, 5);
            Bot = new Bot(Board);
        }
        public void Run(List<string> commands)
        {
            foreach (var cmd in commands)
            {
                RunCommand(cmd);
            }
        }

        public void Run(IEnumerable<string> commands)
        {
            foreach (var cmd in commands)
            {
                RunCommand(cmd);
            }
        }
        public void RunCommand(string cmd)
        {
            // resolve and apply commands
            BotAction action = CommandProcessor.GenerateAction(cmd);
            if (action != null)
            {
                Debug.Write(String.Format("Attempting command: {0}", cmd));
                string keyword = CommandProcessor.GetKeyword(cmd);
                if (!this.Bot.IsPlaced && !string.Equals(keyword, "Place", StringComparison.CurrentCultureIgnoreCase))
                {
                    Debug.Write(String.Format("Bot not placed, ignoring command: {0}", cmd));
                    return;
                }
                else
                {
                    Console.WriteLine("Applying command: {0}", cmd);
                    action.Apply(this.Bot);
                }
            }
            else
            {
                Debug.Write(String.Format("Command not recognized: {0}", cmd));
            }
        }
    }
}
