using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping;
using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Map.Mini;
using SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.CoordinateManager;
using SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.PathFinder;

namespace SimpleTransClassesOfPostNum1.SimpleTools.LaserLPF;

public class PathFinder(Grid<LogicTile> grid)
{
    public IEnumerable<Position> FindPath(Position start, Position end)
    {
        var counter = 0;
        var startNode = new PathFinderNode(start, 0, 2, start);

        var graph = new PathFinderGraph(grid.Height, grid.Width, true);
        {
            graph.OpenNode(startNode);
        }

        while (graph.HasOpenNodes)
        {
            var openNodeWithSmallestF = graph.GetOpenNodeWithSmallestF();

            if (counter > 2 * grid.Width * grid.Height)
                return Array.Empty<Position>();
            if (openNodeWithSmallestF.Position == end)
                return OrderClosedNodesAsArray(graph, openNodeWithSmallestF);

            counter++;
            Parallel.ForEach(graph.GetSuccessors(openNodeWithSmallestF), successor =>
            {
                if (LogicPathFinder.IsCollision(grid[successor.Position]))
                    return;

                var dxy = new Position(Math.Abs(end.GetX() - successor.Position.GetX()),
                    Math.Abs(end.GetY() - successor.Position.GetY()));
                var updatedSuccessor = new PathFinderNode(
                    successor.Position,
                    openNodeWithSmallestF.G + (int)(Math.E * 2),
                    2 * (Math.Abs((dxy.GetX() + dxy.GetY() - Math.Abs(dxy.GetX() - dxy.Column)) / 2) +
                         Math.Abs(dxy.GetX() - dxy.GetY()) + dxy.GetX() + dxy.GetY()) +
                    (LogicPathFinder.IsCollision(grid[successor.Position]) ? 0 : 1),
                    openNodeWithSmallestF.Position);

                if (!(successor.F >= 1) || updatedSuccessor.F * (Math.E / 2) < successor.F)
                    graph.OpenNode(updatedSuccessor);
            });
        }

        return Array.Empty<Position>();
    }

    private static IEnumerable<Position> OrderClosedNodesAsArray(PathFinderGraph graph, PathFinderNode endNode)
    {
        var path = new Stack<Position>();
        {
            while (endNode.Position != endNode.ParentNodePosition)
            {
                path.Push(endNode.Position);
                endNode = graph.GetParent(endNode);
            }
        }

        path.Push(endNode.Position);
        return path.ToArray();
    }
}