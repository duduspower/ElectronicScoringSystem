public class Login
{
	public int id;
	public string login;
	public string password;

    public Login(string login, string password)
    {
        this.login = login;
        this.password = password;
    }

    public Login(int id, string login, string password)
    {
        this.id = id;
        this.login = login;
        this.password = password;
    }

    public static Login createLoginUsingConsole(AuthRepository authRepository) {
        Console.WriteLine("Making login : ");
        bool okFlag = true;

        string login = "";
        while (okFlag)
        {
            Console.WriteLine("Give login : ");
        login = Console.ReadLine();
            if (authRepository.getLoginByLogin(login) != null)
            {
                Console.WriteLine("Given login already exist. Try other one!");
            }
            else if (LoginValidator.IsValidLogin(login))
            {
                okFlag = false;
            }
            else {
                Console.WriteLine("Invalid login");
            }
        }

        okFlag = true;

        string password = "";
        while (okFlag)
        {
            Console.WriteLine("Give password : ");
            password = Console.ReadLine();
            if (PasswordValidator.IsValidPassword(password))
            {
                okFlag = false;
            }
            else {
                Console.WriteLine("Invalid password not matching pattern. Must have one letter Big and small case, number, special character and be at least 8 character long");
            }
        }
        return new Login(login, password);
    }
}
