

using Microsoft.Data.SqlClient;
using System.Data;

public class StudentsClassRepository
{
    DatabaseManager databaseManager;
    private static string ENTITY_NAME = "students_class";

    public StudentsClassRepository(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    public void addStudentClass(StudentsClass studentClass)
    {
        string query = $"INSERT INTO {ENTITY_NAME}(name,teacher_id) VALUES (@name, @teacherId)";
        SqlParameter[] parameters = {
            new SqlParameter("@name", SqlDbType.VarChar) { Value = studentClass.name },
            new SqlParameter("@teacherId", SqlDbType.Int) { Value = studentClass.teacher.id },
        };

        databaseManager.executeSaveQuery(query, parameters);
    }

    public void addStudentToClass(StudentsClass studentsClass, Student student)
    {
        string query = "INSERT INTO students_class_to_students VALUES(@student_id, @students_class_id)";
        SqlParameter[] parameters = {
            new SqlParameter("@student_id", SqlDbType.Int) { Value = student.id },
            new SqlParameter("@students_class_id", SqlDbType.Int) { Value = studentsClass.id },
        };

        databaseManager.executeSaveQuery(query, parameters);
    }

    public StudentsClass findByName(string name)
    {
        string query = $"SELECT * FROM {ENTITY_NAME} where name = @name";
        SqlParameter[] parameters = {
            new SqlParameter("@name", SqlDbType.VarChar) { Value = name }, };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        if (reader.Read())
        {
            return new StudentsClass(reader.GetInt32(0), reader.GetString(1));

        }
        return null;
    }

    public List<StudentsClass> findByTeacher(int teacherId) {
        string query = $"SELECT * FROM {ENTITY_NAME} where teacher_id = @teacherId";
        SqlParameter[] parameters = {
            new SqlParameter("@teacherId", SqlDbType.VarChar) { Value = teacherId }, };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);

        List<StudentsClass> studentsClasses = new List<StudentsClass>();
        while (reader.Read()) {
            studentsClasses.Add(new StudentsClass(reader.GetInt32(0), reader.GetString(1)));
        }
        return studentsClasses;
    }

    public StudentsClass findById(int classId) {
        string query = $"SELECT * FROM {ENTITY_NAME} where id = @id";

        SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.Int) { Value = classId }, };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);

        if (reader.Read())
        {
            return new StudentsClass(reader.GetInt32(0), reader.GetString(1));
        }
        return null;

    }

    public List<StudentsClass> findByStudent(int student_id) {
        List<int> idsOfClasses = findIdsOfStudentsClassesThatStudentIsPartOf(student_id);
        List<StudentsClass> studentsClasses = new List<StudentsClass>();
        idsOfClasses.ForEach(sc => studentsClasses.Add(findById(sc)));
        return studentsClasses;
    }

    private List<int> findIdsOfStudentsClassesThatStudentIsPartOf(int student_id) {
        List<int> ids = new List<int>();
        string query = $"SELECT * from students_class_to_students where student_id = @studentId";
        SqlParameter[] parameters = {
            new SqlParameter("@studentId", SqlDbType.Int) { Value = student_id }, };
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        while (reader.Read())
        {
            ids.Add(reader.GetInt32(1));
        }
        return ids;
    }

}

