using FluentAssertions;

namespace TicTacToe
{
    public class TicTacToeShould
    {
        [Test]
        public void throw_if_starting_player_is_not_X()
        {
            var game = new Game();

            var play = () => game.Play(Player.O, 0, 0);

            play.Should().Throw<WrongTurnException>();
        }

        [Test]
        public void not_throw_if_starting_player_is_X()
        {
            var game = new Game();

            var play = () => game.Play(Player.X, 0, 0);

            play.Should().NotThrow<WrongTurnException>();
        }

        [Test]
        public void alternate_turns_between_player_X_and_O()
        {
            var game = new Game();
            game.Play(Player.X, 0, 0);

            var play = () => game.Play(Player.X, 0, 0);

            play.Should().Throw<WrongTurnException>();
        }

        [Test]
        public void not_be_able_to_play_same_position_twice()
        {
            var game = new Game();
            game.Play(Player.X, 0, 0);

            var play = () => game.Play(Player.X, 0, 0);

            play.Should().Throw<PlayedPositionIsNotEmptyException>();
        }
    }

    public class Game
    {
        private Player _lastPlayer = Player.O;
        public void Play(Player player, int x, int y)
        {
            if (_lastPlayer == player)
                throw new WrongTurnException();
            _lastPlayer = player;
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