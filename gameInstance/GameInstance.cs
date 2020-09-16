using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using NATS.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
namespace gameInstance
{

    public class GameInstance
    {

        public string url = "http://localhost:42096";
        public Object testLock = new Object();

        public List<GameEvent> PlayMove(GameEvent ev)
        {

            List<GameEvent> eventList = new List<GameEvent>();



            return eventList;
        }
        public void InitUsersAndGame(ISyncSubscription s, IConnection c, string subject)
        {

            Msg m;
            int init = 0;

            System.Threading.Thread.Sleep(1000);
            c.Publish(subject, subject + ".server", Encoding.UTF8.GetBytes("introduce"));
            while (init < 2)
            {

                m = s.NextMessage();

                Console.WriteLine("recived init user: " + m);
                User usr = JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(m.Data));
               
                if (State.Instance.playerOne == null)
                {
                    State.Instance.playerOne = usr;
                    this.FetchDeck(State.Instance.playerOne.ActiveDeck.CardList, true);
                    lock(this.testLock){
                        Monitor.Wait(testLock);
                    }
                     State.Instance.ShuffleOne();
                     State.Instance.drawCardOne(2);
                    init++;
                }
                else
                {
                    State.Instance.playerTwo = usr;
                    this.FetchDeck(State.Instance.playerTwo.ActiveDeck.CardList, false);
                    lock(this.testLock){
                        Monitor.Wait(this.testLock);
                    }
                    State.Instance.ShuffleTwo();
                    State.Instance.drawCardTwo(2);
                    init++;
                }
            }
            init = 0;
            while (init < 2)
            {
                State.Instance.WhoGoesFirst();
                c.Publish(subject, subject + ".server", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(State.Instance.CompressState())));
                c.Flush();
                m = s.NextMessage();
                if (Encoding.UTF8.GetString(m.Data) == "initilised")
                    init++;
            }
        }
        public void PlaySpellCard(Card card, int target ){
            Type t = this.GetType();
            MethodInfo method = t.GetMethod(card.Name.ToString());
            method.Invoke(this, null);
        }
        #region Spelke
            public void ArcaneExplosion(){
                if(State.Instance.plOnMove == State.Instance.playerOne.IdUser){
                    foreach (Card item in State.Instance.playerTwoTable)
                    {
                        item.Health -= 2;
                    }
                }
                else{
                    foreach (Card item in State.Instance.playerOneTable)
                    {
                        item.Health -= 2;
                    }
                }
            }
        #endregion
        async void FetchDeck(string cardList, bool p1)
        {
            State.Instance.playerOneCards =  new Dictionary<int, Card>();
            State.Instance.playerTwoCards =  new Dictionary<int, Card>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                                                  
                                string[] splitIdS = cardList.Split(" ");
                                string cardGet;
                                List<Card> deck = new List<Card>();
                                foreach (string idD in splitIdS)
                                {
                                    cardGet = $"{url}/api/Cards/{idD}";
                                    using (HttpResponseMessage responce = await client.GetAsync(cardGet))
                                    {
                                        using (HttpContent contentData = responce.Content)
                                        {
                                            string cardJson = await contentData.ReadAsStringAsync();
                                            Card card = JsonConvert.DeserializeObject<Card>(cardJson);
                                            if (card != null)
                                            {
                                                if (p1)
                                                {
                                                    State.Instance.playerOneDeck.Add(card);
                                                    if (!State.Instance.playerOneCards.ContainsKey(card.IdCard))
                                                    { 
                                                        State.Instance.playerOneCards.Add(card.IdCard, card);
                                                    }
                                                }
                                                else
                                                {
                                                    State.Instance.playerTwoDeck.Add(card);
                                                    if (!State.Instance.playerTwoCards.ContainsKey(card.IdCard))
                                                    { 
                                                        State.Instance.playerTwoCards.Add(card.IdCard, card);
                                                    }
                                                }
                                                
                                               
                                            }
                                        }
                                    }
                                }
                                lock (this.testLock)
                                {
                                    Monitor.Pulse(this.testLock);
                                }

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

    }

}