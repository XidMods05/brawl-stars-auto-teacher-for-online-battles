namespace SimpleTransClassesOfPostNum1.SimpleKeep;

public static class VariablesKeeper
{
    public static int PrintCount;
    public static int WarningCount;
    public static int ErrorCount;
    public static string PathToSavedFiles = "";
    public static readonly Dictionary<string, List<object>> MapStructureDictionary = new();
}