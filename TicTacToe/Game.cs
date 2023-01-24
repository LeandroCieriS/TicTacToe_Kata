namespace TicTacToe;

public class Game
{
    private Player _lastPlayer = Player.O;
    private readonly Board _board = new();

    public Player? Winner => _board.HasAWinner() ? _lastPlayer : null;
    public bool IsOver { get; set; }

    public void Play(Player player, Position position)
    {
        CheckTurns(player);
        _board.SetPosition(player, position);
        if (_board.HasAWinner() || _board.IsFull())
            IsOver = true;
    }

    private void CheckTurns(Player player)
    {
        if (IsOver)
            throw new GameIsOverException();
        if (_lastPlayer == player)
            throw new WrongTurnException();
        _lastPlayer = player;
    }

}