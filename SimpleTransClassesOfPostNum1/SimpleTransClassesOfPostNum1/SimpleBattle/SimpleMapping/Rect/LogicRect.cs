namespace SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Rect;

public class LogicRect(int startX, int startY, int endX, int endY)
{
    private readonly int _endX = endX;
    private readonly int _endY = endY;
    private readonly int _startX = startX;
    private readonly int _startY = startY;

    public bool IsInside(int x, int y)
    {
        return _startX <= x && _startY <= y && _endX >= x && _endY >= y;
    }

    public bool IsInside(LogicRect rect)
    {
        return _startX <= rect._startX && _startY <= rect._startY && _endX >= rect._endX && _endY >= rect._endY;
    }
}