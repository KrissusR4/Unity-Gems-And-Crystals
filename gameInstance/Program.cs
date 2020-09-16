using System;
using System.Text;
using System.Collections.Generic;
using NATS.Client;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace gameInstance
{
    class Program
    {
    public static string url = "https://localhost:44345";
    public static Object testLock = new Object();
       static async void FetchDeck(int id, bool p1)
        {

            string baseURL = $"{url}/api/Decks/{id}";
            
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseURL))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                var dataObj = JObject.Parse(data);
                                string idS = (string)dataObj["cardList"];
                                string[] splitIdS = idS.Split(" ");
                                string cardGet;
                                List<Card> deck = new List<Card>();
                                foreach (string idD in splitIdS)
                                {
                                    cardGet = $"{url}/api/Cards/{idD}";
                                    using (HttpResponseMessage responce = await client.GetAsync(cardGet))
                                    {
                                        using (HttpContent contentData = responce.Content)
                                        {
                                            string card = await contentData.ReadAsStringAsync();
                                            if (card != null)
                                            {
                                                deck.Add(JsonConvert.DeserializeObject<Card>(card));
                                            }
                                        }
                                    }
                                }    
                                if(p1){
                                    State.Instance.playerOneDeck.AddRange(deck);
                                }
                                else{
                                    State.Instance.playerTwoDeck.AddRange(deck);
                                }
                                lock(testLock){
                                    Monitor.Pulse(testLock);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Data is null!");
                                lock(testLock){
                                    Monitor.Pulse(testLock);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }
        static void Main(string[] args)
        {   
            GameInstance instance = new GameInstance();
            Options opts = ConnectionFactory.GetDefaultOptions();
            opts.Url = "nats://demo.nats.io:4222";
            opts.Secure = false;
            string subject = "game"+args[0];
            Console.WriteLine("Sending on: " + subject);
            Console.WriteLine("Url is: " + args[1]);
            bool gameOver = false;
            if(args[1] != "null")
                opts.Url = args[1];

            using (IConnection c = new ConnectionFactory().CreateConnection(opts))
            {

                // c.Flush();
                using (ISyncSubscription s = c.SubscribeSync(subject+".server"))
                {
                    instance.InitUsersAndGame(s,c,subject);
                    while(!gameOver){

                        			Msg m = s.NextMessage();
                        
                        GameEvent ev = JsonConvert.DeserializeObject<GameEvent>(Encoding.UTF8.GetString(m.Data));
                        List<GameEvent> sideEfects = new List<GameEvent>(instance.PlayMove(ev));
                        if(ev.Type == EvType.endTurn){
                            if(State.Instance.plOnMove == State.Instance.playerOne.IdUser){
                                foreach (Card item in State.Instance.playerTwoTable)
                                {
                                    item.Shield = State.Instance.playerTwoCards[item.IdCard].Shield;
                                }
                                State.Instance.plOnMove = State.Instance.playerTwo.IdUser;
                            }
                            else{
                                foreach (Card item in State.Instance.playerOneTable)
                                {
                                    item.Shield = State.Instance.playerOneCards[item.IdCard].Shield;
                                }
                                State.Instance.plOnMove = State.Instance.PlayerOne.IdUser;
                            }
                            if(State.Instance.turn < 3){
                                State.Instance.playerCrystals = 0;
                            }
                            else if(State.Instance.turn < 5){
                                State.Instance.playerCrystals = 1;
                            }else if(State.Instance.turn < 7){
                                State.Instance.playerCrystals = 2;
                            }else if(State.Instance.turn < 12){
                                State.Instance.playerCrystals = 3;
                            }
                            State.Instance.playerGems = 8;
                            State.Instance.turn++;
                         
                        }
                        else if(ev.Type == EvType.attack){
                            Card attacker = null;
                            Card beingAttacked  = null;;
                            if(State.Instance.plOnMove == State.Instance.playerOne.IdUser){
                                foreach (Card card in State.Instance.playerOneTable)
                                {
                                    if(card.IdCard == ev.Source)
                                        attacker = card;
                                }
                                foreach (Card card in State.Instance.playerTwoTable)
                                {
                                    if(card.IdCard == ev.Target)
                                        beingAttacked = card;
                                }
                                attacker.Shield -= beingAttacked.Attack <= attacker.Shield ? beingAttacked.Attack : attacker.Shield;
                                attacker.Health -= beingAttacked.Attack - attacker.Shield;
                                if(attacker.Health <= 0){
                                    State.Instance.playerOneTable.Remove(attacker);
                                    //TODO turi ga u groblje
                                }
                                beingAttacked.Shield -= attacker.Attack <= beingAttacked.Shield ? attacker.Attack : beingAttacked.Shield;
                                beingAttacked.Health -= attacker.Attack - beingAttacked.Shield;
                                if(beingAttacked.Health <= 0){
                                    State.Instance.playerTwoTable.Remove(beingAttacked);
                                    //TODO turi ga u groblje
                                }
                            }
                            else{
                                foreach (Card card in State.Instance.playerOneTable)
                                {
                                    if(card.IdCard == ev.Source)
                                        beingAttacked = card;
                                }
                                foreach (Card card in State.Instance.playerTwoTable)
                                {
                                    if(card.IdCard == ev.Target)
                                        attacker = card; 
                                }
                                attacker.Shield -= beingAttacked.Attack <= attacker.Shield ? beingAttacked.Attack : attacker.Shield;
                                attacker.Health -= beingAttacked.Attack - attacker.Shield;
                                if(attacker.Health <= 0){
                                    State.Instance.playerTwoTable.Remove(attacker);
                                    //TODO turi ga u groblje
                                }
                                beingAttacked.Shield -= attacker.Attack <= beingAttacked.Shield ? attacker.Attack : beingAttacked.Shield;
                                beingAttacked.Health -= attacker.Attack - beingAttacked.Shield;
                                if(beingAttacked.Health <= 0){
                                    State.Instance.playerOneTable.Remove(beingAttacked);
                                    //TODO turi ga u groblje
                                }
                            }
                        }
                        else if(ev.Type == EvType.attackPlayer){
                            Card attacker = null;
                            if(State.Instance.plOnMove == State.Instance.playerOne.IdUser){
                                foreach (Card card in State.Instance.playerOneTable)
                                {
                                    if(card.IdCard == ev.Source)
                                        attacker = card;
                                }
                                State.Instance.playerTwoLifePoints -= attacker.Attack;
                                if(State.Instance.playerTwoLifePoints <= 0)
                                {
                                    gameOver = true;
                                }
                            }
                            else{
                                foreach (Card card in State.Instance.playerTwoTable)
                                {
                                    if(card.IdCard == ev.Source)
                                        attacker = card;
                                }
                                State.Instance.playerOneLifePoints -= attacker.Attack;
                                if(State.Instance.playerOneLifePoints <= 0)
                                {
                                    gameOver = true;
                                }
                            }
                        }
                        else if(ev.Type == EvType.surender){
                            if(State.Instance.plOnMove == State.Instance.playerOne.IdUser){
                                State.Instance.playerOneLifePoints = 0;
                            }
                            else{
                                State.Instance.playerTwoLifePoints = 0;
                            }
                                gameOver = true;
                        }
                        else if(ev.Type == EvType.play){
                            
                            if(ev.Source == State.Instance.plOnMove){
                                if(State.Instance.plOnMove == State.Instance.PlayerOne.IdUser){
                                    Card playd = new Card();
                                    foreach (var item in State.Instance.playerOneHand)
                                    {
                                        if(item.IdCard == ev.Target){
                                            playd = item;
                                            break;
                                        }
                                    }
                                    if(State.Instance.playerGems >= playd.GemsCost && State.Instance.playerCrystals >= playd.CrystalsCost){
                                        State.Instance.playerOneTable.Add(playd);
                                        State.Instance.playerOneHand.Remove(playd);
                                    }
                                    
                                }
                                else{
                                    Card playd = new Card();
                                    foreach (var item in State.Instance.playerTwoHand)
                                    {
                                        if(item.IdCard == ev.Target){
                                            playd = item;
                                            break;
                                        }
                                    }
                                    if(State.Instance.playerGems >= playd.GemsCost && State.Instance.playerCrystals >= playd.CrystalsCost){
                                        State.Instance.playerTwoTable.Add(playd);
                                        State.Instance.playerTwoHand.Remove(playd);
                                    }
                                }
                            }
                        }
                        else if(ev.Type == EvType.spell){
                            Card spellCard;
                            if(State.Instance.plOnMove == State.Instance.playerOne.IdUser){
                                spellCard = State.Instance.playerOneCards[ev.Source];
                            }
                            else{
                                spellCard = State.Instance.playerTwoCards[ev.Source];
                            }
                            instance.PlaySpellCard(spellCard, ev.Target);
                            
                        }
                        //TODO dodaj obradu poruka/eventa
                        //ako je event tipa surender gameOver = true
                        //poslati poruku ko je pobednik
                        Console.WriteLine("original msg: "+ m);
                        // Console.WriteLine("recived move: "+ Encoding.UTF8.GetString(m.Data));
                        c.Publish(subject , subject+".server" ,Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(State.Instance.CompressState())));

                        //TODO: provera da li neki player ima LP < 0
                    }

                        //u stanju pise ko je na potezu i to seproverava u unity-u
                        //dodati kod za updatovanje pobeda/gubitka korisnika u bazi i mmr-a
                }
            }
        }
    }
}
