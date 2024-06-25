using System;
using System.Text;
using System.Text.RegularExpressions;

public class HexUtils
{
    public static bool IsHexadecimal(string value)
    {
        return Regex.IsMatch(value, @"\A\b0x[0-9a-fA-F]+\b\Z");
    }

    public static string ConvertHex(string message)
    {
        var serialized = BitConverter.ToString(Encoding.Default.GetBytes(message));
        serialized = serialized.Replace("-", "");
        return "0x" + serialized;
    }
}