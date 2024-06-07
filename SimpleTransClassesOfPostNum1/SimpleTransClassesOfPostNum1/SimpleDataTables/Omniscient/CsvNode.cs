using SimpleTransClassesOfPostNum1.SimpleMath.ArrayList;
using SimpleTransClassesOfPostNum1.SimpleUtilities;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables.Omniscient;

public class CsvNode
{
    private readonly string _fileName;
    private CsvTable _table = null!;

    public CsvNode(string[] lines, string fileName)
    {
        _fileName = fileName;

        try
        {
            Load(lines);
        }
        catch (Exception e)
        {
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Info, $"Node filename: {fileName}.");
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Error, e);
        }
    }

    public void Load(string[] lines)
    {
        if (lines.Length <= 1) return;

        _table = new CsvTable(this, lines.Length);
        {
            for (var i = 0; i < ParseLine(lines[0]).Count; i++)
                _table.AddColumn(ParseLine(lines[0])[i]);

            for (var i = 0; i < ParseLine(lines[1]).Count; i++)
            {
                var columnType = -1;
                {
                    if (!string.IsNullOrEmpty(ParseLine(lines[1])[i]))
                    {
                        if (string.Equals(ParseLine(lines[1])[i], "string",
                                StringComparison.InvariantCultureIgnoreCase))
                            columnType = 0;
                        else if (string.Equals(ParseLine(lines[1])[i], "int",
                                     StringComparison.InvariantCultureIgnoreCase))
                            columnType = 1;
                        else if (string.Equals(ParseLine(lines[1])[i], "boolean",
                                     StringComparison.InvariantCultureIgnoreCase))
                            columnType = 2;
                    }
                }

                _table.AddColumnType(columnType);
            }
        }

        _table.ValidateColumnTypes();

        if (lines.Length <= 2) return;
        {
            for (var i = 2; i < lines.Length; i++)
            {
                if (ParseLine(lines[i]).Count <= 0) continue;

                if (!string.IsNullOrEmpty(ParseLine(lines[i])[0]))
                    _table.CreateRow();

                for (var j = 0; j < ParseLine(lines[i]).Count; j++)
                    _table.AddAndConvertValue(ParseLine(lines[i])[j], j);
            }
        }
    }

    public static LogicArrayList<string> ParseLine(string line)
    {
        var fields = new LogicArrayList<string>();

        var inQuote = false;
        var readField = string.Empty;

        for (var i = 0; i < line.Length; i++)
            switch (line[i])
            {
                case '"' when inQuote:
                {
                    if (i + 1 < line.Length && line[i + 1] == '"')
                        readField += line[i];
                    else
                        inQuote = false;

                    break;
                }

                case '"':
                    inQuote = true;
                    break;

                case ',' when !inQuote:
                    fields.Add(readField);
                    readField = string.Empty;
                    break;

                default:
                    readField += line[i];
                    break;
            }

        fields.Add(readField);

        return fields;
    }

    public string GetFileName()
    {
        return _fileName;
    }

    public CsvTable GetTable()
    {
        return _table;
    }
}