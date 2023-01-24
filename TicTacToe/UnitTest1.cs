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

            play.Should().Throw<WrongTurn>();
        }
    }

    public class Game
    {
        public void Play(char c)
        {
            throw new NotImplementedException();
        }
    }

    public class WrongTurn : Exception
    {
    }
}