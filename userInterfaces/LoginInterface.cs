class Credidential {
    public string login;
    public string password;

    public Credidential(string login, string password)
    {
        this.login = login;
        this.password = password;
    }
}

public class LoginInterface : AbstractUserInterface{
	List<string> operations = new List<string>() {
		"(0) Login as student",
		"(1) Login as teacher",
        "(2) Login as admin",
		"(-1) Exit program",
	};

    private AuthRepository authRepository;
    private StudentRepository studentRepository;
    private TeacherRepository teacherRepository;

    public LoginInterface(AuthRepository authRepository, StudentRepository studentRepository, TeacherRepository teacherRepository)
    {
        this.authRepository = authRepository;
        this.studentRepository = studentRepository;
        this.teacherRepository = teacherRepository;
    }

    private void handleLoginAsStudent() {
        Credidential credidential = getCredidential();
        Student student = authenticateStudent(credidential.login, credidential.password);
        if (student == null) {
            return;
        }
        MainProgram.switchToStudentPanel(student);
    }

    private void handleLoginAsTeacher() {
        Credidential credidential = getCredidential();
        Teacher teacher = authenticateTeacher(credidential.login, credidential.password);
        if (teacher == null) {
            return;
        }
        MainProgram.switchToTeacherPanel(teacher);
    }

    private void handleLoginAsAdmin() {
        Credidential credidential = getCredidential();
        if (!authenticateAdmin(credidential.login, credidential.password)) {
            return;
        }
        MainProgram.switchToAdminPanel();
    }

    private Student authenticateStudent(string login, string password) {
        Login loginEntity = authRepository.getLoginByLogin(login);
        if (!authenticate(login, password, loginEntity))
        {
            return null;
        }
        Student studentEntity = studentRepository.findByLoginId(loginEntity.id);
        if (studentEntity == null) {
            throw new ArgumentException($"Student entity not found for givenId : {loginEntity.id}");
        }
        return studentEntity;
    }

    private Teacher authenticateTeacher(string login, string password)
    {
        Login loginEntity = authRepository.getLoginByLogin(login);
        if (!authenticate(login, password, loginEntity)) {
            return null;
        }
        Teacher teacherEntity = teacherRepository.findByLoginId(loginEntity.id);
        if (teacherEntity == null)
        {
            throw new ArgumentException($"Teacher entity not found for givenId : {loginEntity.id}");
        }
        return teacherEntity;
    }

    private bool authenticateAdmin(string login, string password) {
        Login loginEntity = authRepository.getLoginByLogin("admin");
        if (!authenticate(login, password, loginEntity)) {
            return false;
        }
        return true; //means admin is authenticated
    }

    private bool authenticate(string loginFromUser, string passwordFromUser, Login login) {
        if (login == null || login.login != loginFromUser)
        {
            Console.WriteLine("Invalid login");
            return false;
        }
        if (passwordFromUser != login.password)
        {
            Console.WriteLine("Invalid password");
            return false;
        }
        return true;
    }

    private Credidential getCredidential()
    {
        Console.WriteLine("Give Login");
        string login = Console.ReadLine();
        Console.WriteLine("Give Password");
        string password = Console.ReadLine();
        return new Credidential(login, password);
    }

    public override void handleChoosedOperation()
    {
        while (true)
        {
            printOperations(operations);
            int answear = getOperationChoosed();
            switch (answear)
            {
                case 0:
                    handleLoginAsStudent();
                    break;
                case 1:
                    handleLoginAsTeacher();
                    break;
                case 2:
                    handleLoginAsAdmin();
                    break;
                case -1:
                    Console.WriteLine("Exiting with status code 0");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
