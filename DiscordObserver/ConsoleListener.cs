using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordObserver
{
    //Early command implementation [WIP]
    static class ConsoleListener
    {
        public static async void ConsoleHandler()
        {
            await Listener();
        }

        public static Task Listener()
        {
            //WIP: Will be changed in future updates
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case ("stop"):
                        Environment.Exit(0);
                        break;
                    case ("clear"):
                        Console.Clear();
                        break;
                    case "help":
                    case "?":
                        Console.WriteLine("Avaliable commands: \n stop - exit program \n clear - clear console \n help / ? - this info \n");
                        break;
                    default:
                        Console.WriteLine("Unknown command!");
                        break;
                }
            }
            return Task.CompletedTask;
        }

    }
}
