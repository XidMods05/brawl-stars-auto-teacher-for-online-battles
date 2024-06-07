using SimpleTransClassesOfPostNum1.Configurator;
using SimpleTransClassesOfPostNum1.SimpleKeep;

namespace SimpleTransClassesOfPostNum1.SimpleUtilities;

public static class Debugger
{
    public static void Print(string log)
    {
        ConsoleLogger.Print($"/{++VariablesKeeper.PrintCount} [print] " + log);
    }

    public static void Warning(string log)
    {
        ConsoleLogger.Print($"/{++VariablesKeeper.WarningCount} [warn] " + log);
    }

    public static void Error(string log)
    {
        ConsoleLogger.Print($"/{++VariablesKeeper.ErrorCount} [error] " + log);
    }
}