using System;
using System.Collections.Generic;

namespace publishTest
{
    public class CompCard
    {
        public CompCard(int idCard, string name, int attack, int health, int shield)
        {
            IdCard = idCard;
            Name = name;
            Attack = attack;
            Health = health;
            Shield = shield;
        }

        // #nullable enable
        public int IdCard { get; set; }
        public string Name { get; set; }//mozda i ovo ulonim
        public int Attack { get; set; }
        public int Health { get; set; }
        public int Shield { get; set; }
    }
}
