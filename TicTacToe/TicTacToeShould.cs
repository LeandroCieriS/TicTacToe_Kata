using FluentAssertions;
using System.Numerics;

namespace TicTacToe
{
    public class TicTacToeShould
    {
        [Test]
        public void Throw_if_starting_player_is_not_X()
        {
            var game = new Game();

            var play = () => game.Play(Player.O, Position.TopLeft);

            play.Should().Throw<WrongTurnException>();
        }

        [Test]
        public void Not_throw_if_starting_player_is_X()
        {
            var game = new Game();

            var play = () => game.Play(Player.X, Position.TopLeft);

            play.Should().NotThrow<WrongTurnException>();
        }

        [Test]
        public void Alternate_turns_between_player_X_and_O()
        {
            var game = new Game();
            game.Play(Player.X, Position.MidRight);

            var play = () => game.Play(Player.X, Position.TopLeft);

            play.Should().Throw<WrongTurnException>();
        }

        [Test]
        public void Not_be_able_to_play_same_position_twice()
        {
            var game = new Game();
            game.Play(Player.X, Position.TopLeft);

            var play = () => game.Play(Player.O, Position.TopLeft);

            play.Should().Throw<PlayedPositionIsNotEmptyException>();
        }

        [Test]
        public void Declare_a_winner_if_first_row_is_full_with_the_same_player()
        {
            var game = new Game();
            game.Play(Player.X, Position.TopLeft);
            game.Play(Player.O, Position.MidLeft);
            game.Play(Player.X, Position.TopCenter);
            game.Play(Player.O, Position.MidCenter);

            game.Play(Player.X, Position.TopRight);

            game.Winner.Should().Be(Player.X);
        }

        [Test]
        public void Declare_a_winner_if_second_row_is_full_with_the_same_player()
        {
            var game = new Game();
            game.Play(Player.X, Position.TopLeft);
            game.Play(Player.O, Position.MidLeft);
            game.Play(Player.X, Position.TopCenter);
            game.Play(Player.O, Position.MidCenter);
            game.Play(Player.X, Position.BottomRight);

            game.Play(Player.O, Position.MidRight);

            game.Winner.Should().Be(Player.O);
        }

        [Test]
        public void Declare_a_winner_if_third_row_is_full_with_the_same_player()
        {
            var game = new Game();
            game.Play(Player.X, Position.TopLeft);
            game.Play(Player.O, Position.BottomLeft);
            game.Play(Player.X, Position.TopCenter);
            game.Play(Player.O, Position.BottomCenter);
            game.Play(Player.X, Position.MidRight);

            game.Play(Player.O, Position.BottomRight);

            game.Winner.Should().Be(Player.O);
        }
    }

    public class Game
    {
        private Player _lastPlayer = Player.O;
        private readonly Board _board = new();

        public Player? Winner => _board.GetWinner();

        public void Play(Player player, Position position)
        {
            CheckTurns(player);
            _board.SetPosition(player, position);
            _board.GetWinner();
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
        private readonly Dictionary<Position, Player> _cells = new();

        private readonly Position[] firstRow = { Position.TopLeft, Position.TopCenter, Position.TopRight };
        private readonly Position[] secondRow = { Position.MidLeft, Position.MidCenter, Position.MidRight };
        private readonly Position[] thirdRow = { Position.BottomLeft, Position.BottomCenter, Position.BottomRight };

        public void SetPosition(Player player, Position position)
        {
            if (CellIsOccupied(position))
                throw new PlayedPositionIsNotEmptyException();
            _cells[position] = player;
        }

        private bool CellIsOccupied(Position position) => _cells.ContainsKey(position);

        public Player? GetWinner()
        {
            if (RowIsFull(firstRow) && RowIsSamePlayer(firstRow))
                return _cells[Position.TopLeft];

            if (RowIsFull(secondRow) && RowIsSamePlayer(secondRow))
                return _cells[Position.MidLeft];

            if (RowIsFull(thirdRow) && RowIsSamePlayer(thirdRow))
                return _cells[Position.BottomLeft];

            return null;
        }

        private bool RowIsSamePlayer(IReadOnlyList<Position> rowPositions) =>
            _cells[rowPositions[0]] == _cells[rowPositions[1]] &&
            _cells[rowPositions[1]] == _cells[rowPositions[2]];

        private bool RowIsFull(IReadOnlyList<Position> rowPositions) =>
            _cells.ContainsKey(rowPositions[0]) && _cells.ContainsKey(rowPositions[1]) && _cells.ContainsKey(rowPositions[2]);
    }

    public enum Position
    {
        TopLeft,
        TopCenter,
        TopRight,

        MidLeft,
        MidCenter,
        MidRight,

        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public enum Player
    {
        X,
        O
    }

    public class WrongTurnException : Exception { }

    public class PlayedPositionIsNotEmptyException : Exception { }
}