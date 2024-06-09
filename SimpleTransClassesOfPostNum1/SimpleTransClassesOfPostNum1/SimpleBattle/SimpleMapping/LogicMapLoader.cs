using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Map;
using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Map.Meta;
using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Rect;
using SimpleTransClassesOfPostNum1.SimpleDataTables;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;
using SimpleTransClassesOfPostNum1.SimpleHTables;
using SimpleTransClassesOfPostNum1.SimpleKeep;

namespace SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping;

public class LogicMapLoader
{
    public string GlobalMapStringedData { get; private set; } = string.Empty;
    public LogicTileMetadata GlobalMapMetadata { get; private set; } = null!;

    private LogicLocationData _logicLocationData = null!;
    private LogicTileMap _logicTileMap = null!;
    private LogicRect _logicRect = null!;

    public void InitWithMapFromDataTable(LogicLocationData logicLocationData)
    {
        _logicLocationData = logicLocationData;
        var u2 = VariablesKeeper.MapStructureDictionary[logicLocationData.GetMap()];

        var logicTileMap = new LogicTileMap(this);
        {
            logicTileMap.MapHeight = Convert.ToInt32(u2[0]);
            logicTileMap.MapWidth = Convert.ToInt32(u2[1]);
        }

        GlobalMapStringedData = u2[2].ToString()!;
        
        _logicTileMap = logicTileMap;
        _logicRect = new LogicRect(0, 0, LogicTileMap.TileToLogic(logicTileMap.MapWidth),
            LogicTileMap.TileToLogic(logicTileMap.MapHeight));
        
        PopulateMetaData(logicLocationData);
    }

    public void PopulateMetaData(LogicLocationData logicLocationData)
    {
        var u2 = VariablesKeeper.MapStructureDictionary[logicLocationData.GetMap()];
        GlobalMapMetadata = u2.Count > 3 ? new LogicTileMetadata(u2[3].ToString()!) : new LogicTileMetadata(null);
    }

    public int GetTileIndex(int x, int y, bool isTileCords)
    {
        if (!isTileCords)
            return LogicTileMap.LogicToTile(y) * _logicTileMap.MapWidth + LogicTileMap.LogicToTile(x);
        return y * _logicTileMap.MapWidth + x;
    }
    
    public static LogicTileData TileCodeToTileData(char code)
    {
        foreach (var data in LogicDataTables.GetAllDataFromCsvById(CsvFilesHelperTable.Tiles.GetId()))
        {
            if (data is not LogicTileData tileData) continue;
            if (tileData.GetTileCode() != code) continue;

            return tileData;
        }

        return (LogicTileData)LogicDataTables.GetAllDataFromCsvById(CsvFilesHelperTable.Tiles.GetId())[0];
    }

    public void Destruct()
    {
        GlobalMapStringedData = null!;
        GlobalMapMetadata = null!;

        _logicTileMap = null!;
        _logicRect = null!;
    }
}
