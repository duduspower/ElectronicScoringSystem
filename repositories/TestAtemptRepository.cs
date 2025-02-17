using Microsoft.Data.SqlClient;
using System.Data;

public class TestAtemptRepository
{
    DatabaseManager databaseManager;
    private static string ENTITY_NAME = "test_atempt";

    public TestAtemptRepository(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    public List<TestAtempt> getTestResult(int testId) {
        string query = $"SELECT * from {ENTITY_NAME} where test_id = @testId";

        SqlParameter[] parameters = {
            new SqlParameter("@testId", SqlDbType.Int){ Value = testId},
        };

        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeMultipleResult(reader);
    }

    public TestAtempt getTestResultForStudent(Test test, Student student) {
        string query = $"SELECT * from {ENTITY_NAME} where test_id = @testId and student_id = @studentId";

        SqlParameter[] parameters = {
            new SqlParameter("@testId", SqlDbType.Int){ Value = test.id},
            new SqlParameter("@studentId", SqlDbType.Int){ Value = student.id}
        };

        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleResult(reader);
    }

    public void atemptTest(Test test, Student student, int correctAnswears, int incorrectAnswears) {
        string query = $"INSERT INTO {ENTITY_NAME} (name, test_id,correct_answears, incorrect_answears, student_id) VALUES (@name, @test_id, @correct_answears, @incorrect_answears, @student_id)";
        SqlParameter[] parameters = {
            new SqlParameter("@name", SqlDbType.VarChar){ Value = test.name},
            new SqlParameter("@test_id", SqlDbType.Int){ Value = test.id},
            new SqlParameter("@correct_answears", SqlDbType.Int){ Value = correctAnswears},
            new SqlParameter("@incorrect_answears", SqlDbType.Int){ Value = incorrectAnswears},
            new SqlParameter("@student_id", SqlDbType.Int){ Value = student.id}
        };
        databaseManager.executeSaveQuery(query, parameters);
    }

    public List<TestAtempt> getAttendedByStudent(int studentId) {
        string query = $"SELECT * FROM {ENTITY_NAME} where student_id = @studentId";
        SqlParameter[] parameters = {
            new SqlParameter("@studentId", SqlDbType.Int){ Value = studentId}
        };

        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        List<TestAtempt> attempts = decodeMultipleResult(reader);
        return attempts;
    }

    public TestAtempt decodeSingleResult(SqlDataReader reader) {
        if (reader.Read()) {
            return new TestAtempt(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3), reader.GetInt32(4));
        }
        return null;
    }

    public List<TestAtempt> decodeMultipleResult(SqlDataReader reader) {
        List<TestAtempt> atempts = new List<TestAtempt>();
        while (reader.Read())
        {
            atempts.Add(new TestAtempt(reader.GetInt32(0), reader.GetString(1),reader.GetInt32(3), reader.GetInt32(4)));
        }
        return atempts;
    }

}
