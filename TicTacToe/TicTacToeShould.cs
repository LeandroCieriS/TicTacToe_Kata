using FluentAssertions;

namespace TicTacToe
{
    public class TicTacToeShould
    {
        [Test]
        public void Throw_if_starting_player_is_not_X()
        {
            var game = new Game();

            var play = () => game.Play(Player.O, new Position(0, 0));

            play.Should().Throw<WrongTurnException>();
        }

        [Test]
        public void Not_throw_if_starting_player_is_X()
        {
            var game = new Game();

            var play = () => game.Play(Player.X, new Position(0, 0));

            play.Should().NotThrow<WrongTurnException>();
        }

        [Test]
        public void Alternate_turns_between_player_X_and_O()
        {
            var game = new Game();
            game.Play(Player.X, new Position(0, 1));

            var play = () => game.Play(Player.X, new Position(0, 0));

            play.Should().Throw<WrongTurnException>();
        }

        [Test]
        public void Not_be_able_to_play_same_position_twice()
        {
            var game = new Game();
            game.Play(Player.X, new Position(0, 0));

            var play = () => game.Play(Player.O, new Position(0, 0));

            play.Should().Throw<PlayedPositionIsNotEmptyException>();
        }

        [Test]
        public void Declare_a_winner_if_there_is_a_row_with_the_same_player()
        {
            var game = new Game();
            game.Play(Player.X, new Position(0, 0));
            game.Play(Player.O, new Position(1, 0));
            game.Play(Player.X, new Position(0, 1));
            game.Play(Player.O, new Position(1, 1));

            var play = () => game.Play(Player.X, new Position(0, 2));
        }
    }

    public class Game
    {
        private Player _lastPlayer = Player.O;
        private readonly Board _board = new();

        public void Play(Player player, Position position)
        {
            CheckTurns(player);
            _board.SetPosition(player, position);

        }

        private void CheckTurns(Player player)
        {
            if (_lastPlayer == player)
                throw new WrongTurnException();
            _lastPlayer = player;
        }
    }

    internal class Board
    {
        private readonly string[,] _cells = new string[3, 3];

        public void SetPosition(Player player, Position position)
        {
            if (CellIsOccupied(position))
                throw new PlayedPositionIsNotEmptyException();
            _cells[position.X, position.Y] = player.ToString();
        }

        private bool CellIsOccupied(Position position) => !string.IsNullOrEmpty(_cells[position.X, position.Y]);
    }

    public class Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public enum Player
    {
        X,
        O
    }

    public class WrongTurnException : Exception { }

    public class PlayedPositionIsNotEmptyException : Exception { }
}