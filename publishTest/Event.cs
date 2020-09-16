namespace publishTest{

    public enum EvType{
        attack,
        play,
        damadge,
        destroy,
        spell,
        endTurn,
        surender,
    }
    public interface IGameEvent{
        public IGameEvent CheckState();
    }
    public class GameEvent{
        public string Source { get ; set; }
        public string Target { get ; set; }
        public EvType Type { get ; set; }
    }
    public class AttackEvent : GameEvent , IGameEvent
    {
        public IGameEvent CheckState()
        {
            throw new System.NotImplementedException();
        }
    }
    public class PlayEvent : GameEvent , IGameEvent
    {
        public IGameEvent CheckState()
        {
            throw new System.NotImplementedException();
        }
    }
    public class DestroyEvent : GameEvent , IGameEvent
    {
        public IGameEvent CheckState()
        {
            throw new System.NotImplementedException();
        }
    }
    public class SpellEvent : GameEvent , IGameEvent
    {
        public IGameEvent CheckState(){
            throw new System.NotImplementedException();
        }
    }
    



}