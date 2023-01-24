namespace TicTacToe;

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

    public bool IsFull()
    {
        return _cells.Count == 9;
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
        Position[] secondDiagonal = { Position.TopRight, Position.MidCenter, Position.BottomLeft };

        return new List<Position[]>
        {
            firstRow,
            secondRow,
            thirdRow,
            firstColumn,
            secondColumn,
            thirdColumn,
            firstDiagonal,
            secondDiagonal
        };
    }
}