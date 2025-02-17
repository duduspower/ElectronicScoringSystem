class MainProgram {
    private static DatabaseManager databaseManager = new DatabaseManager();
    private static AuthRepository authRepository = new AuthRepository(databaseManager);
    private static StudentRepository studentRepository = new StudentRepository(databaseManager, authRepository);
    private static TeacherRepository teacherRepository = new TeacherRepository(databaseManager, authRepository);
    private static LoginInterface loginInterface = new LoginInterface(authRepository, studentRepository, teacherRepository);
    private static AdminInterface adminInterface = new AdminInterface(studentRepository, teacherRepository, authRepository);
    private static StudentsClassRepository studentsClassRepository = new StudentsClassRepository(databaseManager);
    private static QuestionRepository questionRepository = new QuestionRepository(databaseManager);
    private static TestAtemptRepository testAtemptRepository = new TestAtemptRepository(databaseManager);
    private static AnswearRepository answearRepository = new AnswearRepository(databaseManager);
    private static TestRepository testRepository = new TestRepository(databaseManager, questionRepository, studentsClassRepository,testAtemptRepository, answearRepository);


    public static void Main(String[] args) {
        try
        {
            loginInterface.handleChoosedOperation();
        }
        catch (Exception e) { 
            Console.WriteLine(e.Message);
        }
        loginInterface.handleChoosedOperation();

    }

    public static void switchToAdminPanel() {
        while (true)
        {
            adminInterface.handleChoosedOperation();
        }
    }
    public static void switchToStudentPanel(Student student) {
        StudentUserInterface studentUserInterface = new StudentUserInterface(testRepository, testAtemptRepository, answearRepository, student);
        while (true) { 
            studentUserInterface.handleChoosedOperation();
        }
    }

    public static void switchToTeacherPanel(Teacher teacher) {
        TeacherUserInterface teacherUserInterface = new TeacherUserInterface(teacher, studentRepository, studentsClassRepository, questionRepository, answearRepository, testRepository);
        while (true) { 
            teacherUserInterface.handleChoosedOperation();
        }
    }

}
