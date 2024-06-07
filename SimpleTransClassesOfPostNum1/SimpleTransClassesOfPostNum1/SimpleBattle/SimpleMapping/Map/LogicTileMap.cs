using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Map.Mini;
using SimpleTransClassesOfPostNum1.SimpleMath.SectorOfVector;
using SimpleTransClassesOfPostNum1.SimpleTools.LaserLPF;

namespace SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Map;

public class LogicTileMap(LogicMapLoader logicMapLoader)
{
    private Dictionary<int, LogicTile> _dynamicTiles = null!;
    private Dictionary<int, int> _dynamicTiles2 = null!;
    private LogicTile[,] _tiles = null!;

    public int MapHeight;
    public int MapWidth;
    
    public LogicPathFinder PathFinder = null!;

    public void LoadMap()
    {

        _tiles = new LogicTile[MapHeight, MapWidth];
        _dynamicTiles = new Dictionary<int, LogicTile>();
        _dynamicTiles2 = new Dictionary<int, int>();
    }

    public void GenerateTileMap(string data)
    {
        var chars = data.ToCharArray();
        var counter = 0;

        for (var i = 0; i < MapHeight; i++)
        for (var j = 0; j < MapWidth; j++)
        {
            _tiles[i, j] = new LogicTile(chars[counter], TileToLogic(j), TileToLogic(i));
            {
                counter++;
            }
        }

        PathFinder = new LogicPathFinder(this);
    }

    public int GetPathFinderCost(int x, int y)
    {
        return GetTile(x, y) != null! ? GetTile(x, y).GetPathFinderCost() : 0x7FFFFFFF;
    }

    public bool IsPassablePathFinder(int x, int y)
    {
        return GetTile(x, y) != null! && GetTile(x, y).IsPassablePathFinder();
    }

    public int GetOriginalWallCount()
    {
        return _dynamicTiles.Count;
    }

    public LogicTile GetTile(LogicVector2 logicVector2, bool isTile = false, bool isDynamicTile = false,
        int dynamicTileCounter = -1)
    {
        var x = logicVector2.GetX();
        var y = logicVector2.GetY();

        if (!isTile)
        {
            x = LogicToTile(x);
            y = LogicToTile(y);
        }

        if (isDynamicTile)
            return dynamicTileCounter > -1
                ? _dynamicTiles[_dynamicTiles2[dynamicTileCounter]]
                : _dynamicTiles[logicMapLoader.GetTileIndex(x, y, true)];

        if (x >= 0 && x < MapWidth && y >= 0 && y < MapHeight)
            return _tiles[y, x];

        return null!;
    }

    public LogicTile GetTile(int x, int y, bool isTile = false, bool isDynamicTile = false!,
        int dynamicTileCounter = -1)
    {
        if (!isTile)
        {
            x = LogicToTile(x);
            y = LogicToTile(y);
        }

        if (isDynamicTile)
            return dynamicTileCounter > -1
                ? _dynamicTiles[_dynamicTiles2[dynamicTileCounter]]
                : _dynamicTiles[logicMapLoader.GetTileIndex(x, y, true)];

        if (x >= 0 && x < MapWidth && y >= 0 && y < MapHeight)
            return _tiles[y, x];

        return null!;
    }

    public void AddDynamicTile(LogicBattleModeServer logicBattleModeServer, int tileX, int tileY, bool isTile = true,
        int dynamicCode = 1)
    {
        var v1 = logicMapLoader.GetTileIndex(tileX, tileY, isTile);
        _dynamicTiles.TryAdd(v1,
            new LogicTile(dynamicCode.ToString()[0], isTile ? TileToLogic(tileX) : tileX,
                isTile ? TileToLogic(tileY) : tileY));
        _dynamicTiles2.TryAdd(_dynamicTiles2.Count, v1);
    }

    public void RemoveDynamicTile(int tileX, int tileY, bool isTile = true)
    {
        var v1 = logicMapLoader.GetTileIndex(tileX, tileY, isTile);

        _dynamicTiles.Remove(v1);
        _dynamicTiles2.Remove(_dynamicTiles2.FirstOrDefault(x => x.Value == v1).Key);
    }

    public static int LogicToTile(int logicValue)
    {
        return logicValue / 300;
    }

    public static int TileToLogic(int tile)
    {
        return 300 * tile + 150;
    }

    public static int LogicToPathFinderTile(int logicValue)
    {
        return logicValue / 100;
    }

    public static int PathFinderTileToLogic(int tile, bool a2)
    {
        return 100 * tile + (a2 ? 50 : 0);
    }
}