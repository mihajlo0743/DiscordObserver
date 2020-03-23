using DSharpPlus;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
                try
                {
                    File.WriteAllText("./config.json", JsonConvert.SerializeObject(token));
                }
                catch(Exception e) { 
                    Console.WriteLine("ERR: Cant write file(./config.json). " + e.Message);
                    Console.WriteLine("Your token wount be saved");
                }
            }
            Console.Clear();
            try
            {
                config.Token = JsonConvert.DeserializeObject(File.ReadAllText("./config.json")).ToString();
            }
            catch(Exception e) {
                Console.WriteLine("ERR: Cant read file(./config.json). " + e.Message);
            }
            Console.WriteLine("Initializing client...");
            client = new DiscordClient(config);
            client.Ready += onReady;
            client.ClientErrored += onERR;
            client.ConnectAsync();

            ConsoleListener.ConsoleHandler();
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
                try
                {
                    Console.WriteLine("Channel: " + e.Message.Channel.Name + " | " + e.Message.Channel.Guild.Name);
                    msg.channel = e.Message.Channel.Name;
                    msg.server = e.Message.Channel.Guild.Name;
                }
                catch { }    //Exception appears when hanled direct message
                try
                {
                    Console.WriteLine("By: " + e.Message.Author.Username);
                    msg.Author = e.Message.Author.Username;
                }
                //Some wierd bug when author name is null
                catch { Console.WriteLine("Author name error(null)"); }
                Console.WriteLine("Message: " + e.Message.Content);

                msg.message = e.Message.Content;
                Console.WriteLine("At: " + e.Message.CreationTimestamp.UtcDateTime.ToString() + " - " + DateTime.Now.ToString());
                msg.C_time = e.Message.CreationTimestamp.UtcDateTime; msg.R_time = DateTime.Today;
                if (e.Message.MentionedUsers.Contains(client.CurrentUser)) { Console.WriteLine("Mentioned!"); msg.mentioned = true; }
            }
            catch (Exception excp) { Console.WriteLine("ERR: " + excp.Message); }
            File.AppendAllText("./Log.json", JsonConvert.SerializeObject(msg) + Environment.NewLine);

            Console.WriteLine("-------------END MESSAGE--------------");
            return Task.CompletedTask;
        }

        private static Task onReady(ReadyEventArgs e)
        {
            client.SocketErrored += onSerr;
            Console.WriteLine("Init: Completed!");
            Console.WriteLine("DiscordObserver. By mihajlo0743");
            if (e.Client.CurrentUser != null)
            {
                Console.WriteLine("Logged in: " + e.Client.CurrentUser);
            }
            else
            {
                Console.WriteLine("ERR: Invalid token!");
            }
            Console.WriteLine("");
            client.MessageDeleted += MsgLogger;
            return Task.CompletedTask;
        }

        private static Task onERR(ClientErrorEventArgs e)
        {
            Console.WriteLine("!-------------------------------!");
            Console.WriteLine(" Exception: ClientError" /*+ e.Exception*/);
            Console.WriteLine("Name: " + e.EventName);
            Console.WriteLine("!-------------------------------!");
            return Task.CompletedTask;
        }
        private static Task onSerr(SocketErrorEventArgs e)
        {
            Console.WriteLine("!-------------------------------!");
            Console.WriteLine(" Exception: SoketError" /*+ e.Exception*/);
            Console.WriteLine("!-------------------------------!");
            return Task.CompletedTask;
        }
    }
}
