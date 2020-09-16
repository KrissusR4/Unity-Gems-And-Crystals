using System;
using System.Collections.Generic;

namespace publishTest
{
    public class Card
    {
        //#nullable enable
        public int IdCard { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public sbyte Effect { get; set; }
        public string EffectList { get; set; }
        public sbyte Spell { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public int Shield { get; set; }
        public int GemsCost { get; set; }
        public int CrystalsCost { get; set; }
    }
}
