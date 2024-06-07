using SimpleTransClassesOfPostNum1.SimpleDataTables.DataSector;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Omniscient;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;

public class LogicRegionData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _displayName = null!;
    private bool _isCountry;

    // LogicRegionData.

    public override void CreateReferences()
    {
        _displayName = GetValue("DisplayName", 0);
        _isCountry = GetBooleanValue("IsCountry", 0);
    }

    public string GetDisplayName()
    {
        return _displayName;
    }

    public bool GetIsCountry()
    {
        return _isCountry;
    }
}