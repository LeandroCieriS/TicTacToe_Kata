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

            var play = () => game.Play('O');

            play.Should().Throw<WrongTurnException>();
        }

        [Test]
        public void not_throw_if_starting_player_is_X()
        {
            var game = new Game();

            var play = () => game.Play('X');

            play.Should().NotThrow<WrongTurnException>();
        }
    }

    public class Game
    {
        public void Play(char player)
        {
            if (player == 'O')
                throw new WrongTurnException();
        }
    }

    public class WrongTurnException : Exception
    {
    }
}