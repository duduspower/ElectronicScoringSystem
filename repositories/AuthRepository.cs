using Microsoft.Data.SqlClient;
using System.Data;

public class AuthRepository
{
    private DatabaseManager databaseManager;
    private static string ENTITY_NAME = "login";

    public AuthRepository(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    public Login getLoginByLogin(string login)
    {
        string query = $"Select * from {ENTITY_NAME} where login = @login";
        SqlParameter[] parameters = {
            new SqlParameter("@login", SqlDbType.VarChar) { Value = login },
        };
        SqlDataReader reader = databaseManager.executeSelectQuery(query,parameters);
        return decodeSingleResult(reader);
    }

    public Login getLoginById(int id)
    {
        string query = $"Select * from {ENTITY_NAME} where id = @id";
        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.VarChar) { Value = id },
        };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleResult(reader);
    }

    private Login decodeSingleResult(SqlDataReader reader) {
        if (reader.Read())
        {
            int id = reader.GetInt32(0);
            string entityLogin = reader.GetString(1);
            string entityPassword = reader.GetString(2);
            return new Login(id, entityLogin, entityPassword);
        }
        return null;
    }

    public void addLogin(Login login) {
        string query = $"INSERT INTO {ENTITY_NAME}(login,password) VALUES(@login,@password)";
        SqlParameter[] parameters = {
            new SqlParameter("@login", SqlDbType.VarChar) { Value = login.login },
            new SqlParameter("@password", SqlDbType.VarChar) { Value =login.password }
        };
        databaseManager.executeSaveQuery(query,parameters);
    }

    public void updateLogin(Login login) {
        string query = $"UPDATE {ENTITY_NAME} set login = @login, password = @password where id = @id";
        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int){ Value = login.id},
            new SqlParameter("@login", SqlDbType.VarChar) { Value = login.login },
            new SqlParameter("@password", SqlDbType.VarChar) { Value = login.password }
        };
        databaseManager.executeSaveQuery(query,parameters);
    }

    public void deleteLogin(int login_id) {
        string query = $"DELETE FROM {ENTITY_NAME} where id = @id";
        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int){ Value = login_id}
        };
        databaseManager.executeSaveQuery(query, parameters);
    }
}
