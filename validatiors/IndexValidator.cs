using System.Text.RegularExpressions;

public class IndexValidator
{
    public static bool IsValidIndex(string input)
    {
        string pattern = @"^[A-Za-z]\d{5}$";
        return Regex.IsMatch(input, pattern);
    }
}
