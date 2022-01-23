namespace Game.Player
{
    public class PlayerStats
    {
        private int _position;
        private string _name;
        private int _score;
        public int Position { get { return _position; } set { _position = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public int Score { get { return _score; } set { _score = value; } }
    }
}