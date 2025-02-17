public class StudentUserInterface : AbstractUserInterface
{
    TestRepository testRepository;
    TestAtemptRepository testAtemptRepository;
    AnswearRepository answearRepository;
    Student student;

    private static List<string> operations = new List<string>() {
        "(0) Show test to do",
        "(1) Show tests results",
        "(2) Attend test",
        "(-1) Exit"
    };

    public StudentUserInterface(TestRepository testRepository, TestAtemptRepository testAtemptRepository, AnswearRepository answearRepository, Student student)
    {
        this.testRepository = testRepository;
        this.testAtemptRepository = testAtemptRepository;
        this.answearRepository = answearRepository;
        this.student = student;
    }

    private void handleShowTestsToDo()
    {
        var result = testRepository.findUnattendedTestsForStudent(student.id);
        if (result != null)
        {
            List<Test> tests = result;
            tests.ForEach(test => test.printTest());
        }

    }

    private void handleShowTestsResults()
    {
        List<TestAtempt> attempts = testAtemptRepository.getAttendedByStudent(student.id);
        attempts.ForEach(t => t.printResultForStudent());
    }

    private void handleAttendTest()
    {
        Console.WriteLine("Give id of test to attend : ");
        int answear = Convert.ToInt32(Console.ReadLine());
        Test test = testRepository.findTestById(answear);
        if (test == null) {
            Console.WriteLine("Cannot find test for this id...");
            return;
        }
        TestAtempt testAtempt = TestAtempt.attemptTestUsingConsole(student, test, answearRepository);
        testAtemptRepository.atemptTest(test, student, testAtempt.correctAnswears, testAtempt.incorrectAnswers);
    }

    public override void handleChoosedOperation()
    {
        printOperations(operations);
        int answear = getOperationChoosed();
        switch (answear)
        {
            case 0:
                handleShowTestsToDo();
                break;
            case 1:
                handleShowTestsResults();
                break;
            case 2:
                handleAttendTest();
                break;
            case -1:
                Console.WriteLine("Exiting with status code 0");
                Environment.Exit(0);
                break;
        }
    }
}
