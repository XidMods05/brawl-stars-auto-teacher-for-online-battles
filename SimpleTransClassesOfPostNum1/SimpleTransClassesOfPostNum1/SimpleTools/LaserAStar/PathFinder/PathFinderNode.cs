using System.Runtime.InteropServices;
using SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.CoordinateManager;

namespace SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.PathFinder;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal readonly struct PathFinderNode(Position position, int g, int h, Position parentNodePosition)
{
    public Position Position { get; } = position;
    public Position ParentNodePosition { get; } = parentNodePosition;

    public int G { get; } = g;
    public int H { get; } = h;
    public int F { get; } = g + h;
}