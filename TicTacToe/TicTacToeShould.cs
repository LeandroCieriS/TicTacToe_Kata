using FluentAssertions;

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

        [Test]
        public void Declare_a_winner_if_first_column_is_full_with_the_same_player()
        {
            var game = new Game();
            game.Play(Player.X, Position.TopLeft);
            game.Play(Player.O, Position.TopRight);
            game.Play(Player.X, Position.BottomLeft);
            game.Play(Player.O, Position.BottomCenter);

            game.Play(Player.X, Position.MidLeft);

            game.Winner.Should().Be(Player.X);
        }

        [Test]
        public void Declare_a_winner_if_second_column_is_full_with_the_same_player()
        {
            var game = new Game();
            game.Play(Player.X, Position.TopLeft);
            game.Play(Player.O, Position.TopCenter);
            game.Play(Player.X, Position.BottomLeft);
            game.Play(Player.O, Position.BottomCenter);
            game.Play(Player.X, Position.MidRight);

            game.Play(Player.O, Position.MidCenter);

            game.Winner.Should().Be(Player.O);
        }

        [Test]
        public void Declare_a_winner_if_third_column_is_full_with_the_same_player()
        {
            var game = new Game();
            game.Play(Player.X, Position.TopLeft);
            game.Play(Player.O, Position.TopRight);
            game.Play(Player.X, Position.BottomLeft);
            game.Play(Player.O, Position.BottomRight);
            game.Play(Player.X, Position.MidCenter);

            game.Play(Player.O, Position.MidRight);

            game.Winner.Should().Be(Player.O);
        }

        [Test]
        public void Declare_a_winner_if_first_diagonal_is_full_with_the_same_player()
        {
            var game = new Game();
            game.Play(Player.X, Position.TopLeft);
            game.Play(Player.O, Position.TopRight);
            game.Play(Player.X, Position.MidCenter);
            game.Play(Player.O, Position.BottomLeft);

            game.Play(Player.X, Position.BottomRight);

            game.Winner.Should().Be(Player.X);
        }
    }

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

    internal class Board
    {
        private readonly Dictionary<Position, Player> _cells = new();
        private readonly List<Position[]> _viableTicTacToes = InitializeBoard();

        public void SetPosition(Player player, Position position)
        {
            if (CellIsOccupied(position))
                throw new PlayedPositionIsNotEmptyException();
            _cells[position] = player;
        }

        private bool CellIsOccupied(Position position)
        {
            return _cells.ContainsKey(position);
        }

        public bool HasAWinner()
        {
            return _viableTicTacToes.Any(LineIsSamePlayer);
        }

        private bool LineIsSamePlayer(IReadOnlyList<Position> linePositions)
        {
            return LineIsFull(linePositions) &&
                   _cells[linePositions[0]] == _cells[linePositions[1]] &&
                   _cells[linePositions[1]] == _cells[linePositions[2]];
        }

        private bool LineIsFull(IReadOnlyList<Position> rowPositions)
        {
            return _cells.ContainsKey(rowPositions[0]) && _cells.ContainsKey(rowPositions[1]) &&
                   _cells.ContainsKey(rowPositions[2]);
        }

        private static List<Position[]> InitializeBoard()
        {
            Position[] firstRow = { Position.TopLeft, Position.TopCenter, Position.TopRight };
            Position[] secondRow = { Position.MidLeft, Position.MidCenter, Position.MidRight };
            Position[] thirdRow = { Position.BottomLeft, Position.BottomCenter, Position.BottomRight };

            Position[] firstColumn = { Position.TopLeft, Position.MidLeft, Position.BottomLeft };
            Position[] secondColumn = { Position.TopCenter, Position.MidCenter, Position.BottomCenter };
            Position[] thirdColumn = { Position.TopRight, Position.MidRight, Position.BottomRight };

            Position[] firstDiagonal = { Position.TopLeft, Position.MidCenter, Position.BottomRight };

            return new List<Position[]>
            {
                firstRow,
                secondRow,
                thirdRow,
                firstColumn,
                secondColumn,
                thirdColumn,
                firstDiagonal
            };
        }
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