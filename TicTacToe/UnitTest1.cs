using FluentAssertions;
using System;

namespace TicTacToe
{
    public class TicTacToeShould
    {
        [Test]
        public void throw_if_starting_player_is_not_X()
        {
            var game = new Game();

            var play = () => game.Play(Player.O);

            play.Should().Throw<WrongTurnException>();
        }

        [Test]
        public void not_throw_if_starting_player_is_X()
        {
            var game = new Game();

            var play = () => game.Play(Player.X);

            play.Should().NotThrow<WrongTurnException>();
        }

        [Test]
        public void alternate_turns_between_player_X_and_O()
        {
            var game = new Game();
            game.Play(Player.X);

            var play = () => game.Play(Player.X);

            play.Should().Throw<WrongTurnException>();
        }
    }

    public enum Player
    {
        X,
        O
    }

    public class Game
    {
        private Player _lastPlayer = Player.O;
        public void Play(Player player)
        {
            if (_lastPlayer == player)
                throw new WrongTurnException();
            _lastPlayer = player;
        }
    }

    public class WrongTurnException : Exception { }
}