using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping;
using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Map;
using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Map.Mini;
using SimpleTransClassesOfPostNum1.SimpleMath.SectorOfVector;
using SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.CoordinateManager;

namespace SimpleTransClassesOfPostNum1.SimpleTools.LaserLPF;

public class LogicPathFinder // ! ONLY CUSTOM REALIZATIONS !
{
    private readonly LogicTileMap _logicTileMap;

    private readonly Dictionary<LogicVector2, LogicTile> _openTiles;
    private readonly PathFinder _pathfinder;

    public LogicPathFinder(LogicTileMap logicTileMap)
    {
        _logicTileMap = logicTileMap;
        
        var allTiles = new LogicTile[_logicTileMap.MapWidth, _logicTileMap.MapHeight];
        _openTiles = new Dictionary<LogicVector2, LogicTile>();

        var grid = new Grid<LogicTile>(_logicTileMap.MapWidth, _logicTileMap.MapHeight);
        
        for (var nodeX = 0;
             nodeX < _logicTileMap.MapWidth;
             nodeX++)
        for (var nodeY = 0;
             nodeY < _logicTileMap.MapHeight;
             nodeY++)
        {
            var tile = _logicTileMap.GetTile(nodeX, nodeY, true);

            allTiles[nodeX, nodeY] = tile;
            if (tile.TileCode == '.') _openTiles.TryAdd(new LogicVector2(tile.LogicX, tile.LogicY), tile);
            grid[nodeX, nodeY] = tile;
        }
        
        _pathfinder = new PathFinder(grid);
    }

    public List<LogicTile> FindPath(LogicTile s, LogicTile e)
    {
        var d = _pathfinder.FindPath(new Position(s.TileX, s.TileY), new Position(e.TileX, e.TileY));
        return d.Select(b => _logicTileMap.GetTile(b.Row, b.Column, true)).ToList();
    }
    
    public LogicVector2 AStare(LogicVector2 deltaTileVector) // A* to available tile.
    {
        if (!IsCollision(_logicTileMap.GetTile(deltaTileVector))) return deltaTileVector.Clone();
        
        var deltaTileDictionary = new Dictionary<int, LogicVector2>();
        {
            for (var nodeX = 0; nodeX < _logicTileMap.MapWidth; nodeX++)
            for (var nodeY = 0; nodeY < _logicTileMap.MapHeight; nodeY++)
            {
                var tile = _logicTileMap.GetTile(nodeX, nodeY, true);
                if (tile.TileData.GetTileCode() != '.') continue;

                var v1 = new LogicVector2(tile.LogicX, tile.LogicY);
                deltaTileDictionary.TryAdd(v1.GetDistance(deltaTileVector), v1);
            }
        }

        return deltaTileDictionary.Count < 3 ? deltaTileVector : deltaTileDictionary[deltaTileDictionary.Keys.Min()].Clone();
    }

    public static bool IsCollision(LogicTile deltaTile) // true = movement system are blocked.
    {
        return deltaTile.TileData.GetBlocksMovement() && !deltaTile.IsDestroyed();
    }
    
    public bool IsCollisionLevel3(LogicTile delta0) // true = movement system are blocked.
    {
        var x = delta0.TileX;
        var y = delta0.TileY;

        var delta1 = _logicTileMap.GetTile(x + 1, y, true);
        var delta2 = _logicTileMap.GetTile(x, y + 1, true);

        return IsCollision(delta0) || IsCollision(delta1) || IsCollision(delta2);
    }
    
    public bool IsCollisionLevel6(LogicTile delta0) // true = movement system are blocked.
    {
        var x = delta0.TileX;
        var y = delta0.TileY;

        var delta1 = _logicTileMap.GetTile(x + 1, y, true);
        var delta2 = _logicTileMap.GetTile(x, y + 1, true);
        
        var delta4 = _logicTileMap.GetTile(x - 1, y, true);
        var delta5 = _logicTileMap.GetTile(x, y - 1, true);

        return IsCollision(delta0) || IsCollision(delta1) || IsCollision(delta2) ||
               IsCollision(delta4) || IsCollision(delta5);
    }
    
    public bool IsCollisionLevel8(LogicTile delta0) // true = movement system are blocked.
    {
        var x = delta0.TileX;
        var y = delta0.TileY;

        var delta1 = _logicTileMap.GetTile(x + 1, y, true);
        var delta2 = _logicTileMap.GetTile(x, y + 1, true);
        
        var delta4 = _logicTileMap.GetTile(x - 1, y, true);
        var delta5 = _logicTileMap.GetTile(x, y - 1, true);
        
        var delta7 = _logicTileMap.GetTile(x + 1, y - 1, true);
        var delta8 = _logicTileMap.GetTile(x - 1, y + 1, true);

        return IsCollision(delta0) || IsCollision(delta1) || IsCollision(delta2) ||
               IsCollision(delta4) || IsCollision(delta5) || IsCollision(delta7) ||
               IsCollision(delta8) || IsCollision(delta8);
    }
    
    public bool IsCollisionLevel10(LogicTile delta0) // true = movement system are blocked.
    {
        var x = delta0.TileX;
        var y = delta0.TileY;

        var delta1 = _logicTileMap.GetTile(x + 1, y, true);
        var delta2 = _logicTileMap.GetTile(x, y + 1, true);
        var delta3 = _logicTileMap.GetTile(x + 1, y + 1, true);
        
        var delta4 = _logicTileMap.GetTile(x - 1, y, true);
        var delta5 = _logicTileMap.GetTile(x, y - 1, true);
        var delta6 = _logicTileMap.GetTile(x - 1, y - 1, true);
        
        var delta7 = _logicTileMap.GetTile(x + 1, y - 1, true);
        var delta8 = _logicTileMap.GetTile(x - 1, y + 1, true);
        
        var delta9 = _logicTileMap.GetTile(x + 2, y - 1, true);
        var delta10 = _logicTileMap.GetTile(x - 2, y + 1, true);

        return IsCollision(delta0) || IsCollision(delta1) || IsCollision(delta2) || IsCollision(delta3) ||
               IsCollision(delta4) || IsCollision(delta5) || IsCollision(delta6) || IsCollision(delta7) ||
               IsCollision(delta8) || IsCollision(delta8) || IsCollision(delta9) || IsCollision(delta10);
    }
}