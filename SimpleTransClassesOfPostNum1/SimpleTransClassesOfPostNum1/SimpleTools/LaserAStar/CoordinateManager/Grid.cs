namespace SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.CoordinateManager;

public class Grid<T>
{
    private readonly T[] _grid;

    public Grid(int height, int width)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);

        Width = width;
        Height = height;

        _grid = new T[height * width];
    }

    private static IEnumerable<(sbyte row, sbyte column)> CardinalDirectionOffsets
    {
        get
        {
            yield return (0, -1);
            yield return (1, 0);
            yield return (0, 1);
            yield return (-1, 0);
        }
    }

    private static IEnumerable<(sbyte row, sbyte column)> DiagonalsOffsets
    {
        get
        {
            yield return (1, -1);
            yield return (1, 1);
            yield return (-1, 1);
            yield return (-1, -1);
        }
    }

    public int Width { get; }
    public int Height { get; }

    public T this[Position position]
    {
        get => _grid[Width * position.Row + position.Column];
        set => _grid[Width * position.Row + position.Column] = value;
    }

    public T this[int row, int column]
    {
        get => _grid[Width * row + column];
        set => _grid[Width * row + column] = value;
    }

    public IEnumerable<Position> GetSuccessorPositions(Position node, bool optionsUseDiagonals = false)
    {
        return from neighbourOffset in optionsUseDiagonals
                ? CardinalDirectionOffsets.Concat(DiagonalsOffsets)
                : CardinalDirectionOffsets
            let successorRow = node.Row + neighbourOffset.row
            let successorColumn = node.Column + neighbourOffset.column
            where successorRow >= 0 && successorRow < Height
            where successorColumn >= 0 && successorColumn < Width
            select new Position(successorRow, successorColumn);
    }
}