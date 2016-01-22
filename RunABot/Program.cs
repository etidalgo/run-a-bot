using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunABot
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] commands =
            {
                "Report",
                "Place 2,2,W",
                "Report",
                "Move",
                "Report",
                "Left",
                "Move"
            };

            foreach (var cmd in commands)
            {
                // apply command
            }
        }
    }
}
