using SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;

namespace SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Map.Mini;

public class LogicTile(char code, int x, int y)
{
    public readonly int LogicX = x, LogicY = y;
    public readonly char TileCode = code;
    public readonly LogicTileData TileData = LogicMapLoader.TileCodeToTileData(code);
    public readonly int TileX = x / 300, TileY = y / 300;

    private bool _destructed;

    public void DestroyTile()
    {
        _destructed = true;
    }

    public bool IsDestroyed()
    {
        return _destructed;
    }

    public bool IsDestroyableWithWeaponType()
    {
        return TileData.GetIsDestructibleNormalWeapon();
    }

    public bool IsPassablePathFinder()
    {
        return ((uint)LogicX | (uint)LogicY) <= 1 && ((1 << (LogicX + 2 * LogicY)) & 16) == 0;
    }

    public int GetPathFinderCost()
    {
        if (((uint)LogicX | (uint)LogicY) > 1) return 0x7FFFFFFF;
        return (16 & (1 << (LogicX + 2 * LogicY))) == 0 ? 0 : 0x7FFFFFFF;
    }
}