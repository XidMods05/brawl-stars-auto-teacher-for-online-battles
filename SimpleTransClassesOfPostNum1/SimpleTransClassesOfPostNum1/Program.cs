using System.Text;
using Newtonsoft.Json.Linq;
using SimpleTransClassesOfPostNum1.Configurator;
using SimpleTransClassesOfPostNum1.SimpleDataTables;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;
using SimpleTransClassesOfPostNum1.SimpleHTables;
using SimpleTransClassesOfPostNum1.SimpleKeep;
using SimpleTransClassesOfPostNum1.SimpleUtilities;

namespace SimpleTransClassesOfPostNum1;

public static class Program
{
    public static void Main(string[] args)
    {
        Directory.SetCurrentDirectory(AppContext.BaseDirectory);

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        
        Initialize();
        
        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Cmd, "Hello v41 user!");

        if (!Configuration.LocalCsvFileSavePath)
            LogicDataTables.CreateReferences(VariablesKeeper.PathToSavedFiles + "Assets/csv_logic/");
        else
            LogicDataTables.CreateReferences("Files/Assets/csv_logic/");
        
        InitializeMaps();

        while (true)
        {
            // delta unClosed code;
        }
    }

    public static void Initialize()
    {
        VariablesKeeper.PathToSavedFiles = HelperCity.FixAutoimmuneFilePath(AppDomain.CurrentDomain.BaseDirectory) + "/Files/";
    }
    
    private static void InitializeMaps()
    {
        var obj = File.ReadAllText("Files/map_structure.json");
        var mapDataList = JArray.Parse(obj);

        foreach (var jToken in mapDataList)
        {
            try
            {
                var mapData = (JObject)jToken;
                var name = "";
                {
                    foreach (var data in LogicDataTables.GetAllDataFromCsvById(CsvFilesHelperTable.Locations.GetId()))
                    {
                        var d1 = (LogicLocationData)data;
                        if (mapData.GetValue("Map")!.ToString() == d1.GetMap()) name = d1.GetLocationTheme();
                    }
                }

                var a1 = LogicDataTables.GetLocationThemeByName(name);
                VariablesKeeper.MapStructureDictionary.Add(
                    mapData.GetValue("Map")!.ToString(),
                    [a1.GetMapHeight(), a1.GetMapWidth(), mapData.GetValue("Data")!.ToString(), mapData.GetValue("MetaData")!.ToString()]);
            }
            catch
            {
                // unsecured;
            }
        }
    }
}