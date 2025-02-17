using Microsoft.Data.SqlClient;
using System.Data;

public class TeacherRepository
{
    DatabaseManager databaseManager;
    AuthRepository authRepository;
    private string ENTITY_NAME = "teacher";

    public TeacherRepository(DatabaseManager databaseManager, AuthRepository authRepository)
    {
        this.databaseManager = databaseManager;
        this.authRepository = authRepository;
    }
    public void addTeacher(Teacher teacher)
    {
        authRepository.addLogin(teacher.login);
        Login login = authRepository.getLoginByLogin(teacher.login.login);

        string query = $@"
            INSERT INTO {ENTITY_NAME} (name, surname, academic_title, email, phone, login_id)
            VALUES (@name, @surname, @academic_title, @email, @phone, @login_id)";

        SqlParameter[] parameters = {
            new SqlParameter("@name", SqlDbType.VarChar) { Value = teacher.name },
            new SqlParameter("@surname", SqlDbType.VarChar) { Value = teacher.surname },
            new SqlParameter("@academic_title", SqlDbType.VarChar) { Value = teacher.academicTitle },
            new SqlParameter("@email", SqlDbType.VarChar) { Value = teacher.email },
            new SqlParameter("@phone", SqlDbType.Char) { Value = teacher.phone },
            new SqlParameter("@login_id", SqlDbType.Int) { Value = login.id }
        };
        databaseManager.executeSaveQuery(query, parameters);
    }

    public void updateTeacher(Teacher teacher, int id)
    {
        string query = $@"
            UPDATE {ENTITY_NAME} 
            SET name = @name, surname = @surname, academic_title = @academic_title, 
                email = @email, phone = @phone
            WHERE id = @id";

        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int) { Value = teacher.id },
            new SqlParameter("@name", SqlDbType.VarChar) { Value = teacher.name },
            new SqlParameter("@surname", SqlDbType.VarChar) { Value = teacher.surname },
            new SqlParameter("@academic_title", SqlDbType.VarChar) { Value = teacher.academicTitle },
            new SqlParameter("@email", SqlDbType.VarChar) { Value = teacher.email },
            new SqlParameter("@phone", SqlDbType.Char) { Value = teacher.phone }
        };

        databaseManager.executeSaveQuery(query, parameters);
    }

    public void deleteTeacher(int id)
    {
        string query = $"DELETE FROM {ENTITY_NAME} WHERE id = @id";

        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int) { Value = id }
        };

        databaseManager.executeSaveQuery(query, parameters);
    }
    public List<Teacher> findAllTeachers()
    {
        string query = $"SELECT * FROM {ENTITY_NAME}";
        SqlDataReader reader = databaseManager.executeSelectQuery(query, null);
        return decodeMultipleFromReader(reader);
    }

    public Teacher findByLoginId(int loginId)
    {
        string query = $"SELECT * FROM {ENTITY_NAME} WHERE login_id = @login_id";

        SqlParameter[] parameters = {
            new SqlParameter("@login_id", SqlDbType.Int) { Value = loginId }
        };

        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleFromReader(reader);
    }

    public Teacher findBySurname(string surname)
    {
        string query = $"SELECT * FROM {ENTITY_NAME} WHERE surname = @surname";

        SqlParameter[] parameters = {
            new SqlParameter("@surname", SqlDbType.VarChar) { Value = surname }
        };

        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleFromReader(reader);
    }

    public Teacher findById(int id)
    {
        string query = $"SELECT * FROM {ENTITY_NAME} WHERE id = @id";

        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int) { Value = id }
        };

        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleFromReader(reader);
    }

    private List<Teacher> decodeMultipleFromReader(SqlDataReader reader)
    {
        List<Teacher> teachers = new List<Teacher>();
        while (reader.Read())
        {
            teachers.Add(decodeTeacher(reader));
        }
        return teachers;
    }

    private Teacher decodeSingleFromReader(SqlDataReader reader)
    {
        return reader.Read() ? decodeTeacher(reader) : null;
    }

    private Teacher decodeTeacher(SqlDataReader reader) {
        Login login = authRepository.getLoginById(reader.GetInt32(6));
        return new Teacher(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetString(4),
            reader.GetString(5),
            new List<StudentsClass>(),
            login
        );
    }

}
