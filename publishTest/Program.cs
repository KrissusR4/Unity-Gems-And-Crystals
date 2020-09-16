using System;
using NATS.Client;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace publishTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region za svaki slucaj da ima
                
            // byte[] payload = null;
            // Options opts = ConnectionFactory.GetDefaultOptions();
            // Object testLock = new Object();
            // Msg replyMsg = new Msg();

            // User me = new User(args[1], int.Parse(args[2]), 3, args[0]);
            // string send = JsonConvert.SerializeObject(me);
            // payload = Encoding.UTF8.GetBytes(send);

            // using (IConnection c = new ConnectionFactory().CreateConnection(opts))
            // {

            //     EventHandler<MsgHandlerEventArgs> msgHandler = (sender, args) =>
            //     {
            //         Match match = JsonConvert.DeserializeObject<Match>(Encoding.UTF8.GetString(args.Message.Data));

            //         if (match.playerOne.name == me.name)
            //         {

            //             Console.WriteLine("Received oponent: " + match.playerTwo.name);
            //             Console.WriteLine("Playing on server: " + match.gameServerUrl);
            //             Console.WriteLine("Sending on: game" + match.gameID);

            //             c.Close();
            //             if (match.gameServerUrl == "null")
            //                 opts = ConnectionFactory.GetDefaultOptions();
            //             else
            //                 opts.Url = match.gameServerUrl;
            //             using (IConnection gameCon = new ConnectionFactory().CreateConnection(opts))
            //             {
            //                 gameCon.Publish( "game"+match.gameID+".server",Encoding.UTF8.GetBytes("1"));
            //                 using (ISyncSubscription s = gameCon.SubscribeSync("game" + match.gameID.ToString()))
            //                 {
            //                     bool init = false;
            //                     Msg m;
            //                     while (init == false)
            //                     {

            //                         m = s.NextMessage();

            //                         //Console.WriteLine("Received: " + m);

            //                         if (System.Text.Encoding.UTF8.GetString(m.Data) == "introduce")
            //                         {
            //                             replyMsg.Data = payload;
            //                         }
            //                         else
            //                         {
            //                             Console.WriteLine("recived init state: " + m);
            //                             replyMsg.Data = Encoding.UTF8.GetBytes("initilised");
            //                             init = true;
            //                         }
            //                         replyMsg.Subject = m.Reply;
            //                         gameCon.Publish(replyMsg);

            //                     }
            //                     s.Unsubscribe();
            //                     s.Dispose();
            //                 }

            //                 lock (testLock)
            //                 {
            //                     Monitor.Pulse(testLock);
            //                 }
            //             }
            //         }
            //         if (match.playerTwo.name == me.name)
            //         {

            //             Console.WriteLine("Received oponent: " + match.playerOne.name);
            //             Console.WriteLine("Playing on server: " + match.gameServerUrl);
            //             Console.WriteLine("Sending on: game" + match.gameID);
            //             c.Close();
            //             if (match.gameServerUrl == "null")
            //                 opts = ConnectionFactory.GetDefaultOptions();
            //             else
            //                 opts.Url = match.gameServerUrl;
            //             using (IConnection gameCon = new ConnectionFactory().CreateConnection(opts))
            //             {
            //                 gameCon.Publish( "game"+match.gameID+".server",Encoding.UTF8.GetBytes("1"));
            //                 using (ISyncSubscription s = gameCon.SubscribeSync("game" + match.gameID.ToString()))
            //                 {
            //                     bool init = false;
            //                     Msg m;
            //                     while (init == false)
            //                     {

            //                         m = s.NextMessage();

            //                         //Console.WriteLine("Received: " + m);

            //                         if (System.Text.Encoding.UTF8.GetString(m.Data) == "introduce")
            //                         {
            //                             replyMsg.Data = payload;
            //                         }
            //                         else
            //                         {
            //                             Console.WriteLine("recived init state: " + m);
            //                             replyMsg.Data = Encoding.UTF8.GetBytes("initilised");
            //                             init = true;
            //                         }
            //                         replyMsg.Subject = m.Reply;
            //                         gameCon.Publish(replyMsg);

            //                     }
            //                     s.Unsubscribe();
            //                     s.Dispose();
            //                 }

            //                 lock (testLock)
            //                 {
            //                     Monitor.Pulse(testLock);
            //                 }
            //             }
            //         }
            //     };


            //     using (IAsyncSubscription s = c.SubscribeAsync(me.region, msgHandler))
            //     {
            //         // just wait until we are done.

            //         c.Publish("matchs", payload);

            //         c.Flush();

            //         Console.WriteLine("Published on topic matchs, msg {0} ", me);
            //         Console.WriteLine("My region is {0} , name is {1} ", args[0], args[1]);

            //         lock (testLock)
            //         {
            //             Monitor.Wait(testLock);
            //         }
            //     }
            // }


        #endregion
        
            Deck deck = new Deck{
                IdDeck = int.Parse(args[3]),
                CardList = "1 2 3 4 " + args[3],
                Name = "spil",
            };
            User me = new User{
                IdUser = int.Parse(args[3]),
                MyDecksList = "1",
                Username = args[1],
                Region = args[0],
                MMR = int.Parse(args[2]),
                ActiveDeck = deck,
            };
            Client.Instance.Me = me;
            Client.Instance.GetUserDecks(Client.Instance.Me.IdUser);
            Client.Instance.StartMatchMaking();

            // Card card = new Card{
            //     Attack = 5,
            //     Shield = 0,
            //     Health = 3
            // };
       
            //Console.WriteLine(Client.Instance.GameDeck[1].Name, "ime od kartu");
            
            //client.PlayMove(card);
        }
    }
}
