public class AdminInterface : AbstractUserInterface
{
    StudentRepository studentRepository;
    TeacherRepository teacherRepository;
    AuthRepository authRepository;

    private static List<string> operations = new List<string>() { 
        "(0) Add student",
        "(1) Add teacher",
        "(2) Update student",
        "(3) Update teacher",
        "(4) Delete student",
        "(5) Delete teacher",
        "(6) View all students",
        "(7) View all teachers",
        "(-1) Exit"
    };

    public AdminInterface(StudentRepository studentRepository, TeacherRepository teacherRepository, AuthRepository authRepository)
    {
        this.studentRepository = studentRepository;
        this.teacherRepository = teacherRepository;
        this.authRepository = authRepository;
    }

    public override void handleChoosedOperation()
    {
        printOperations(operations);
        int answear = getOperationChoosed();
        switch (answear)
        {
            case 0:
                handleAddStudent();
                break;
            case 1:
                handleAddTeacher();
                break;
            case 2:
                handleUpdateStudent();
                break;
            case 3:
                handleUpdateTeacher();
                break;
            case 4:
                handleDeleteStudent();
                break;
            case 5:
                handleDeleteTecher();
                break;
            case 6:;
                handleViewAllStudents();
                break;
            case 7:
                handleViewAllTeachers();
                break;
            case -1:
                Console.WriteLine("Exiting with status code 0");
                Environment.Exit(0);
                break;
        }
    }

    private void handleAddStudent() {
        Login login = Login.createLoginUsingConsole(authRepository);
        if (login == null)
        {
            Console.WriteLine("Given login already exist...");
            return;
        }
        Student student = Student.createStudentUsingConsole(login);
        studentRepository.addStudent(student);
    }


    private void handleAddTeacher() {
        Login login = Login.createLoginUsingConsole(authRepository);
        if (login == null) {
            Console.WriteLine("Given login already exist...");
            return;
        }
        Teacher teacher = Teacher.createTeacherUsingConsole(login);
        teacherRepository.addTeacher(teacher);
    }

    private void handleUpdateStudent() {
        Console.WriteLine("Give id of student to update : ");
        int id = Convert.ToInt32(Console.ReadLine());
        Student found = studentRepository.findById(id);
        Student student = Student.createStudentUsingConsole(found.login);
        studentRepository.updateStudent(student, id);
    }
    private void handleUpdateTeacher() {
        Console.WriteLine("Give id of teacher  to update : ");
        int id = Convert.ToInt32(Console.ReadLine());
        Teacher found = teacherRepository.findById(id);
        Teacher teacher = Teacher.createTeacherUsingConsole(found.login);
        teacherRepository.updateTeacher(teacher, id);
    }
    private void handleDeleteStudent() {
        Console.WriteLine("Give id of student to delete");
        int id = Convert.ToInt32(Console.ReadLine());
        studentRepository.deleteStudent(id);
    }
    private void handleDeleteTecher() {
        Console.WriteLine("Give id of teacher to delete");
        int id = Convert.ToInt32(Console.ReadLine());
        teacherRepository.deleteTeacher(id);
    }
    private void handleViewAllStudents() {
        studentRepository.findAllStudents().ForEach(s => Student.printStudent(s));
    }
    private void handleViewAllTeachers() {
        teacherRepository.findAllTeachers().ForEach(t => Teacher.printTeacher(t));
    }

}
