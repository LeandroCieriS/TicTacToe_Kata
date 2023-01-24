using FluentAssertions;
using System;

namespace TicTacToe
{
    public class TicTacToeShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void throw_if_starting_player_is_not_X()
        {
            var game = new Game();

            var play = () => game.Play('O');

            play.Should().Throw<WrongTurnException>();
        }
    }

    public class Game
    {
        public void Play(char player)
        {
            throw new WrongTurnException();
        }
    }

    public class WrongTurnException : Exception
    {
    }
}