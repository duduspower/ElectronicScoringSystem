using System.Text.RegularExpressions;

public class LoginValidator
{
    public static bool IsValidLogin(string login)
    {
        return Regex.IsMatch(login, @"^[a-zA-Z0-9_-]{3,20}$");
    }
}

