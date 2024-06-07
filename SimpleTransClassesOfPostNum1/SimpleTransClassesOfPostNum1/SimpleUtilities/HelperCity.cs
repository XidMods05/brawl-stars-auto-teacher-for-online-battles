using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;
using SimpleTransClassesOfPostNum1.SimpleKeep;
using SimpleTransClassesOfPostNum1.SimpleMath;
using SimpleTransClassesOfPostNum1.SimpleMath.SectorOfVector;
using SimpleTransClassesOfPostNum1.SimpleUtilities.Regex;

namespace SimpleTransClassesOfPostNum1.SimpleUtilities;

public static class HelperCity
{
    public static void Skip()
    {
    }

    public static void Destructor(object @class)
    {
        var fields = @class.GetType().GetFields();
        {
            foreach (var field in fields)
                try
                {
                    field.SetValue(@class, GetTypeAndSetOfGetDefaultValueToJson(field));
                }
                catch (Exception)
                {
                    // ignored.
                }
        }
    }

    // islands start;
    // island 1 start;
    public static int LocalAttackCounterToGlobalAttackCounter(int attackCounter, int max)
    {
        return LogicMath.Clamp(attackCounter == 0 ? 3 : attackCounter, 0, max);
    }

    public static int AttackAngleExtractMutation(int mutationStart, int mutationCounter, int mutationSpread,
        int mutationCount,
        int mutationStep)
    {
        mutationSpread = -mutationSpread + mutationStep;

        var deltaMutator = mutationSpread != 0 ? -mutationSpread / 4 : 0;
        {
            for (var i = 0; i < mutationCounter; i++)
                deltaMutator += mutationSpread != 0 ? mutationSpread / (2 * mutationCount) : 4;
        }

        return mutationStart + deltaMutator + 360 / 100;
    }

    public static int GetAngleBetweenPositions(LogicVector2 logicVector2X, LogicVector2 logicVector2Y)
    {
        var x = logicVector2X.GetX() - logicVector2Y.GetX();
        var y = logicVector2X.GetY() - logicVector2Y.GetY();

        var angleInDegrees = Math.Atan2(y, x) * 180 / Math.PI;
        {
            if (angleInDegrees < 0) angleInDegrees += 360;
        }

        return (int)angleInDegrees;
    }

    public static int ConvertToRadians(double angle)
    {
        return (int)(Math.PI / 180 * angle);
    }
    // island 1 end;

    // island 2 start;
    public static void OnSend(IAsyncResult iAsyncResult)
    {
        try
        {
            ((Socket)iAsyncResult.AsyncState!).EndSend(iAsyncResult);
        }
        catch (Exception)
        {
            // ignored.
        }
    }

    public static string GetIpFromDomain(string domain)
    {
        return domain[..domain.IndexOf(':')];
    }

    public static int GetPortFromDomain(string domain)
    {
        return Convert.ToInt32(domain[(domain.IndexOf(':') + 1)..]);
    }

    public static IPEndPoint GetFullyEndPointByDomain(string domain)
    {
        return new IPEndPoint(IPAddress.Parse(GetIpFromDomain(domain)), GetPortFromDomain(domain));
    }

    public static string? GetIpBySocket(Socket socket)
    {
        return socket.RemoteEndPoint!.ToString()?
            [..socket.RemoteEndPoint.ToString()!.IndexOf(':')];
    }

    public static int? GetPortBySocket(Socket socket)
    {
        return Convert.ToInt32(socket.RemoteEndPoint!.ToString()?
            [(socket.RemoteEndPoint.ToString()!.IndexOf(':') + 1)..]);
    }
    // island 2 end;

    // island 3 start;
    public static string GenerateRandomString(int length)
    {
        var result = new char[length];
        {
            for (var i = 0; i < result.Length; i++)
                result[i] = ConstantsKeeper.StringCharacters[
                    new Random().Next(ConstantsKeeper.StringCharacters.Length)];
        }

        return new string(result);
    }

    public static string ConvertStringToUnderscore(string input)
    {
        var charArray = input.ToCharArray();
        {
            for (var i = 0; i < charArray.Length; i++)
                if (charArray[i] == '_' && i < charArray.Length - 1)
                {
                    charArray[i] = char.ToUpper(charArray[i + 1]);
                    {
                        Array.Copy(charArray, i + 2, charArray, i + 1, charArray.Length - i - 2);
                        Array.Resize(ref charArray, charArray.Length - 1);
                    }
                }
                else if (i == 0)
                {
                    charArray[i] = char.ToLower(charArray[i]);
                }
        }

        return new string(charArray);
    }

    public static string ConvertStringToCamelCase(string str)
    {
        var result = new StringBuilder();
        {
            var capitalizeNext = false;
            {
                foreach (var currentChar in str)
                    if (currentChar == '_')
                    {
                        capitalizeNext = true;
                    }
                    else
                    {
                        if (capitalizeNext)
                        {
                            result.Append(char.ToUpper(currentChar));
                            capitalizeNext = false;
                        }
                        else
                        {
                            result.Append(char.ToLower(currentChar));
                        }
                    }
            }
        }

        return result.ToString();
    }

    public static byte[] ConvertStringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
    }
    // island 3 end;

    // island 4 start;
    public static string GenerateToken(long id)
    {
        return GenerateRandomString(10) + id * 3 + GenerateRandomString(id < 100 ? 50 : 100);
    }

    public static string GenerateScIdToken(long id)
    {
        return "#SC-" + "PU" + id + "/" + GenerateRandomString(10) + id * 3 + ":";
    }
    // island 4 end;

    // island 5 start;
    public static bool GetIsAdequateString(string name)
    {
        return !name.Contains("tg", StringComparison.CurrentCultureIgnoreCase) &&
               !name.Contains("ddos", StringComparison.CurrentCultureIgnoreCase);
    }
    // island 5 end;

    // island 6 start;
    public static bool GetIsCorrectName(string nameForCheck)
    {
        return nameForCheck.Length is > 3 and <= 15;
    }

    public static int GetChangeNameCostByCount(int count)
    {
        return Math.Min(Math.Max(count, 0) * 30, 120);
    }
    // island 6 end;

    // island 7 start;
    public static int GenerateRandomIntForBetween(int min, int max)
    {
        return new Random().Next(min, max + 1);
    }

    public static bool GetChanceByPercentage(int percentage)
    {
        return new Random().Next(0, 100) <= percentage;
    }
    // island 7 end;

    // island 8 start;
    public static string? GetPacketNameByType(int type)
    {
        return JObject.Parse(ConstantsKeeper.PacketInfo).ToObject<Dictionary<string, string>>()!.ContainsKey(
            type.ToString())
            ? JObject.Parse(ConstantsKeeper.PacketInfo)[type.ToString()]?.ToString()
            : "NULL-NAME-WARNING";
    }

    public static int GetPacketTypeByName(string name)
    {
        var jsonObject = JObject.Parse(ConstantsKeeper.PacketInfo);

        foreach (var pair in jsonObject)
            if (pair.Value!.ToString() == name)
                return int.Parse(pair.Key);

        return 0;
    }
    // island 8 end;

    // island 9 start;
    public static LogicVector2 SmoothPosition(Vector2 currentPosition, Vector2 targetPosition, float smoothFactor)
    {
        var v1 = Vector2.Lerp(currentPosition, targetPosition, smoothFactor);
        return new LogicVector2(v1);
    }

    public static LogicVector2 SmoothPosition(LogicVector2 currentPosition, LogicVector2 targetPosition,
        float smoothFactor)
    {
        var v1 = Vector2.Lerp(new Vector2(currentPosition.GetX(), currentPosition.GetY()),
            new Vector2(targetPosition.GetX(), targetPosition.GetY()), smoothFactor);
        return new LogicVector2(v1);
    }

    public static float Lerp(int a, int b, int t)
    {
        return a + (b - a) * t;
    }
    // island 9 end;

    // island 10 start;
    public static object GetTypeAndSetOfGetDefaultValueToJson(FieldInfo field)
    {
        try
        {
            if (field.FieldType == typeof(string)) return "NULL";
            if (field.FieldType == typeof(int) || field.FieldType == typeof(long) ||
                field.FieldType == typeof(byte)) return -1;
            if (field.FieldType == typeof(bool)) return false;
            if (field.FieldType == typeof(double)) return (double)-1;
            if (field.FieldType == typeof(short)) return (short)-1;
            if (field.FieldType == typeof(float)) return -1f;
            if (field.FieldType == typeof(char)) return '\u0000';
            if (field.FieldType == typeof(Dictionary<object, object>)) return new Dictionary<object, object>();

            if (field.FieldType.IsArray)
            {
                var componentType = field.FieldType.GetElementType();
                {
                    if (componentType == typeof(int)) return Array.Empty<int>();
                    if (componentType == typeof(string)) return Array.Empty<string>();
                    if (componentType == typeof(char)) return Array.Empty<char>();
                    if (componentType == typeof(long)) return Array.Empty<long>();
                    if (componentType == typeof(byte)) return Array.Empty<byte>();
                    if (componentType == typeof(Dictionary<object, object>)) return new Dictionary<object, object>();
                }
            }
        }
        catch (Exception)
        {
            // ignored.
        }

        return null!;
    }

    public static List<int> SumRepeatedElements(IEnumerable<int> inputList)
    {
        var enumerable = inputList as int[] ?? inputList.ToArray();
        {
            if (enumerable.Length < 3) return enumerable.ToList();
        }

        var elementSumDictionary = new Dictionary<int, int>();

        foreach (var number in enumerable.ToList().Where(number => !elementSumDictionary.TryAdd(number, number)))
            elementSumDictionary[number] += number;
        return elementSumDictionary.Values.ToList();
    }

    public static string FixAutoimmuneFilePath(string appDomainBasePath)
    {
        return Regexes.NetPathRegexVestibular().Replace(appDomainBasePath.Replace("\\", "/"), string.Empty) + "/";
    }

    public static Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>
        (Dictionary<TKey, TValue> original) where TValue : ICloneable where TKey : notnull
    {
        var ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);
        {
            foreach (var entry in original) ret.Add(entry.Key, (TValue)entry.Value.Clone());
        }

        return ret;
    }
    // island 10 end;
    // islands end;
}