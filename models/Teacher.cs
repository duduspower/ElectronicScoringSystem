public class Teacher
{
    public int id { get; }
    public string name { get; }
    public string surname { get; }
    public string academicTitle { get; }
    public string email { get; }
    public string phone { get; }
    public List<StudentsClass> studentsClasses { get; set; }
    public Login login { get; }

    public Teacher(int id, string name, string surname, string academicTitle, string email, string phone, List<StudentsClass> studentsClasses, Login login)
    {
        this.id = id;
        this.name = name;
        this.surname = surname;
        this.academicTitle = academicTitle;
        this.email = email;
        this.phone = phone;
        this.studentsClasses = studentsClasses;
        this.login = login;
    }

    public Teacher(string name, string surname, string academicTitle, string email, string phone, List<StudentsClass> studentsClasses, Login login)
    {
        this.name = name;
        this.surname = surname;
        this.academicTitle = academicTitle;
        this.email = email;
        this.phone = phone;
        this.studentsClasses = studentsClasses;
        this.login = login;
    }

    public Teacher(string name, string surname, string academicTitle, string email, string phone, Login login)
    {
        this.name = name;
        this.surname = surname;
        this.academicTitle = academicTitle;
        this.email = email;
        this.phone = phone;
        this.login = login;
    }

    public static Teacher createTeacherUsingConsole(Login login)
    {
        Console.WriteLine("Making teacher : ");
        Console.WriteLine("Give name : ");
        string name = Console.ReadLine();
        Console.WriteLine("Give surname : ");
        string surname = Console.ReadLine();
        Console.WriteLine("academicTitle : ");
        string academicTitle = Console.ReadLine();
        bool okFlag = true;

        string email = "";
        while (okFlag)
        {
            Console.WriteLine("Give email : ");
            email = Console.ReadLine();
            if (EmailValidator.IsValidEmail(email))
            {
                okFlag = false;
            }
            else
            {
                Console.WriteLine("Invalid email not matching pattern");
            }
        }

        okFlag = true;

        string phone = "";
        while (okFlag)
        {
            Console.WriteLine("Give phone : ");
            phone = Console.ReadLine();
            if (PhoneValidator.IsValidPhoneNumber(phone))
            {
                okFlag = false;
            }
            else
            {
                Console.WriteLine("Invalid phone not matching pattern. Must have no blank characters starts with +(2countrynumber)+(9numbers)");
            }
        }
        okFlag = true;
        return new Teacher( name, surname, academicTitle, email, phone, login);
    }
    public string toString()
    {
        return $"Teacher [ID: {id}, Name: {name}, Surname: {surname}, Academic Title: {academicTitle}, " +
               $"Email: {email}, Phone: {phone}, Login ID: {login.id}]";
    }

    public static void printTeacher(Teacher teacher)
    {
        Console.WriteLine(teacher.toString());
    }
}
