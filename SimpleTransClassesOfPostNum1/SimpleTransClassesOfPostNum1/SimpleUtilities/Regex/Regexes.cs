using System.Text.RegularExpressions;

namespace SimpleTransClassesOfPostNum1.SimpleUtilities.Regex;

public static partial class Regexes
{
    [GeneratedRegex(@"/bin/Debug/net\d+\.\d+/")]
    public static partial System.Text.RegularExpressions.Regex NetPathRegexVestibular();

    [GeneratedRegex("[a-z]+[A-Za-z]*")]
    public static partial System.Text.RegularExpressions.Regex FieldRegexVestibular();
}