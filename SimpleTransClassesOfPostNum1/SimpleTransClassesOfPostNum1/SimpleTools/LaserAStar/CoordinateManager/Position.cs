namespace SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.CoordinateManager;

public readonly struct Position(int row = 0, int column = 0)
{
    public bool Equals(Position other)
    {
        return Row == other.Row && Column == other.Column;
    }

    public override bool Equals(object? obj)
    {
        return obj is Position other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Column);
    }

    public int Row { get; } = row;
    public int Column { get; } = column;

    public int GetX()
    {
        return Row;
    }

    public int GetY()
    {
        return Column;
    }

    public static bool operator ==(Position a, Position b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Position a, Position b)
    {
        return !a.Equals(b);
    }
}