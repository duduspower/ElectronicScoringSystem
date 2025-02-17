using System;

public class StudentsClass
{
    public int id { get; }
    public string name { get; }
    public List<Student> students { get; }
    List<Test> tests;
    public Teacher teacher { get;  }

    public StudentsClass(string name, List<Student> students, Teacher teacher)
    {
        this.name = name;
        this.students = students;
        this.teacher = teacher;
    }

    public StudentsClass(int id, string name, List<Student> students, List<Test> tests, Teacher teacher)
    {
        this.id = id;
        this.name = name;
        this.students = students;
        this.tests = tests;
        this.teacher = teacher;
    }

    public StudentsClass(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public static StudentsClass createStudentsClassUsingConsole(Teacher teacher, StudentRepository repository) {
        Console.WriteLine("Give class name : ");
        string className = Console.ReadLine();
        Console.WriteLine("Give students list : (separated with ',')");
        string response = Console.ReadLine();
        string[] idsString = response.Split(',');
        int[] idsOfStudents = new int[idsString.Length];
        List<Student> students = new List<Student>();
        for (int i = 0; i < idsString.Length; i++)
        {
            idsOfStudents[i] = Convert.ToInt32(idsString[i]);
            students.Add(repository.findById(idsOfStudents[i]));
        }
        return new StudentsClass(className, students, teacher);
    }

    public void printForSelectList() {
        Console.WriteLine($"Id : {this.id}, Name : {this.name}");
    }
}

 
