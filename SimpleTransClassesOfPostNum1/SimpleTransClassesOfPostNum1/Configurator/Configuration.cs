using Newtonsoft.Json.Linq;
using SimpleTransClassesOfPostNum1.SimpleKeep;

namespace SimpleTransClassesOfPostNum1.Configurator;

public static class Configuration
{
    static Configuration()
    {
        var config = JObject.Parse(File.ReadAllText(VariablesKeeper.PathToSavedFiles + "conf_structure.json"));
        
        LocalCsvFileSavePath = ((bool)config["localCsvFileSavePath"]!)!;
    }
    
    public static bool LocalCsvFileSavePath { get; }
}