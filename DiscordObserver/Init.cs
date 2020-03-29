using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordObserver
{
    class Init
    {
        public string InitFunc()
        {
            string token = "";
            Console.WriteLine("Checking token file...");
            if (!File.Exists("./config.json"))
            {
                token = Request("Enter token: ");
                while (token.Length != 59)
                {
                    Console.WriteLine("ERR: Invalid Token");
                    token = Request("Enter token: ");
                }
                try
                {
                    File.WriteAllText("./config.json", JsonConvert.SerializeObject(token));
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERR: Cant write file(./config.json). " + e.Message);
                    Console.WriteLine("Your token wount be saved");
                }
            }
            Console.Clear();
            try
            {
                token = JsonConvert.DeserializeObject(File.ReadAllText("./config.json")).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERR: Cant read file(./config.json). " + e.Message);
            }
            return token;
        }

        static string Request(string inf)
        {
            Console.WriteLine(inf);
            return (Console.ReadLine());
        }
    }
}
