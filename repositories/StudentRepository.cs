using Microsoft.Data.SqlClient;
using System.Data;

public class StudentRepository
{

    private DatabaseManager databaseManager;
    private AuthRepository authRepository;
    private static string ENTITY_NAME = "student";

    public StudentRepository(DatabaseManager databaseManager, AuthRepository authRepository)
    {
        this.databaseManager = databaseManager;
        this.authRepository = authRepository;
    }

    public Student findByLoginId(int login_id)
    {
        string query = $"SELECT * FROM {ENTITY_NAME} where login_id = @loginId";
        SqlParameter[] parameters = {
            new SqlParameter("@loginId", SqlDbType.Int) { Value = login_id },
        };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleFromReader(reader);
    }

    public void addStudent(Student student)
    {
        string query = $@"
            INSERT INTO {ENTITY_NAME} (name, surname, date_of_birth, email, phone, student_index, login_id)
            VALUES (@name, @surname, @date_of_birth, @email, @phone, @student_index, @login_id)";

        authRepository.addLogin(student.login);
        Login login = authRepository.getLoginByLogin(student.login.login);

        SqlParameter[] parameters = {
            new SqlParameter("@name", SqlDbType.VarChar) { Value = student.name },
            new SqlParameter("@surname", SqlDbType.VarChar) { Value = student.surname },
            new SqlParameter("@date_of_birth", SqlDbType.Date) { Value = student.dateOfBirth },
            new SqlParameter("@email", SqlDbType.VarChar) { Value = student.email },
            new SqlParameter("@phone", SqlDbType.Char) { Value = student.phone },
            new SqlParameter("@student_index", SqlDbType.Char) { Value = student.index },
            new SqlParameter("@login_id", SqlDbType.Int) { Value = login.id }
        };

        databaseManager.executeSaveQuery(query, parameters);
    }

    public void updateStudent(Student student, int id)
    {
        string query = $@"
            UPDATE {ENTITY_NAME} 
            SET name = @name, surname = @surname, date_of_birth = @date_of_birth, 
                email = @email, phone = @phone, student_index = @student_index
            WHERE id = @id";

        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int) { Value = id },
            new SqlParameter("@name", SqlDbType.VarChar) { Value = student.name },
            new SqlParameter("@surname", SqlDbType.VarChar) { Value = student.surname },
            new SqlParameter("@date_of_birth", SqlDbType.Date) { Value = student.dateOfBirth },
            new SqlParameter("@email", SqlDbType.VarChar) { Value = student.email },
            new SqlParameter("@phone", SqlDbType.Char) { Value = student.phone },
            new SqlParameter("@student_index", SqlDbType.Char) { Value = student.index }
        };
        databaseManager.executeSaveQuery(query, parameters);
    }

    public void deleteStudent(int id)
    {
        string query = $"DELETE FROM {ENTITY_NAME} where id = @id";
        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int) { Value = id},
        };
        databaseManager.executeSaveQuery(query, parameters);
    }
    public List<Student> findAllStudents()
    {
        string query = $"SELECT * FROM {ENTITY_NAME}";
        SqlDataReader reader = databaseManager.executeSelectQuery(query, null);
        return decodeMultipleFromReader(reader);
    }

    public Student findBySurname(string surname)
    {
        string query = $"SELECT * FROM {ENTITY_NAME} where surname = @surname";
        SqlParameter[] parameters = {
            new SqlParameter("@surname", SqlDbType.Int) { Value = surname},
        };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleFromReader(reader);
    }

    public Student findById(int id)
    {
        string query = $"SELECT * FROM {ENTITY_NAME} where id = @id";
        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int) { Value = id},
        };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        return decodeSingleFromReader(reader);
    }

    private Student decodeSingleFromReader(SqlDataReader reader)
    {
        if (reader.Read())
        {
            Login login = authRepository.getLoginById(reader.GetInt32(7));
            Student student = new Student
            (
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                new DateOnly(),
               reader.GetString(4),
               reader.GetString(5),
               reader.GetString(6),
               login
            );
            return student;
        }
        return null;
    }

    private List<Student> decodeMultipleFromReader(SqlDataReader reader)
    {
        List<Student> students = new List<Student>();
        while (reader.Read())
        {
            Login login = authRepository.getLoginById(reader.GetInt32(7));
            Student student = new Student
            (
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                new DateOnly(),
               reader.GetString(4),
               reader.GetString(5),
               reader.GetString(6),
               login
            );
            students.Add(student);
        }
        return students;
    }
}
