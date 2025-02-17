public class Student
{
    public int id { get; }
    public string name { get; }
	public string surname { get; }
	public DateOnly dateOfBirth { get; }
	public string email { get; }
	public string phone { get; }
	public string index { get; }
	public Login login { get; set; }

    public Student(int id, string name, string surname, DateOnly dateOfBirth, string email, string phone, string index, Login login)
    {
        this.id = id;
        this.name = name;
        this.surname = surname;
        this.dateOfBirth = dateOfBirth;
        this.email = email;
        this.phone = phone;
        this.index = index;
        this.login = login;
    }

    public Student(string name, string surname, DateOnly dateOfBirth, string email, string phone, string index, Login login)
    {
        this.name = name;
        this.surname = surname;
        this.dateOfBirth = dateOfBirth;
        this.email = email;
        this.phone = phone;
        this.index = index;
        this.login = login;
    }

    public static Student createStudentUsingConsole(Login login) {
        Console.WriteLine("Making student : ");
        Console.WriteLine("Give name : ");
        string name = Console.ReadLine();
        Console.WriteLine("Give surname : ");
        string surname = Console.ReadLine();
        Console.WriteLine("Give date of birth : ");
        string dateOfBirthStringValue = Console.ReadLine();
        DateOnly dateOfBirth = DateOnly.Parse(dateOfBirthStringValue);
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

        string index = "";
        while (okFlag)
        {
            Console.WriteLine("Give index : (letter and 5 numbers)");
            index = Console.ReadLine();
            if (IndexValidator.IsValidIndex(index))
            {
                okFlag = false;
            }
            else
            {
                Console.WriteLine("Invalid index not matching pattern. Must be (letter+5numbers)");
            }
        }

        return new Student(name, surname, dateOfBirth, email, phone, index, login);
    }

    public string toString() {
        return $"Student [ID: {id}, Name: {name}, Surname: {surname}, Date of Birth: {dateOfBirth:yyyy-MM-dd}, " +
                $"Email: {email}, Phone: {phone}, Student Index: {index}, Login ID: {login.id}]";
    }

    public static void printStudent(Student student) {
        Console.WriteLine(student.toString());
    }
}
