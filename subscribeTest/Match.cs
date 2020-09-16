
namespace subscribeTest
{
    public class Match {

        public User playerOne ;
        public User playerTwo ;
        public string gameServerUrl;
        public long gameID;
        public Match(User one , User two , string url , long id ){
            this.playerOne = one;
            this.playerTwo = two;
            this.gameServerUrl = url;
            this.gameID = id;
        }
    }
}   
    