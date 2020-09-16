using System;
using System.Collections.Generic;

namespace publishTest
{
    public class State
    {
        public int plOnMove;
        public int firstMovePl;
        public int turn;
        public User playerOne = null;
        public User playerTwo = null;
        public User PlayerOne{get; set;}
        public User PlayerTwo{get; set;}

        public int playerOneLifePoints;
        public int playerTwoLifePoints;
        public int playerGems;
        public int playerCrystals;

        public List<Card> playerOneHand;
        public List<Card> playerTwoHand;

        public List<Card> playerOneTable;
        public List<Card> playerTwoTable;

        public List<Card> playerOneDeck;
        public List<Card> playerTwoDeck;

        private static State instance = null;
        public static State Instance{ get {
            if(instance == null){
                instance = new State();
                return instance;
            }
            return instance;
        }}
        private State()
        {
            this.turn = 0;
            this.playerCrystals = 8;
            this.playerGems = 0;
            this.playerOneLifePoints = 35;
            this.playerTwoLifePoints = 35;
           
            this.playerOneHand = new List<Card>(); 
            this.playerTwoHand = new List<Card>();
                 
            this.playerOneTable = new List<Card>();
            this.playerTwoTable = new List<Card>();
                   
            this.playerOneDeck = new List<Card>();
            this.playerTwoDeck = new List<Card>();
        }
        public void UncompressState(CompState state, Dictionary<int,Card> myDeck,Dictionary<int,Card> oponentDeck, User me, User oponent){

                this.playerOneLifePoints = state.P1Lp ;
                this.playerTwoLifePoints = state.P2Lp ;
                this.playerCrystals = state.PlayerCrystals;
                this.playerGems = state.PlayerGems;
                this.firstMovePl = state.GoesFirst;
                this.plOnMove = state.OnMove;

                if(state.P1 == me.IdUser){
                    this.PlayerOne = me;
                    foreach( int cardId in state.P1Hand )
                    {
                        Card value;
                        myDeck.TryGetValue(cardId, out value);
                        instance.playerOneHand.Add(value);
                    }
                    this.PlayerTwo = oponent;
                    foreach( CompCard card in state.P1Table )
                    {
                        Card value;
                        myDeck.TryGetValue(card.IdCard, out value);
                        value.Attack = card.Attack;
                        value.Health = card.Health;
                        value.Shield = card.Shield;
                        instance.playerOneHand.Add(value);
                    }
                    foreach( CompCard card in state.P2Table )
                    {
                        Card value;
                        oponentDeck.TryGetValue(card.IdCard, out value);
                        value.Attack = card.Attack;
                        value.Health = card.Health;
                        value.Shield = card.Shield;
                        instance.playerTwoHand.Add(value);
                    }
                }
                else{
                    this.PlayerTwo = me;
                    foreach( int cardId in state.P2Hand )
                    {
                        Card value;
                        myDeck.TryGetValue(cardId, out value);
                        instance.playerTwoHand.Add(value);
                    }
                    this.PlayerOne = oponent;
                    foreach( CompCard card in state.P1Table )
                    {
                        Card value;
                        oponentDeck.TryGetValue(card.IdCard, out value);
                        value.Attack = card.Attack;
                        value.Health = card.Health;
                        value.Shield = card.Shield;
                        instance.playerOneHand.Add(value);
                    }
                    foreach( CompCard card in state.P2Table )
                    {
                        Card value;
                        myDeck.TryGetValue(card.IdCard, out value);
                        value.Attack = card.Attack;
                        value.Health = card.Health;
                        value.Shield = card.Shield;
                        instance.playerTwoHand.Add(value);
                    }
                }
                

                
        }
        public CompState CompressState(){

            CompState sendState = new CompState{
                P1 = instance.playerOne.IdUser,
                P2 = instance.playerTwo.IdUser,
                P1Lp = instance.playerOneLifePoints,
                P2Lp = instance.playerTwoLifePoints,
                PlayerCrystals = instance.playerCrystals,
                PlayerGems = instance.playerGems,
                GoesFirst = instance.firstMovePl,
                OnMove = instance.plOnMove,
                P1Hand = new List<int>(),
                P2Hand = new List<int>(),
                P1Table = new List<CompCard>(),
                P2Table = new List<CompCard>(),
            };
            foreach(Card card in instance.playerOneHand){
                sendState.P1Hand.Add(card.IdCard);
            }
            foreach(Card card in instance.playerTwoHand){
                sendState.P2Hand.Add(card.IdCard);
            }
            foreach(Card card in instance.playerOneTable){
                sendState.P1Table.Add(new CompCard(card.IdCard ,card.Name ,card.Attack , card.Health , card.Shield));
            }
            foreach(Card card in instance.playerTwoTable){
                sendState.P2Table.Add(new CompCard(card.IdCard ,card.Name ,card.Attack , card.Health , card.Shield));
            }
            return sendState;
        }
        public void InitDeckOne(List<Card> karte)
        {
            instance.playerOneDeck.AddRange(karte);
        }

        public void InitDeckTwo(List<Card> karte)
        {
            instance.playerTwoDeck.AddRange(karte);
        }

        public void drawCardOne(int nmb)
        {
            instance.playerOneHand.AddRange(instance.playerOneDeck.GetRange(0, nmb));
            instance.playerOneDeck.RemoveRange(0, nmb);
        }

        public void drawCardTwo(int nmb)
        {
            instance.playerTwoHand.AddRange(instance.playerTwoDeck.GetRange(0, nmb));
            instance.playerTwoDeck.RemoveRange(0, nmb);
        }

        public List<Card> Shuffle(List<Card> lsc)
        {
            Random rnd = new Random();
            var count = lsc.Count;
            var last = count - 1;
            for(var i = 0; i< last; i++)
            {
                var r = rnd.Next(i, count);
                var tmp = lsc[i];
                lsc[i] = lsc[r];
                lsc[r] = tmp;
            }
            return lsc;
        }

        public void ShuffleOne()
        {
            instance.playerOneDeck = Shuffle(instance.playerOneDeck);
        }

        public void ShuffleTwo()
        {
            instance.playerTwoDeck = Shuffle(instance.playerTwoDeck);
        }

        public void AddCardToHandOne(Card card)
        {
            instance.playerOneHand.Add(card);
        }

        public void AddCardToHandTwo(Card card)
        {
            instance.playerTwoHand.Add(card);
        }

        public void DamageLifePointsOne(int n)
        {
            instance.playerOneLifePoints -= n;
        }

        public void DamageLifePointsTwo(int n)
        {
            instance.playerTwoLifePoints -= n;
        }

        public void HealLifePointsOne(int n)
        {
            instance.playerOneLifePoints += n;
        }

        public void HealLifePointsTwo(int n)
        {
            instance.playerTwoLifePoints += n;
        }

        public void AddCardToTableOne(Card card)
        {
            instance.playerOneTable.Add(card);
        }

        public void AddCardToTableTwo(Card card)
        {
            instance.playerTwoTable.Add(card);
        }
        public void WhoGoesFirst(){

            Random coinTos = new Random();
            if(coinTos.Next(100) > 50){
                instance.firstMovePl = instance.playerOne.IdUser;
                instance.plOnMove = instance.playerOne.IdUser;
            }
            else{
                instance.firstMovePl = instance.playerTwo.IdUser;
                instance.plOnMove = instance.playerTwo.IdUser;
            }
        }
    }
}
