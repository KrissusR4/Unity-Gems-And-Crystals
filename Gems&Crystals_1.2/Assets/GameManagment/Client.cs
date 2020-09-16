using System;
using NATS.Client;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using gameInstance;

namespace publishTest
{

    public class Client
    {

        private User me;
        private User oponent;
        private List<Card> ownedCards;
        private List<Deck> myDecks;
        private Dictionary<int, Card> gameDeck;
        private Dictionary<int, Card> opGameDeck;
        private Match match;
        private CompState curentState;
        private System.Object testLock;
        private IConnection gameConnection;
        private ISyncSubscription gameSub;
        private string gameSubject;
        public string url = "http://localhost:42096";

        public User Me { get => me; set => me = value; }
        public List<Card> OwnedCards { get => ownedCards; set => ownedCards = value; }
        public Match Match { get => match; set => match = value; }
        public CompState CurentState { get => curentState; set => curentState = value; }
        public List<Deck> MyDecks { get => myDecks; set => myDecks = value; }

        private static Client instance = null;
        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                    return instance;
                }
                return instance;
            }
        }

        public User Oponent { get => oponent; set => oponent = value; }
        public Dictionary<int, Card> GameDeck { get => gameDeck; set => gameDeck = value; }
        public Dictionary<int, Card> OpGameDeck { get => opGameDeck; set => opGameDeck = value; }

        private Client()
        {
            this.GameDeck = new Dictionary<int, Card>();
            this.OpGameDeck = new Dictionary<int, Card>();
            this.CurentState = new CompState();
            this.MyDecks = new List<Deck>();
            this.OwnedCards = new List<Card>();
            this.Me = null;
            this.testLock = new System.Object();
        }

        public void StartMatchMaking()
        {
            Options opts = ConnectionFactory.GetDefaultOptions();
            opts.Url = "nats://demo.nats.io:4222";
            opts.Secure = false;
            string send = JsonConvert.SerializeObject(this.Me);
            byte[] payload = Encoding.UTF8.GetBytes(send);

            using (IConnection c = new ConnectionFactory().CreateConnection(opts))
            {

                EventHandler<MsgHandlerEventArgs> msgHandler = (sender, args) =>
                {
                    this.Match = JsonConvert.DeserializeObject<Match>(Encoding.UTF8.GetString(args.Message.Data));

                    if (Match.playerOne.Username == Me.Username)
                    {

                        Debug.Log("Received oponent: " + Match.playerTwo.Username);
                        Debug.Log("Playing on server: " + Match.gameServerUrl);
                        Debug.Log("Sending on: game" + Match.gameID);
                        Debug.Log("oponent deck" + Match.playerTwo.ActiveDeck.CardList);
                        Client.Instance.oponent = match.playerTwo;
                        this.GetGameDecks();
                        c.Close();
                        this.InitGame();
                    }
                    if (Match.playerTwo.Username == Me.Username)
                    {

                        Debug.Log("Received oponent: " + Match.playerOne.Username);
                        Debug.Log("Playing on server: " + Match.gameServerUrl);
                        Debug.Log("Sending on: game" + Match.gameID);
                        Debug.Log("oponent deck" + Match.playerOne.ActiveDeck.CardList);
                        Client.Instance.oponent = match.playerOne;
                        this.GetGameDecks();
                        Debug.Log("my card" + Client.instance.gameDeck[4].Name);
                        Debug.Log("oponent card" + Client.instance.opGameDeck[4].Name);

                        c.Close();
                        this.InitGame();
                    }
                };


                    c.Publish("matchs", payload);

                    Debug.Log("Published on topic matchs, msg {0} " + " " + Me);
                    Debug.Log("My region is {0} , name is {1} " +  Me.Region + " " +Me.Username);
                    Debug.Log("deck list:" + Me.ActiveDeck.CardList);
                using (ISyncSubscription s = c.SubscribeSync(Me.Region))
                {
                    
                    // just wait until we are done.


                    Msg m = s.NextMessage();

                    this.Match = JsonConvert.DeserializeObject<Match>(Encoding.UTF8.GetString(m.Data));

                    if (Match.playerOne.Username == Me.Username)
                    {

                        Debug.Log("Received oponent: " + Match.playerTwo.Username);
                        Debug.Log("Playing on server: " + Match.gameServerUrl);
                        Debug.Log("Sending on: game" + Match.gameID);
                        Debug.Log("oponent deck" + Match.playerTwo.ActiveDeck.CardList);
                        Client.Instance.oponent = match.playerTwo;
                        this.GetGameDecks();
                        c.Close();
                        this.InitGame();
                    }
                    if (Match.playerTwo.Username == Me.Username)
                    {

                        Debug.Log("Received oponent: " + Match.playerOne.Username);
                        Debug.Log("Playing on server: " + Match.gameServerUrl);
                        Debug.Log("Sending on: game" + Match.gameID);
                        Debug.Log("oponent deck" + Match.playerOne.ActiveDeck.CardList);
                        Client.Instance.oponent = match.playerOne;
                        this.GetGameDecks();
                        Debug.Log("my card" + Client.instance.gameDeck[4].Name);
                        Debug.Log("oponent card" + Client.instance.opGameDeck[4].Name);

                        c.Close();
                        this.InitGame();
                    }
                }
            }

        }

        public void InitGame()
        {

            Options opts = ConnectionFactory.GetDefaultOptions();
            opts.Url = "nats://demo.nats.io:4222";
            opts.Secure = false;
            string send = JsonConvert.SerializeObject(Me);
            byte[] payload = Encoding.UTF8.GetBytes(send);
            Msg replyMsg = new Msg();

            if (Match.gameServerUrl == "null")
            {
                opts = ConnectionFactory.GetDefaultOptions();
                opts.Url = "nats://demo.nats.io:4222";
                opts.Secure = false;
            }
            else
                opts.Url = Match.gameServerUrl;
            try
            {


                IConnection gameCon = new ConnectionFactory().CreateConnection(opts);

                ISyncSubscription s = gameCon.SubscribeSync("game" + Match.gameID.ToString());

                this.gameConnection = gameCon;
                this.gameSub = s;
                this.gameSubject = "game" + Match.gameID.ToString();
                bool init = false;
                Msg m;
                while (init == false)
                {

                    m = s.NextMessage();

                    //Debug.Log("Received: " + m);

                    if (System.Text.Encoding.UTF8.GetString(m.Data) == "introduce")
                    {
                        replyMsg.Data = payload;
                    }
                    else
                    {
                        Debug.Log("recived init state: " + m);
                        this.CurentState = JsonConvert.DeserializeObject<CompState>(Encoding.UTF8.GetString(m.Data));
                        State.Instance.turn = 0;
                        State.Instance.UncompressState(this.curentState, this.gameDeck, this.opGameDeck, this.me, this.oponent);
                        //Debug.Log("my hand is: " + this.CurentState.P1Hand[1]); //proveri dal se lepo CompCard preslikalo
                        replyMsg.Data = Encoding.UTF8.GetBytes("initilised");

                        foreach (Card card in State.Instance.playerOneHand)
                        {
                            Debug.Log(card.IdCard + " " + card.Description + " " + card.Name + card.Attack + " \n");
                        }

                        foreach (Card card in State.Instance.playerOneDeck)
                        {
                            Debug.Log(card.IdCard + " " + card.Description + " " + card.Name + card.Attack + " \n");
                        }

                        init = true;
                        
                    }
                    replyMsg.Subject = m.Reply;
                    gameCon.Publish(replyMsg);
                    lock (testLock)
                    {
                        Monitor.Pulse(testLock);
                    }
                }


            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }


        }
        public void GetGameDecks()
        {

            string[] splitIdS = instance.me.ActiveDeck.CardList.Split(' ');
            Client.instance.gameDeck = new Dictionary<int, Card>();
            Card card;
            foreach (string idC in splitIdS)
            {
                //Debug.Log("id karte" + idC);
                string baseUrl = $"{url}/api/Cards/{idC}";
                HttpWebRequest request = WebRequest.CreateHttp(baseUrl);
                request.Method = "GET";
                card = new Card();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        // Get a reader capable of reading the response stream
                        using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            // Read stream content as string
                            string responseJSON = myStreamReader.ReadToEnd() ;
                            //Debug.Log(responseJSON);
                            // Assuming the response is in JSON format, deserialize it
                            // creating an instance of TData type (generic type declared before).
                            card = JsonConvert.DeserializeObject<Card>(responseJSON);
                            
                            if (!Client.instance.gameDeck.ContainsKey(card.IdCard))
                            { 
                                Client.instance.gameDeck.Add(card.IdCard, card);
                            }
                            
                        }
                    }
                }
            }
            
            splitIdS = instance.oponent.ActiveDeck.CardList.Split(' ');
            Client.instance.opGameDeck = new Dictionary<int, Card>();
            
            foreach (string idC in splitIdS)
            {
                string baseUrl =  $"{url}/api/Cards/{idC}";
                HttpWebRequest request = WebRequest.CreateHttp(baseUrl);
                request.Method = "GET";
                card = new Card();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        // Get a reader capable of reading the response stream
                        using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            // Read stream content as string
                            string responseJSON = myStreamReader.ReadToEnd();

                            // Assuming the response is in JSON format, deserialize it
                            // creating an instance of TData type (generic type declared before).
                            card = JsonConvert.DeserializeObject<Card>(responseJSON);
                            if (!Client.instance.opGameDeck.ContainsKey(card.IdCard))
                            {
                                Client.instance.opGameDeck.Add(card.IdCard, card); 
                            }
                           
                        }
                    }
                }
            }
        }
        public void GetUserCards()
        {

            string[] splitIdS = instance.Me.OwnedCardsList.Split(' ');
            List<Card> cards = new List<Card>();
            Client.instance.OwnedCards = new List<Card>();
            string cardGet;
            foreach (string idD in splitIdS)
            {
                cardGet = $"{url}/api/Cards/{idD}";

                HttpWebRequest request = WebRequest.CreateHttp(cardGet);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        // Get a reader capable of reading the response stream
                        using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            // Read stream content as string
                            string responseJSON = myStreamReader.ReadToEnd();
                            Debug.Log(responseJSON);

                            // Assuming the response is in JSON format, deserialize it
                            // creating an instance of TData type (generic type declared before).
                            cards.Add(JsonConvert.DeserializeObject<Card>(responseJSON));
                        }
                    }
                }

            }


            this.OwnedCards.AddRange(cards);
        }
        public void PostUserCard(int idCard)
        {
            string baseURL = $"{url}/api/Users/{Me.IdUser}";
            try
            {
                User putUser = Me;
                putUser.OwnedCardsList = putUser.OwnedCardsList + " " + idCard;
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(putUser), Encoding.UTF8, "application/json");
                    client.PutAsync(baseURL, content);
                }
            }
            catch (Exception exception)
            {
                Debug.Log(exception);
            }
        }
        public void GetUserDecks(int userId)
        {
            string[] splitIdS = Client.instance.Me.MyDecksList.Split(' ');
            List<Deck> decks = new List<Deck>();
            Client.Instance.MyDecks = new List<Deck>();
            Debug.Log(Client.instance.Me.MyDecksList.Length);
            if (Client.instance.Me.MyDecksList.Length == 2)
            {
                string deckGet = $"{url}/api/Decks/{Client.instance.Me.MyDecksList[1]}";
                Debug.Log(url+"/api/Decks/" + Client.instance.Me.MyDecksList[1]);
                HttpWebRequest request = WebRequest.CreateHttp(deckGet);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        // Get a reader capable of reading the response stream
                        using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            // Read stream content as string
                            string responseJSON = myStreamReader.ReadToEnd();
                            Debug.Log(responseJSON);
                            // Assuming the response is in JSON format, deserialize it
                            // creating an instance of TData type (generic type declared before).
                            decks.Add(JsonConvert.DeserializeObject<Deck>(responseJSON));
                        }
                    }
                }
            }
            else
            {


                foreach (string idD in splitIdS)
                {
                    Debug.Log(idD);
                    string deckGet = $"{url}/api/Decks/{idD}";
                    HttpWebRequest request = WebRequest.CreateHttp(deckGet);
                    request.Method = "GET";

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            // Get a reader capable of reading the response stream
                            using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                            {
                                // Read stream content as string
                                string responseJSON = myStreamReader.ReadToEnd();
                                Debug.Log(responseJSON);
                                // Assuming the response is in JSON format, deserialize it
                                // creating an instance of TData type (generic type declared before).
                                decks.Add(JsonConvert.DeserializeObject<Deck>(responseJSON));
                            }
                        }
                    }
                }
            }
                Client.Instance.MyDecks.AddRange(new List<Deck>(decks));
        }

        public void PostUserDeck(Deck deck)
        {
            string baseURL = $"{url}/api/Decks";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(deck), Encoding.UTF8, "application/json");
                    client.PostAsync(baseURL, content);

                }
            }
            catch (Exception exception)
            {
                Debug.Log(exception);
            }
        }
        public void PutUser(SendUser user)
        {
            string baseURL = $"{url}/api/Users/{user.IdUser}";
            Debug.Log(user.IdUser);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    client.PutAsync(baseURL, content);
                }
            }
            catch (Exception exception)
            {
                Debug.Log(exception);
            }
        }
        public void Register(SendUser user)
        {
            string baseURL = $"{url}/api/Users";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    client.PostAsync(baseURL, content);
                }
            }
            catch (Exception exception)
            {
                Debug.Log(exception);
            }
        }
        public void Login(string username, string pass)
        {
            string baseURL = $"{url}/api/Users/Login?username={username}&password={pass}";
            HttpWebRequest request = WebRequest.CreateHttp(baseURL);
            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Get a reader capable of reading the response stream
                    using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        // Read stream content as string
                        string responseJSON = myStreamReader.ReadToEnd();
                        
                        // Assuming the response is in JSON format, deserialize it
                        // creating an instance of TData type (generic type declared before).
                        Client.instance.me = JsonConvert.DeserializeObject<User>(responseJSON+"}");
                    }
                }
            }
        }

        public void PlayMove(IGameEvent c)
        {
            try
            {

                this.gameConnection.Publish(this.gameSubject + ".server", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(c)));

                Msg m = this.gameSub.NextMessage();

                Debug.Log("answer recived");
                this.curentState = JsonConvert.DeserializeObject<CompState>(Encoding.UTF8.GetString(m.Data));

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
}