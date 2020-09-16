using System;
using System.Collections.Generic;

namespace publishTest 
{
    public partial class User
    {
        public User()
        {
            GameIdP1Navigation = new HashSet<Game>();
            GameIdP2Navigation = new HashSet<Game>();
        }

        public int IdUser { get; set; }
        public Deck ActiveDeck {get; set;}
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public int WinNo { get; set; }
        public int LossNo { get; set; }
        public string Rank { get; set; }
        public int MMR { get; set; }
        public string Region { get; set; }
        public string MyDecksList { get; set; }
        public string OwnedCardsList { get; set; }
        public User IdUserNavigation { get; set; }
        public User InverseIdUserNavigation { get; set; }
        public ICollection<Game> GameIdP1Navigation { get; set; }
        public ICollection<Game> GameIdP2Navigation { get; set; }

    }
        public partial class SendUser
        {
            public SendUser()
            {
                GameIdP1Navigation = new HashSet<Game>();
                GameIdP2Navigation = new HashSet<Game>();
            }

            public int IdUser { get; set; }
            // public Deck? ActiveDeck {get; set;}
            public string Username { get; set; }
            public int MMR { get; set; }
            public string Region { get; set; }
            public string Password { get; set; }
            public string Nickname { get; set; }
            public string Avatar { get; set; }
            public int WinNo { get; set; }
            public int LossNo { get; set; }
            public string Rank { get; set; }
            public string MyDecksList { get; set; }
            public string OwnedCardsList { get; set; }

            public User IdUserNavigation { get; set; }
            public User InverseIdUserNavigation { get; set; }
            public ICollection<Game> GameIdP1Navigation { get; set; }
            public ICollection<Game> GameIdP2Navigation { get; set; }
        }
}
