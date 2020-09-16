using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using NATS.Client;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
namespace subscribeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> matchUsers = new List<User>();
            Object testLock = new Object();
            Options opts = ConnectionFactory.GetDefaultOptions();
            ProcessStartInfo start;
            bool found;
            long matchId = 0;
            opts.Url = "nats://demo.nats.io:4222";
            opts.Secure = false;
            using (IConnection c = new ConnectionFactory().CreateConnection(opts))
            {

                // elapsed = receiveAsyncSubscriber(c);
                #region asyncConection
                // EventHandler<MsgHandlerEventArgs> msgHandler = (sender, args) =>
                // {
                //     Console.WriteLine("Received: " + System.Text.Encoding.UTF8.GetString(args.Message.Data));
                //     User recUser = JsonConvert.DeserializeObject<User>(System.Text.Encoding.UTF8.GetString(args.Message.Data));

                //     var msg = args.Message;
                //     found = false;
                //     foreach (var user in matchUsers)
                //     {
                //         if (recUser.WinNo + 20 > user.WinNo && recUser.WinNo - 15 < user.WinNo)
                //         {

                //             Match match = new Match(recUser, user, "null", matchId);//TODO: url brokera preko kog se razmenjuju poruke


                //             c.Publish(recUser.Rank, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(match)));
                //             c.Flush();

                //             matchUsers.Remove(user);
                //             found = true;

                //             start = new ProcessStartInfo();

                //             start.Arguments = matchId.ToString() + " null";
                //             //TODO: url brokera preko kog se razmenjuju poruke

                //             start.FileName = @"D:\ELFAK\IV godina\VII Semestar\Arhitektura I Projektovanje Softvera\Project\gameInstance\bin\Debug\netcoreapp3.1\gameInstance.exe";

                //             start.WindowStyle = ProcessWindowStyle.Hidden;
                //             start.CreateNoWindow = false;

                //             using (Process proc = Process.Start(start)) { }

                //             if (++matchId > 500000)
                //                 matchId = 0;

                //             break;
                //         }
                //     }
                //     if (!found)
                //     {
                //         matchUsers.Add(recUser);
                //     }
                //     // lock (testLock)
                //     // {
                //     //     Monitor.Pulse(testLock);
                //     // }
                //};
                #endregion

                using (ISyncSubscription s = c.SubscribeSync("matchs"))
                {
                    // just wait until we are done.
                    Msg m = new Msg();

                    while (true)
                    {

                        m = s.NextMessage();
                        Console.WriteLine("Received: " + System.Text.Encoding.UTF8.GetString(m.Data));
                        User recUser = JsonConvert.DeserializeObject<User>(System.Text.Encoding.UTF8.GetString(m.Data));

                        var msg = m;
                        found = false;
                        foreach (var user in matchUsers)
                        {
                            if (recUser.MMR + 20 > user.MMR && recUser.MMR - 15 < user.MMR)
                            {

                                Match match = new Match(recUser, user, "null", matchId);//TODO: url brokera preko kog se razmenjuju poruke


                                c.Publish(recUser.Region, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(match)));
                                c.Flush();

                                matchUsers.Remove(user);
                                found = true;

                                start = new ProcessStartInfo();

                                start.Arguments = matchId.ToString() + " null";
                                //TODO: url brokera preko kog se razmenjuju poruke

                                start.FileName = @"D:\ELFAK\IV godina\VII Semestar\Arhitektura I Projektovanje Softvera\Project\gameInstance\bin\Debug\netcoreapp3.1\gameInstance.exe";

                                start.WindowStyle = ProcessWindowStyle.Hidden;
                                start.CreateNoWindow = false;

                                using (Process proc = Process.Start(start)) { }

                                if (++matchId > 500000)
                                    matchId = 0;

                                break;
                            }
                        }
                        if (!found)
                        {
                            matchUsers.Add(recUser);
                        }
                    }
                    // lock (testLock)
                    // {
                    //     Monitor.Pulse(testLock);
                    // }

                }


                //Console.Write("Received {0} msgs in {1} seconds ", received, elapsed.TotalSeconds);
            }
        }
    }
}
