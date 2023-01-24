using FluentAssertions;

namespace TicTacToe;

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

    [Test]
    public void Declare_a_winner_if_second_diagonal_is_full_with_the_same_player()
    {
        var game = new Game();
        game.Play(Player.X, Position.TopRight);
        game.Play(Player.O, Position.TopLeft);
        game.Play(Player.X, Position.MidCenter);
        game.Play(Player.O, Position.BottomRight);

        game.Play(Player.X, Position.BottomLeft);

        game.Winner.Should().Be(Player.X);
    }

    [Test]
    public void Declare_game_over_if_board_is_full()
    {
        var game = new Game();
        game.Play(Player.X, Position.TopRight);
        game.Play(Player.O, Position.TopLeft);
        game.Play(Player.X, Position.TopCenter);
        game.Play(Player.O, Position.MidRight);
        game.Play(Player.X, Position.MidLeft);
        game.Play(Player.O, Position.MidCenter);
        game.Play(Player.X, Position.BottomRight);
        game.Play(Player.O, Position.BottomCenter);

        game.Play(Player.X, Position.BottomLeft);

        game.Winner.Should().BeNull();
        game.IsOver.Should().BeTrue();
    }

    [Test]
    public void Not_let_play_if_board_is_full()
    {
        var game = new Game();
        game.Play(Player.X, Position.TopRight);
        game.Play(Player.O, Position.TopLeft);
        game.Play(Player.X, Position.TopCenter);
        game.Play(Player.O, Position.MidRight);
        game.Play(Player.X, Position.MidLeft);
        game.Play(Player.O, Position.MidCenter);
        game.Play(Player.X, Position.BottomRight);
        game.Play(Player.O, Position.BottomCenter);
        game.Play(Player.X, Position.BottomLeft);

        var play = () => game.Play(Player.X, Position.BottomLeft);

        play.Should().Throw<GameIsOverException>();
    }
}