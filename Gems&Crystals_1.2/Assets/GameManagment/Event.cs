namespace gameInstance{

    public enum EvType{
        attackPlayer,
        attack,
        play,
        damadge,
        destroy,
        spell,
        endTurn,
        surender,
        draw,
    }
    public interface IGameEvent{
        int Source { get ; set; }
        int Target { get ; set; }
        EvType Type { get ; set; }
        IGameEvent CheckState();
    }
   
    public class AttackEvent :IGameEvent
    {
        public int Source { get ; set; }
        public int Target { get ; set; }
        public EvType Type { get ; set; }
        public IGameEvent CheckState()
        {
            throw new System.NotImplementedException();
        }
    }
    public class PlayEvent :  IGameEvent
    {
        public int Source { get ; set; }
        public int Target { get ; set; }
        public EvType Type { get ; set; }
        public IGameEvent CheckState()
        {
            throw new System.NotImplementedException();
        }
    }
    public class DestroyEvent :  IGameEvent
    {
        public int Source { get ; set; }
        public int Target { get ; set; }
        public EvType Type { get ; set; }
        public IGameEvent CheckState()
        {
            throw new System.NotImplementedException();
        }
    }
    public class SpellEvent :  IGameEvent
    {
        public int Source { get ; set; }
        public int Target { get ; set; }
        public EvType Type { get ; set; }
        public IGameEvent CheckState(){
            throw new System.NotImplementedException();
        }
    }
    



}