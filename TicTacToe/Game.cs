namespace TicTacToe;

public class Game
{
    private Player _lastPlayer = Player.O;
    private readonly Board _board = new();

    public Player? Winner => _board.HasAWinner() ? _lastPlayer : null;

    public void Play(Player player, Position position)
    {
        CheckTurns(player);
        _board.SetPosition(player, position);
        _board.HasAWinner();
    }

    private void CheckTurns(Player player)
    {
        if (_lastPlayer == player)
            throw new WrongTurnException();
        _lastPlayer = player;
    }

}