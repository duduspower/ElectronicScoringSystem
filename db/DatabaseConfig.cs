public class DatabaseConfig
{
    private static string DB_NAME = "scoring_system";
    public readonly static string CONNECTION_STRING = $"Server=localhost;Database={DB_NAME};Integrated Security=True;Encrypt=False;TrustServerCertificate=False;";
}
