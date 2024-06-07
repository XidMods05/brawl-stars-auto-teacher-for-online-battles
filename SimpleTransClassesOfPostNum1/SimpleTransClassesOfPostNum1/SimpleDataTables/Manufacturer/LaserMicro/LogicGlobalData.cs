using SimpleTransClassesOfPostNum1.SimpleDataTables.DataSector;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Omniscient;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;

public class LogicGlobalData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private bool _booleanValue;
    private int _numberArray;
    private int _numberValue;
    private string _stringArray = null!;
    private string _textValue = null!;

    // LogicGlobalData.

    public override void CreateReferences()
    {
        _numberValue = GetIntegerValue("NumberValue", 0);
        _booleanValue = GetBooleanValue("BooleanValue", 0);
        _textValue = GetValue("TextValue", 0);
        _stringArray = GetValue("StringArray", 0);
        _numberArray = GetIntegerValue("NumberArray", 0);
    }

    public int GetNumberValue()
    {
        return _numberValue;
    }

    public bool GetBooleanValue()
    {
        return _booleanValue;
    }

    public string GetTextValue()
    {
        return _textValue;
    }

    public string GetStringArray()
    {
        return _stringArray;
    }

    public int GetNumberArray()
    {
        return _numberArray;
    }
}