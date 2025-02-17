
using Microsoft.Data.SqlClient;
using System.Data;

public class QuestionRepository
{
    DatabaseManager databaseManager;
    private static string ENTITY_NAME = "question";

    public QuestionRepository(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    public int addQuestion(Question question) {
        string query = $"INSERT INTO {ENTITY_NAME} (value,correct_answear_value) VALUES (@value, @correct_answear);SELECT SCOPE_IDENTITY();";

        SqlParameter[] parameters = {
            new SqlParameter("@value", SqlDbType.VarChar) { Value = question.value },
            new SqlParameter("@correct_answear", SqlDbType.VarChar) { Value = question.correctAnswearValue },
        };
        return databaseManager.executeSaveQuery(query, parameters);
    }

    public void addQuestionTestRelation(int questionId, int testId) {
        string query = $"INSERT INTO test_to_questions (question_id,test_id) VALUES (@questionId, @testId)";
        SqlParameter[] parameters = {
            new SqlParameter("@questionId", SqlDbType.Int) { Value = questionId },
            new SqlParameter("@testId", SqlDbType.Int) { Value = testId },
        };
        databaseManager.executeSaveQuery(query, parameters);
    }

    public void addQuestionAnswearRelation(int questionId, int answearId) {
        string query = $"INSERT INTO question_to_answears (question_id,answear_id) VALUES (@questionId, @answearId)";
        SqlParameter[] parameters = {
            new SqlParameter("@questionId", SqlDbType.Int) { Value = questionId },
            new SqlParameter("@answearId", SqlDbType.Int) { Value = answearId },
        };
        databaseManager.executeSaveQuery(query, parameters);
    }

    public List<Question> findByTestId(int testId) {
        string query = $"SELECT * FROM test_to_questions where test_id = @testId";
        SqlParameter[] parameters = {
            new SqlParameter("@testId", SqlDbType.Int){ Value = testId}
        };

        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        List<int> ids = new List<int>();
        while (reader.Read()) {
            ids.Add(reader.GetInt32(0));
        }
        List<Question> results = new List<Question>();
        ids.ForEach(id => results.Add(findById(id)));
        return results;
    }

    public Question findById(int id) {
        string query = $"SELECT * FROM {ENTITY_NAME} where id = @id";
        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int) { Value = id },
        };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleFromReader(reader);
    }

    private Question decodeSingleFromReader(SqlDataReader reader)
    {
        if (reader.Read())
        {
            return new Question
             (
                 reader.GetInt32(0),
                 reader.GetString(1),
                 reader.GetString(2)
             );
        }
        return null;
    }

}
