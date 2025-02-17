using System.Text.RegularExpressions;

class PhoneValidator
{
    public static bool IsValidPhoneNumber(string phone)
    {
        return Regex.IsMatch(phone,@"^\+\d{1,3}\d{9}$");
    }
}

