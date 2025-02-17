using Microsoft.Data.SqlClient;
using System.Data;

public class AnswearRepository
{
    DatabaseManager databaseManager;
    private static string ENTITY_NAME = "answear";

    public AnswearRepository(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    public int addAnsware(Answear answear)
    {
        string query = $"INSERT INTO {ENTITY_NAME} (value) VALUES (@value);SELECT SCOPE_IDENTITY();";

        SqlParameter[] parameters = {
            new SqlParameter("@value", SqlDbType.VarChar) { Value = answear.value },
        };

        return databaseManager.executeSaveQuery(query, parameters);
    }

    public void addQuestionAnswearRelation(int questionId, int answearId) {
        string query = $"INSERT INTO question_to_answears (answear_id,question_id) VALUES (@answearId, @questionId)";

        SqlParameter[] parameters = {
            new SqlParameter("@answearId", SqlDbType.Int) { Value = answearId },
            new SqlParameter("@questionId", SqlDbType.Int) { Value = questionId },
        };

        databaseManager.executeSaveQuery(query, parameters);
    }



    public Answear findById(int id)
    {
        string query = $"SELECT * FROM {ENTITY_NAME} where id = @id";
        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int) { Value = id },
        };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleFromReader(reader);
    }

    public Answear findByValueAndQuesionId(string value, int questionId) {
        string query = $"SELECT * FROM {ENTITY_NAME} a inner join question_to_answears qta on qta.answear_id=a.id where a.value = @value and qta.question_id = @questionId";
        SqlParameter[] parameters = {
            new SqlParameter("@value", SqlDbType.VarChar) { Value = value },
            new SqlParameter("@questionId", SqlDbType.Int) { Value = questionId },
        };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleFromReader(reader);
    }

    public List<Answear> findByQuestionId(int questionId) {
        string query = $"SELECT * FROM {ENTITY_NAME} a inner join question_to_answears qta on qta.answear_id=a.id where qta.question_id = @questionId";
        SqlParameter[] parameters = {
            new SqlParameter("@questionId", SqlDbType.Int) { Value = questionId },
        };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        List<Answear> answears = new List<Answear>();
        while (reader.Read()) {
            answears.Add(new Answear
             (
                 reader.GetInt32(0),
                 reader.GetString(1)
             ));
        }
        return answears;
    }

    private Answear decodeSingleFromReader(SqlDataReader reader)
    {
        if (reader.Read())
        {
            return new Answear
             (
                 reader.GetInt32(0),
                 reader.GetString(1)
             );
        }
        return null;
    }
}
