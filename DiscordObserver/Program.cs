using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System.Linq;

namespace DiscordObserver
{
    class Program
    {
        public static DiscordClient client;
        static void Main(string[] args)
        {
            DiscordConfiguration config = new DiscordConfiguration();
            config.TokenType = TokenType.User;
            Console.WriteLine("Checking token file...");
            if (!File.Exists("./config.json"))
            {
                string token = Request("Enter token: ");
                while (token.Length != 59)
                {
                    Console.WriteLine("ERR: Invalid Token");
                    token = Request("Enter token: ");
                }
                File.WriteAllText("./config.json", JsonConvert.SerializeObject(token));
            }
            Console.Clear();
            config.Token = JsonConvert.DeserializeObject(File.ReadAllText("./config.json")).ToString();
            Console.WriteLine("Initializing client...");
            client = new DiscordClient(config);
            client.Ready += onReady;
            client.ClientErrored += onERR;
            client.SocketErrored += onSerr;
            client.MessageDeleted += MsgLogger;
            client.ConnectAsync();
            
            Thread.Sleep(-1);
        }
        static string Request(string inf)
        {
            Console.WriteLine(inf);
            return (Console.ReadLine());
        }

        public static Task MsgLogger(MessageDeleteEventArgs e)
        {
            
            MsgClass msg = new MsgClass();
            try
            {
                try{
                    Console.WriteLine("Channel: " + e.Message.Channel.Name + " | " + e.Message.Channel.Guild.Name);
                    msg.channel = e.Message.Channel.Name;    msg.server = e.Message.Channel.Guild.Name;
                }
                catch {}
                Console.WriteLine("By: "+e.Message.Author.Username);    msg.Author = e.Message.Author.Username;
                Console.WriteLine("Message: " + e.Message.Content);     msg.message = e.Message.Content;
                Console.WriteLine("At: "+e.Message.CreationTimestamp.UtcDateTime.ToString()+" - "+DateTime.Today.ToString());   
                msg.C_time = e.Message.CreationTimestamp.UtcDateTime; msg.R_time = DateTime.Today;
                if (e.Message.MentionedUsers.Contains(client.CurrentUser)) { Console.WriteLine("Mentioned!"); msg.mentioned = true; }

            }
            catch(Exception excp) { Console.WriteLine("ERR: "+ excp.ToString());  }
            File.AppendAllText("./Log.json", JsonConvert.SerializeObject(msg)+Environment.NewLine);

            Console.WriteLine("-------------END MESSAGE--------------");
            return null;
        }

        private static Task onReady(ReadyEventArgs e)
        {
            Console.WriteLine("Init: Completed!");
            Console.WriteLine("DiscordObserver. By mihajlo0743");
            if (e.Client.CurrentUser != null)
            {
                Console.WriteLine("Logged in: "+e.Client.CurrentUser);
            }
            else
            {
                Console.WriteLine("ERR: Invalid token!");
            }
            Console.WriteLine("");
            return null;
        }

        private static Task onERR(ClientErrorEventArgs e)
        {
            Console.WriteLine("Exception: " + e.Exception);
            Console.WriteLine("Name: " + e.EventName);
            return null;
        }
        private static Task onSerr(SocketErrorEventArgs e)
        {
            Console.WriteLine("Exception: " + e.Exception);
            return null;
        }
    }
}
