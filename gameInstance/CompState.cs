using System;
using System.Collections.Generic;

namespace gameInstance{

    public class  CompState{

        private int p1;
        private int p2;
        private List<int> p1Hand;
        private List<int> p2Hand;
        private int p1Lp;
        private int p2Lp;        
        private List<CompCard> p1Table;
        private List<CompCard> p2Table;

        private int playerGems;
        private int playerCrystals;
        private int goesFirst;
        private int onMove;

        public List<int> P1Hand { get => p1Hand; set => p1Hand = value; }
        public List<int> P2Hand { get => p2Hand; set => p2Hand = value; }
        public int P1Lp { get => p1Lp; set => p1Lp = value; }
        public int P2Lp { get => p2Lp; set => p2Lp = value; }
        public List<CompCard> P1Table { get => p1Table; set => p1Table = value; }
        public List<CompCard> P2Table { get => p2Table; set => p2Table = value; }
        public int PlayerGems { get => playerGems; set => playerGems = value; }
        public int PlayerCrystals { get => playerCrystals; set => playerCrystals = value; }
        public int GoesFirst { get => goesFirst; set => goesFirst = value; }
        public int OnMove { get => onMove; set => onMove = value; }
        public int P1 { get => p1; set => p1 = value; }
        public int P2 { get => p2; set => p2 = value; }
    }


}