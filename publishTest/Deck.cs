using System;
using System.Collections.Generic;

namespace publishTest
{
    public class Deck
    {
        public Deck()
        {
            GameIdP1deckNavigation = new HashSet<Game>();
            GameIdP2deckNavigation = new HashSet<Game>();
        }

        public int IdDeck { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string CardList { get; set; }

        public ICollection<Game> GameIdP1deckNavigation { get; set; }
        public ICollection<Game> GameIdP2deckNavigation { get; set; }
    }
}
