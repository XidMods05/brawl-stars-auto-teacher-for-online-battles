using SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.CoordinateManager;
using SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.PriorityQueue;

namespace SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.PathFinder;

internal class PathFinderGraph
{
    private readonly bool _allowDiagonalTraversal;
    private readonly Grid<PathFinderNode> _internalGrid;
    private readonly SimplePriorityQueue<PathFinderNode> _open = new();

    public PathFinderGraph(int height, int width, bool allowDiagonalTraversal)
    {
        _allowDiagonalTraversal = allowDiagonalTraversal;
        _internalGrid = new Grid<PathFinderNode>(height, width);
        Initialise();
    }

    public bool HasOpenNodes => _open.Count > 0;

    private void Initialise()
    {
        for (var row = 0; row < _internalGrid.Height; row++)
        for (var column = 0; column < _internalGrid.Width; column++)
            _internalGrid[row, column] = 
                new PathFinderNode(new Position(row, column), 0, 0, default);

        _open.Clear();
    }

    public IEnumerable<PathFinderNode> GetSuccessors(PathFinderNode node)
    {
        return _internalGrid
            .GetSuccessorPositions(node.Position, _allowDiagonalTraversal)
            .Select(successorPosition => _internalGrid[successorPosition]);
    }

    public PathFinderNode GetParent(PathFinderNode node)
    {
        return _internalGrid[node.ParentNodePosition];
    }

    public void OpenNode(PathFinderNode node)
    {
        _internalGrid[node.Position] = node;
        _open.Push(node);
    }

    public PathFinderNode GetOpenNodeWithSmallestF()
    {
        return _open.Pop();
    }
}