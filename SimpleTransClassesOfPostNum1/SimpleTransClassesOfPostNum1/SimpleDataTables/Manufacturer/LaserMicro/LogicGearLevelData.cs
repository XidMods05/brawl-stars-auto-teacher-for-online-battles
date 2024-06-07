using SimpleTransClassesOfPostNum1.SimpleDataTables.DataSector;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Omniscient;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;

public class LogicGearLevelData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private int _gearScrap;
    private int _gearTokens;

    // LogicGearLevelData.

    public override void CreateReferences()
    {
        _gearTokens = GetIntegerValue("GearTokens", 0);
        _gearScrap = GetIntegerValue("GearScrap", 0);
    }

    public int GetGearTokens()
    {
        return _gearTokens;
    }

    public int GetGearScrap()
    {
        return _gearScrap;
    }
}