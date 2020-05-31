namespace GameCore
{
    public class GameController
    {
        private Game _game;
        private Player _player1;
        private Player _player2;

        public void Initialize()
        {
            _game = new Game(7,6);
            _player1 = new Player(1);
            _player2 = new Player(2);
            CurrentPlayer = _player1;
        }

        public int TryPlayAtColumn(int columnId)
        {
            if (!_game.CanPlayAtColumn(columnId))
            {
                return -1;
            }
            _game.PlayAt(CurrentPlayer.Id, columnId);
            CurrentPlayer = CurrentPlayer == _player1 ? _player2 : _player1;
            return CurrentPlayer.Id;
        }

        public bool CanPlayAtColumn(int playerId, int columnId)
        {
            return CurrentPlayer.Id == playerId && _game.CanPlayAtColumn(columnId);
        }

        public int GetGameState()
        {
            return _game.CheckState();
        }

        public int GetValueAt(int x, int y)
        {
            return _game.GetValueAt(x, y);
        }
        
        public Player CurrentPlayer { get; private set; }
    }
}