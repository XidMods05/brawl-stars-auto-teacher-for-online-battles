using SimpleTransClassesOfPostNum1.SimpleMath.ArrayList;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables.Omniscient;

public class CsvColumn
{
    private readonly LogicArrayList<byte> _boolValue;
    private readonly LogicArrayList<int> _intValue;
    private readonly LogicArrayList<string> _stringValue;

    public CsvColumn(int type, int size)
    {
        ColumnType = type;

        _stringValue = new LogicArrayList<string>();
        _intValue = new LogicArrayList<int>();
        _boolValue = new LogicArrayList<byte>();

        _ = type switch
        {
            <= 0 => _stringValue.EnsureCapacity(size),
            1 => _intValue.EnsureCapacity(size),
            _ => _boolValue.EnsureCapacity(size)
        };
    }

    public int ColumnType { get; }

    public void AddEmptyValue()
    {
        switch (ColumnType)
        {
            case <= 0:
                _stringValue.Add(string.Empty);
                break;
            case 1:
                _intValue.Add(0x7fffffff);
                break;
            default:
                _boolValue.Add(2);
                break;
        }
    }

    public void AddStringValue(string value)
    {
        _stringValue.Add(value);
    }

    public void AddIntegerValue(int value)
    {
        _intValue.Add(value);
    }

    public void AddBooleanValue(bool value)
    {
        _boolValue.Add((byte)(value ? 1 : 0));
    }

    public string GetStringValue(int index)
    {
        return _stringValue[index];
    }

    public int GetIntegerValue(int index)
    {
        return _intValue[index];
    }

    public bool GetBooleanValue(int index)
    {
        return _boolValue[index] == 1;
    }

    public int GetArraySize(int startOffset, int endOffset)
    {
        switch (ColumnType)
        {
            case <= 0:
                for (var i = endOffset - 1; i + 1 > startOffset; i--)
                    if (_stringValue[i].Length > 0)
                        return i - startOffset + 1;

                break;

            case 1:
                for (var i = endOffset - 1; i + 1 > startOffset; i--)
                    if (_intValue[i] != 0x7fffffff)
                        return i - startOffset + 1;

                break;

            default:
                for (var i = endOffset - 1; i + 1 > startOffset; i--)
                    if (_boolValue[i] != 2)
                        return i - startOffset + 1;

                break;
        }

        return 0;
    }

    public int GetSize()
    {
        return ColumnType switch
        {
            <= 0 => _stringValue.Count,
            1 => _intValue.Count,
            2 => _boolValue.Count,
            _ => 0
        };
    }
}