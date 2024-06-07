using System.Text;

namespace SimpleTransClassesOfPostNum1.SimpleUtilities;

public abstract class ConsoleLogger
{
    public enum Prefixes
    {
        Battle = 10,
        Cmd = 13,
        Debug = 1,
        Error = 7,
        Info = 2,
        Load = 4,
        Lobby = 9,
        Receive = 17,
        Restart = 12,
        Return = 14,
        Returned = 16,
        Send = 15,
        Start = 20,
        Stop = 3,
        Tcp = 5,
        Udp = 6,
        Unload = 11,
        Warn = 8
    }

    public static readonly Dictionary<Prefixes, ConsoleColor> ColorFramePrefixes = new()
    {
        { Prefixes.Battle, ConsoleColor.Magenta },
        { Prefixes.Cmd, ConsoleColor.DarkRed },
        { Prefixes.Debug, ConsoleColor.Gray },
        { Prefixes.Error, ConsoleColor.Red },
        { Prefixes.Info, ConsoleColor.Green },
        { Prefixes.Lobby, ConsoleColor.DarkMagenta },
        { Prefixes.Load, ConsoleColor.Black },
        { Prefixes.Receive, ConsoleColor.DarkGray },
        { Prefixes.Restart, ConsoleColor.DarkGreen },
        { Prefixes.Return, ConsoleColor.DarkCyan },
        { Prefixes.Returned, ConsoleColor.DarkCyan },
        { Prefixes.Send, ConsoleColor.Cyan },
        { Prefixes.Start, ConsoleColor.Blue },
        { Prefixes.Stop, ConsoleColor.DarkBlue },
        { Prefixes.Tcp, ConsoleColor.White },
        { Prefixes.Udp, ConsoleColor.White },
        { Prefixes.Unload, ConsoleColor.Black },
        { Prefixes.Warn, ConsoleColor.Yellow }
    };

    public static readonly Dictionary<Prefixes, int> PrefixLvlImportance = new()
    {
        { Prefixes.Battle, 1 },
        { Prefixes.Cmd, 3 },
        { Prefixes.Debug, 2 },
        { Prefixes.Error, 3 },
        { Prefixes.Info, 2 },
        { Prefixes.Lobby, 1 },
        { Prefixes.Load, 3 },
        { Prefixes.Receive, 1 },
        { Prefixes.Restart, 3 },
        { Prefixes.Return, 2 },
        { Prefixes.Returned, 2 },
        { Prefixes.Send, 1 },
        { Prefixes.Start, 3 },
        { Prefixes.Stop, 3 },
        { Prefixes.Tcp, 2 },
        { Prefixes.Udp, 2 },
        { Prefixes.Unload, 3 },
        { Prefixes.Warn, 3 }
    };

    public static readonly char[] EndSymbols =
    [
        '.',
        '!',
        '?',
        '@',
        '#',
        '$',
        ';',
        '^',
        '%',
        ')',
        '&',
        '-',
        '_',
        '*',
        '/',
        '<',
        '>',
        '|',
        '~',
        '`'
    ];

    public static void Print(object? text, string? prefix = null)
    {
        var data = text?.ToString()?.ToCharArray()!;

        try
        {
            if (EndSymbols.Contains(data[text!.ToString()!.Length - 1]))
                Console.WriteLine($"[{prefix?.ToUpper()}] {text}");
            else if (text != null!)
                Console.WriteLine($"[{prefix?.ToUpper()}] {text}.");
            else Console.WriteLine(".unfinished-log.");

            Console.ResetColor();
        }
        catch
        {
            // ignored.
        }
    }

    public static void WriteTextWithPrefix(Prefixes prefix = default, object? element = null, bool centring = true,
        Encoding? encoding = null)
    {
        if (encoding != null) Console.OutputEncoding = encoding;
        if (prefix != default) Console.ForegroundColor = ColorFramePrefixes[prefix];
        else return;

        if (centring && element?.ToString()!.Length < Console.WindowWidth)
            Console.SetCursorPosition((Console.WindowWidth - element.ToString()!.Length) / 2, Console.CursorTop);

        Print(element, Enum.GetName(typeof(Prefixes), prefix));
    }
}