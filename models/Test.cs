using System;
using System.Formats.Tar;

public class Test
{
	public int id { get;  }
    public string name { get; }
	public StudentsClass studentsClass { get; set; }
	public List<Question> questions { get; set; }

    private void addToClass(StudentsClass studentsClass)
    {
        this.studentsClass = studentsClass;
    }

    public Test(List<Question> questions)
    {
        this.questions = questions;
    }

    public Test(int id, string name, StudentsClass studentsClass) : this(id, name)
    {
        this.studentsClass = studentsClass;
    }

    public Test(int id, string name, StudentsClass studentsClass, List<Question> questions)
    {
        this.id = id;
        this.name = name;
        this.studentsClass = studentsClass;
        this.questions = questions;
    }

    public Test(int id, string name, List<Question> questions)
    {
        this.id = id;
        this.name = name;
        this.questions = questions;
    }

    public Test(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public Test(string name, StudentsClass studentsClass, List<Question> questions)
    {
        this.name = name;
        this.studentsClass = studentsClass;
        this.questions = questions;
    }

    public static Test createTestUsingConsole(QuestionRepository questionRepository, StudentsClassRepository studentClassRepository, Teacher teacher) {
        Console.WriteLine("Making test : ");
        Console.WriteLine("Give test name : ");
        string testName = Console.ReadLine();
        Console.WriteLine("Give class id : ");
        Console.WriteLine("Printing students classes : ");
        List<StudentsClass> studentClasses = studentClassRepository.findByTeacher(teacher.id);
        studentClasses.ForEach(studentClass => studentClass.printForSelectList());
        int studentClassId = Convert.ToInt32(Console.ReadLine());
        StudentsClass choosedClass = studentClasses.Find(sc => sc.id == studentClassId);
        Console.WriteLine("Declare number of questions in test : ");
        bool notAccepted = true;
        int numberOfQuestions = 0;
        while (notAccepted)
        {
            numberOfQuestions = Convert.ToInt32(Console.ReadLine());
            if (numberOfQuestions < 1)
            {
                Console.WriteLine("You cannot add question with less than one answear...");
            }
            else
            {
                notAccepted = false;
            }
        }
        List<Question> questions = new List<Question>();
        for (int i = 0; i < numberOfQuestions; i++) {
            questions.Add(Question.createQuestionUsingConsole());
        }
        return new Test(testName, choosedClass, questions);
    }

    public override string ToString()
    {
        return $"Test : [ Id : {this.id}, Name : {this.name}, Class : {this.studentsClass.name}]";
    }

    public void printTest() {
        Console.WriteLine(this.ToString());
    }

    public void printTestWithQuestions() { 
        printTest();
        this.questions.ForEach(q => q.printQuestion());
    }
}



