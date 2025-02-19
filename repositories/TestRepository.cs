using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

public class TestRepository
{
    DatabaseManager databaseManager;
    QuestionRepository questionRepository;
    StudentsClassRepository studentsClassRepository;
    TestAtemptRepository testAtemptRepository;
    AnswearRepository answearRepository;
    private static string ENTITY_NAME = "test";

    public TestRepository(DatabaseManager databaseManager, QuestionRepository questionRepository, StudentsClassRepository studentsClassRepository, TestAtemptRepository testAtemptRepository, AnswearRepository answearRepository)
    {
        this.databaseManager = databaseManager;
        this.questionRepository = questionRepository;
        this.studentsClassRepository = studentsClassRepository;
        this.testAtemptRepository = testAtemptRepository;
        this.answearRepository = answearRepository;
    }

    public TestRepository(DatabaseManager databaseManager, QuestionRepository questionRepository, StudentsClassRepository studentsClassRepository, AnswearRepository answearRepository)
    {
        this.databaseManager = databaseManager;
        this.questionRepository = questionRepository;
        this.studentsClassRepository = studentsClassRepository;
        this.answearRepository = answearRepository;
    }






    //no update because it will be problematic in managment(updating every test atempt canceling every atempt that happend before change itd)

    public int addTest(Test test)
    {
        string query = $@"
            INSERT INTO {ENTITY_NAME} (name, students_class)
            VALUES (@name, @students_class);SELECT SCOPE_IDENTITY();";

        SqlParameter[] parameters = {
            new SqlParameter("@name", SqlDbType.VarChar) { Value = test.name },
            new SqlParameter("@students_class", SqlDbType.VarChar) { Value = test.studentsClass.id },
        };

        return databaseManager.executeSaveQuery(query, parameters);
    }

    public void addStudentsClassToTestRelation(int studentsClassId, int testId) {
        string query = "INSERT INTO students_class_to_tests(test_id,students_class_id) VALUES (@test_id,@students_class_id)";

        SqlParameter[] parameters = {
            new SqlParameter("@test_id", SqlDbType.Int) { Value = testId },
            new SqlParameter("@students_class_id", SqlDbType.Int) { Value = studentsClassId },
        };

        databaseManager.executeSaveQuery(query, parameters);
    }

    public List<Test> findAllTestsByClass(int classId)
    {
        List<int> ids = findTestsIdsForClass(classId);
        List<Test> tests = new List<Test>();
        ids.ForEach(id => tests.Add(findTestById(id)));
        return tests;
    }

    private List<int> findTestsIdsForClass(int classId)
    {
        string query = "SELECT * FROM students_class_to_tests where students_class_id = @classId";

        SqlParameter[] parameters = {
            new SqlParameter("@classId", SqlDbType.Int) { Value = classId },
        };

        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        List<int> testIds = new List<int>();
        while (reader.Read())
        {
            testIds.Add(reader.GetInt32(0));
        }
        return testIds;
    }

    public List<Test> findAllTestsByTeacher(int teacherId)
    {
        List<StudentsClass> studentsClasses = studentsClassRepository.findByTeacher(teacherId);
        List<Test> tests = new List<Test>();
        studentsClasses.ForEach(sc => tests.AddRange(findAllTestsByClass(sc.id)));
        return tests;
    }

    public List<Test> findUnattendedTestsForStudent(int studentId)
    {
        List<StudentsClass> studentsClasses = new List<StudentsClass>();
        var result = studentsClassRepository.findByStudent(studentId);
        if (result != null)
        {
            studentsClasses = result;
        }
        else {
            Console.WriteLine("Zero results for this student");
            return null;
        }
        List<Test> tests = new List<Test>();
        studentsClasses.ForEach(sc => {
            findByStudentClass(sc.id).ForEach(t => tests.Add(new Test(t.id, t.name, sc)));
            });
        List<TestAtempt> attended = testAtemptRepository.getAttendedByStudent(studentId);
        List<Test> notAttended = addToListIfNotAttended(tests, attended);
        return notAttended;
    }


    private List<Test> addToListIfNotAttended(List<Test> tests, List<TestAtempt> attended) {
        List<Test> unattended = new List<Test>();
        foreach (Test test in tests)
        {
            if (attended.Find(a => a.test.id == test.id) == null) { 
                unattended.Add(test);
            }
        }
        return unattended;
    }

    public List<Test> findByStudentClass(int studentsClassId)
    {
        string query = $"SELECT * FROM {ENTITY_NAME} where students_class = @classId";
        SqlParameter[] parameters = {
            new SqlParameter("@classId", SqlDbType.Int) { Value = studentsClassId },
        };

        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);

        List<Test> result = new List<Test>();
        while (reader.Read())
        {
            result.Add(new Test(
            reader.GetInt32(0),
            reader.GetString(1)
            ));
        }
        return result;
    }

    public Test findTestById(int id)
    {
        string query = $"SELECT * FROM {ENTITY_NAME} where id = @id";
        SqlParameter[] parameters = {
           new SqlParameter("@id", SqlDbType.Int) { Value = id },
        };

        List<Question> questions = questionRepository.findByTestId(id);
        questions.ForEach(q => q.answears = answearRepository.findByQuestionId(q.id));
        SqlDataReader reader = databaseManager.executeSelectQuery(query, parameters);
        if (reader.Read())
        {
            return new Test(
            reader.GetInt32(0),
            reader.GetString(1),
            studentsClassRepository.findById(reader.GetInt32(2)),
            questions
            );
        }
        return null;
    }

    public void deleteTest(int id) { }//maybe to delete?!
}
