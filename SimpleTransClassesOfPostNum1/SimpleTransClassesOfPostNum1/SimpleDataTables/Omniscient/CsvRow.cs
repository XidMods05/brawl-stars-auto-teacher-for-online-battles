using SimpleTransClassesOfPostNum1.SimpleMath;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables.Omniscient;

public class CsvRow(CsvTable table)
{
    private readonly int _rowOffset = table.GetColumnRowCount();

    public string GetValue(string columnName, int index)
    {
        return table.GetValue(columnName, _rowOffset + index);
    }

    public string GetValueAt(int columnIndex, int index)
    {
        return table.GetValueAt(columnIndex, _rowOffset + index);
    }

    public string GetClampedValue(string columnName, int index)
    {
        if (GetColumnIndexByName(columnName) == -1) return string.Empty;

        var arraySize = table.GetArraySizeAt(this, GetColumnIndexByName(columnName));
        if (index >= arraySize || arraySize < 1) index = LogicMath.Max(arraySize - 1, 0);

        return table.GetValueAt(GetColumnIndexByName(columnName), _rowOffset + index);
    }

    public int GetIntegerValue(string columnName, int index)
    {
        return table.GetIntegerValue(columnName, _rowOffset + index);
    }

    public int GetIntegerValueAt(int columnIndex, int index)
    {
        return table.GetIntegerValueAt(columnIndex, _rowOffset + index);
    }

    public int GetClampedIntegerValue(string columnName, int index)
    {
        if (GetColumnIndexByName(columnName) == -1) return 0;
        var arraySize = table.GetArraySizeAt(this, GetColumnIndexByName(columnName));

        if (index >= arraySize || arraySize < 1) index = LogicMath.Max(arraySize - 1, 0);
        return table.GetIntegerValueAt(GetColumnIndexByName(columnName), _rowOffset + index);
    }

    public bool GetBooleanValue(string columnName, int index)
    {
        return table.GetBooleanValue(columnName, _rowOffset + index);
    }

    public bool GetBooleanValueAt(int columnIndex, int index)
    {
        return table.GetBooleanValueAt(columnIndex, _rowOffset + index);
    }

    public bool GetClampedBooleanValue(string columnName, int index)
    {
        if (GetColumnIndexByName(columnName) == -1) return false;

        var arraySize = table.GetArraySizeAt(this, GetColumnIndexByName(columnName));
        if (index >= arraySize || arraySize < 1) index = LogicMath.Max(arraySize - 1, 0);

        return table.GetBooleanValueAt(GetColumnIndexByName(columnName), _rowOffset + index);
    }

    public int GetColumnCount()
    {
        return table.GetColumnCount();
    }

    public int GetColumnIndexByName(string name)
    {
        return table.GetColumnIndexByName(name);
    }

    public int GetBiggestArraySize()
    {
        var maxSize = 0;
        {
            for (var i = 0; i < table.GetColumnCount(); i++)
                maxSize = LogicMath.Max(table.GetArraySizeAt(this, i), maxSize);
        }

        return maxSize;
    }

    public string GetName()
    {
        return table.GetValueAt(0, _rowOffset);
    }

    public int GetRowOffset()
    {
        return _rowOffset;
    }
}