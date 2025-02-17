using Microsoft.Data.SqlClient;
using System.Data;

public class DatabaseManager
{
    public int executeSaveQuery(string query, SqlParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(DatabaseConfig.CONNECTION_STRING))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters.Length != 0)
                {
                    command.Parameters.AddRange(parameters);
                }
                object insertedId = command.ExecuteScalar();
                int newId = Convert.ToInt32(insertedId);
                return newId;
            }
        }
    }

    public SqlDataReader executeSelectQuery(string query, SqlParameter[] parameters)
    {
        //CommandBehavior.CloseConnection;
        SqlConnection connection = new SqlConnection(DatabaseConfig.CONNECTION_STRING);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        if (parameters != null && parameters.Length != 0) {
            command.Parameters.AddRange(parameters);
        }
        return command.ExecuteReader();
    }
}
