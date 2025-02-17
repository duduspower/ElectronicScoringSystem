using System.Security.Cryptography;

internal class TeacherUserInterface : AbstractUserInterface
{
    Teacher teacher;
    StudentRepository studentRepository;
    StudentsClassRepository studentsClassRepository;
    QuestionRepository questionRepository;
    AnswearRepository answerRepository;
    TestRepository testRepository;

    private static List<string> operations = new List<string>() {
            "(0) Add class",
            "(1) Add test",
            "(2) View tests",
            "(3) View test by id",
            "(4) List all students",
            "(5) List all classes for teacher",
            "(6) Handle add student to class",
            "(-1) Exit"
        };

    public TeacherUserInterface(Teacher teacher, StudentRepository studentRepository, StudentsClassRepository studentsClassRepository, QuestionRepository questionRepository, AnswearRepository answerRepository, TestRepository testRepository)
    {
        this.teacher = teacher;
        this.studentRepository = studentRepository;
        this.studentsClassRepository = studentsClassRepository;
        this.questionRepository = questionRepository;
        this.answerRepository = answerRepository;
        this.testRepository = testRepository;
    }

    public override void handleChoosedOperation()
    {
        printOperations(operations);
        int answear = getOperationChoosed();
        switch (answear)
        {
            case 0:
                handleAddClass();
                break;
            case 1:
                handleAddTest();
                break;
            case 2:
                handleViewTests();
                break;
            case 3:
                handleViewTestById();
                break;
            case 4:
                handleViewAllStudents();
                break;
            case 5:
                handleViewAllClassForTeacher();
                break;
            case 6:
                handleAddStudentToClass();
                break;
            case -1:
                Console.WriteLine("Exiting with status code 0");
                Environment.Exit(0);
                break;
        }
    }

    private void handleAddClass()
    {
        StudentsClass studentsClass = StudentsClass.createStudentsClassUsingConsole(teacher, studentRepository);
        studentsClassRepository.addStudentClass(studentsClass);
        StudentsClass studentClassEntity = studentsClassRepository.findByName(studentsClass.name);
        studentsClass.students.ForEach(student => studentsClassRepository.addStudentToClass(studentClassEntity, student));
    }

    private void handleAddStudentToClass() {
        Console.WriteLine("Give class id : ");
        Console.WriteLine("Printing students classes : ");
        List<StudentsClass> studentClasses = studentsClassRepository.findByTeacher(teacher.id);
        studentClasses.ForEach(studentClass => studentClass.printForSelectList());
        int studentClassId = Convert.ToInt32(Console.ReadLine());
        StudentsClass sc = studentsClassRepository.findById(studentClassId);
        Console.WriteLine("Give studentId : ");
        int studentId = Convert.ToInt32(Console.ReadLine());
        Student student = studentRepository.findById(studentId);
        studentsClassRepository.addStudentToClass(sc, student);
    }

    private void handleAddTest()//to do too complex logic for one method refactor in future
    {
        Test test = Test.createTestUsingConsole(questionRepository, studentsClassRepository, teacher);
        int testId = testRepository.addTest(test);
        testRepository.addStudentsClassToTestRelation(test.studentsClass.id, testId);
        List<int> questionIds = new List<int>();
        test.questions.ForEach(q => questionIds.Add(questionRepository.addQuestion(q)));
        questionIds.ForEach(qId => questionRepository.addQuestionTestRelation(qId, testId));
        List<List<int>> answearIds = new List<List<int>>();

        for (int i = 0; i < test.questions.Count; i++)
        {
            answearIds.Add(new List<int>());
            List<int> answears = answearIds[0];
            for (int j = 0; j < test.questions[i].answears.Count; j++)
            {
                answears.Add(answerRepository.addAnsware(test.questions[i].answears[j]));
            }
        }

        for (int i = 0; i < answearIds.Count; i++)
        {
            for (int j = 0; j < answearIds[i].Count; j++)
            {
                answerRepository.addQuestionAnswearRelation(questionIds[i], answearIds[i][j]);
            }
        }
    }

    private void handleViewTests()
    {
        List<Test> tests = testRepository.findAllTestsByTeacher(teacher.id);
        tests.ForEach(t => t.printTest());
    }

    private void handleViewTestById()
    {
        Console.WriteLine("Give testId");
        int id = Convert.ToInt32(Console.ReadLine());
        Test test = testRepository.findTestById(id);
        if (test == null)
        {
            Console.WriteLine("No results");
            return;
        }
        test.printTestWithQuestions();
    }

    private void handleViewAllStudents()
    {
        List<Student> students = studentRepository.findAllStudents();
        students.ForEach(student => Student.printStudent(student));
    }

    private void handleViewAllClassForTeacher()
    {
        List<StudentsClass> studentsClasses = studentsClassRepository.findByTeacher(teacher.id);
        studentsClasses.ForEach(s => s.printForSelectList());
    }
}
