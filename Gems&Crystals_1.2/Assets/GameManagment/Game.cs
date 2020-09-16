using System;
using System.Collections.Generic;

namespace publishTest
{
    public partial class Game
    {
        public int IdGame { get; set; }
        public int IdP1 { get; set; }
        public int IdP2 { get; set; }
        public int IdP1deck { get; set; }
        public int IdP2deck { get; set; }
        public int P1deckCount { get; set; }
        public int P2deckCount { get; set; }
        public int P1lp { get; set; }
        public int P2lp { get; set; }
        public string P1tableCardsList { get; set; }
        public string P2tableCardsList { get; set; }
        public string P1handCardList { get; set; }
        public string P2handCardList { get; set; }

        public User IdP1Navigation { get; set; }
        public Deck IdP1deckNavigation { get; set; }
        public User IdP2Navigation { get; set; }
        public Deck IdP2deckNavigation { get; set; }
    }
}
