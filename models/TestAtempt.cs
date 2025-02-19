
public class TestAtempt
{
    public int id;
    public string name;
    public Test test { get; }
    public int correctAnswears { get; }
    public int incorrectAnswers { get; }
    private Student student;

    public TestAtempt(int id, string name, int correctAnswears, int incorrectAnswers)
    {
        this.id = id;
        this.name = name;
        this.correctAnswears = correctAnswears;
        this.incorrectAnswers = incorrectAnswers;
    }

    public TestAtempt(int id, string name, Test test, int correctAnswears, int incorrectAnswers, Student student)
    {
        this.id = id;
        this.name = name;
        this.test = test;
        this.correctAnswears = correctAnswears;
        this.incorrectAnswers = incorrectAnswers;
        this.student = student;
    }

    public TestAtempt(string name, Test test, int correctAnswears, int incorrectAnswers, Student student)
    {
        this.name = name;
        this.test = test;
        this.correctAnswears = correctAnswears;
        this.incorrectAnswers = incorrectAnswers;
        this.student = student;
    }

    public static TestAtempt attemptTestUsingConsole(Student student, Test test, AnswearRepository answearRepository) {
        Console.WriteLine($"Attempting test {test.name}");
        List<Question> questions = randomiseQuestion(test.questions);
        int correctAnswers = 0;
        int incorrectAnswers = 0;
        questions.ForEach(q =>
        {
            if (getAnswearFromUser(q).value.Equals(q.correctAnswearValue)){
                correctAnswers++;
            } else {
                incorrectAnswers++;
            }
        });
        TestAtempt currentAttempt = new TestAtempt(test.name, test, correctAnswers, incorrectAnswers, student);
        return currentAttempt;
    }

    private static Answear getAnswearFromUser(Question question) {
        question.printQuestionWithAnswers();
        Console.WriteLine("Give answear(numberOfAnswear) : ");
        int answear = Convert.ToInt32(Console.ReadLine());
        return question.answears[answear];
    }

    public static List<Question> randomiseQuestion(List<Question> questions)
    {
        Random random = new Random();
        int n = questions.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            (questions[i], questions[j]) = (questions[j], questions[i]);
        }
        return questions;
    }

    public void printResultForStudent() {
        int result = getResultForTest(correctAnswears, incorrectAnswers);
        Console.WriteLine($"Result for test: [name : {this.name}, result : {result}, student : {student.name + " " + student.surname}]");
    }

    public int getResultForTest(int correctAnswers, int incorrectAnswers)
    {
        int totalAnswers = correctAnswers + incorrectAnswers;
        if (totalAnswers == 0) return 2; // Jeśli brak odpowiedzi, zwraca najniższą ocenę

        double correctPercentage = (double)correctAnswers / totalAnswers * 100;

        if (correctPercentage < 50) return 2;
        if (correctPercentage < 70) return 3;
        if (correctPercentage < 90) return 4;
        return 5;
    }

}

