using System;

public class Answear {
    public int id { get; }
    public string value { get;  }

    public Answear(int id, string value)
    {
        this.id = id;
        this.value = value;
    }

    public Answear(string value)
    {
        this.value = value;
    }

    public static Answear createAnswearUsingConsole() {
        Console.WriteLine("Give answear value");
        string answear = Console.ReadLine();
        return new Answear(answear);
    }
}

public class Question
{
    public int id { get; }
    public string value;
    public List<Answear> answears { get; set; }
    public string correctAnswearValue { get; set; }

    public Question(string value, string correctAnswearValue)
    {
        this.value = value;
        this.correctAnswearValue = correctAnswearValue;
    }

    public Question(int id, string value, List<Answear> answears, string correctAnswearValue)
    {
        this.id = id;
        this.value = value;
        this.answears = answears;
        this.correctAnswearValue = correctAnswearValue;
    }

    public Question(string value, List<Answear> answears, string correctAnswearValue)
    {
        this.value = value;
        this.answears = answears;
        this.correctAnswearValue = correctAnswearValue;
    }

    public Question(int id, string value, string correctAnswearValue)
    {
        this.id = id;
        this.value = value;
        this.correctAnswearValue = correctAnswearValue;
    }

    public Question(string value, List<Answear> answears)
    {
        this.value = value;
        this.answears = answears;
    }

    public static Question createQuestion(string value, Answear correctAnswear) {
        Question result = new Question(value, correctAnswear.value);
        return result;
    }

    public static Question createQuestionUsingConsole() {
        Console.WriteLine("Making question : ");
        Console.WriteLine("Give question value : ");
        string question = Console.ReadLine();
        bool notAccepted = true;
        int numberOfAnswears = 0;
        while (notAccepted)
        {
            Console.WriteLine("Give number of answears : ");
            numberOfAnswears = Convert.ToInt32(Console.ReadLine());
            if (numberOfAnswears < 2)
            {
                Console.WriteLine("You cannot add question with less than two answear...");
            }
            else {
                notAccepted = false;
            }
        }
        List<Answear> answears = new List<Answear>();
        Console.WriteLine("Give correct answear : ");
        answears.Add(Answear.createAnswearUsingConsole());
        for (int i = 1; i < numberOfAnswears; i++) {
            answears.Add(Answear.createAnswearUsingConsole());
        }
        return new Question(question, answears , answears[0].value);
    }

    public void printQuestion() {
        Console.WriteLine($"Question : [ Id : {this.id}, Name : {this.value}]");
    }

    public void printQuestionsWithAnswers()
    {
        Console.WriteLine("");
        Console.WriteLine("Answears : {");
        for (int i = 0; i < this.answears.Count; i++) {
            Console.WriteLine($"({i}) : {answears[i].value}");
        }
        Console.WriteLine("}");
    }
}
