using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vp4_proxy
{
    internal class Program
    {
        static string AdminHelpMessage()
        {
            string help_message = new string('*', 51) + "\n  HELP:" + new string(' ', 42) + "\n  Possible commands:" + new string(' ', 29) +
                        "\n  1  - to read from file. " + new string(' ', 25) +
                        "\n  2  - to sort elements. " + new string(' ', 25) +
                        "\n  3  - to search element.  " + new string(' ', 20) +
                        "\n  4  - to add Product to collection. " + new string(' ', 10) +
                        "\n  5  - to del Product from collection.  " + new string(' ', 10) +
                        "\n  6  - to edit Product in collection.  " + new string(' ', 9) +
                        "\n  7  - to write collection elements to json file. " +
                        "\n  8 - to show collection. " + new string(' ', 22) +
                        "\n  9 - to view by id. " + new string(' ', 22) +
                        "\n  10 - to logout. " + new string(' ', 22) +
                        "\n  exit - to exit.  " + new string(' ', 30) + "\n" +
                        new string('*', 51) + "\n";
            return help_message;
        }
        static string CustomerHelpMessage()
        {
            string help_message = new string('*', 51) + "\n  HELP:" + new string(' ', 42) + "\n  Possible commands:" + new string(' ', 29) +
                        "\n  1  - to sort elements. " + new string(' ', 25) +
                        "\n  2  - to search element.  " + new string(' ', 20) +
                        "\n  3 - to show collection. " + new string(' ', 22) +
                        "\n  4 - to view by id. " + new string(' ', 22) +
                        "\n  5 - to logout. " + new string(' ', 22) +
                        "\n  exit - to exit.  " + new string(' ', 30) + "\n" +
                        new string('*', 51) + "\n";
            return help_message;
        }

        static void Register(Auth auth)
        {
            var errors = new List<string>();
            Console.WriteLine("Your first name: ");
            string f_name = Console.ReadLine();
            if (!Helper.NameValid(f_name)) errors.Add("Invalid first name");
            Console.WriteLine("Your last name: ");
            string l_name = Console.ReadLine();
            if (!Helper.NameValid(l_name)) errors.Add("Invalid last name");
            Console.WriteLine("Your email: ");
            string email = Console.ReadLine();
            if (!Helper.EmailValid(email)) errors.Add("Invalid email");
            Console.WriteLine("Your password: ");
            string password = Console.ReadLine();
            if (!Helper.PasswordValid(password)) errors.Add("Invalid password");
            Console.WriteLine("Confirm password: ");
            string confirmPassword = Console.ReadLine();
            if (password != confirmPassword) errors.Add("Passwords don't match");

            if (errors.Count == 0)
            {
                var newUser = new User(f_name, l_name, email, password, Role.customer);
                try
                {
                    auth.Register(newUser);
                    Console.WriteLine($"{f_name} {l_name} successfuly registred!");
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }
            if (errors.Count > 0)
            {
                Console.WriteLine("Customer cannot be created: ");
                foreach (string error in errors) Console.WriteLine(error);
            }

        }
        static User? LogIn(Auth auth)
        {
            var errors = new List<string>();
            Console.WriteLine("Your email: ");
            string email = Console.ReadLine();
            if (!Helper.EmailValid(email)) errors.Add("Invalid email");
            Console.WriteLine("Your password: ");
            string password = Console.ReadLine();
            if (!Helper.PasswordValid(password)) errors.Add("Invalid password");

            if (errors.Count == 0)
            {
                try
                {
                    var user = auth.LogIn(email, password);
                    Console.WriteLine($"{user.firstName} {user.lastName} logged in.");
                    return user;
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }
            if (errors.Count > 0)
            {
                Console.WriteLine("One or more login errors: ");
                foreach (string error in errors) Console.WriteLine(error);
            }
            return null;
        }
        static void LogOut(Auth auth)
        {
            try
            {
                auth.LogOut();
                Console.WriteLine("Loged out");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void adminView(Auth auth, ICollection collection)
        {
            User user = Auth.LoggedIn;
            while (true)
            {
                Console.Write(AdminHelpMessage());
                string task = Console.ReadLine();
                switch (task)
                {
                    case "1":
                        user.ReadJson(collection);
                        break;
                    case "2":
                        user.Sort(collection);
                        break;
                    case "3":
                        user.Search(collection);
                        break;
                    case "4":
                        user.Add(collection);
                        break;
                    case "5":
                        user.Delete(collection);
                        break;
                    case "6":
                        user.Edit(collection);
                        break;
                    case "7":
                        user.WriteInJson(collection);
                        break;
                    case "8":
                        user.Show(collection);
                        break;
                    case "9":
                        user.getById(collection);
                        break;
                    case "10":
                        LogOut(auth);
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                }
                if (Auth.LoggedIn == null) break;

            }
            Authorization(auth, collection);
        }
        static void customerView(Auth auth, ICollection collection)
        {
            User user = Auth.LoggedIn;
            while (true)
            {

                Console.Write(CustomerHelpMessage());
                string task = Console.ReadLine();
                switch (task)
                {
                    case "1":
                        user.Sort(collection);
                        break;
                    case "2":
                        user.Search(collection);
                        break;
                    case "3":
                        user.Show(collection);
                        break;
                    case "4":
                        user.getById(collection);
                        break;
                    case "5":
                        LogOut(auth);
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                }
                if (Auth.LoggedIn == null) break;
            }
            Authorization(auth, collection);
        }
        static void Authorization(Auth auth, ICollection collection)
        {
            while (true)
            {
                Console.WriteLine("Authorization:");
                Console.WriteLine("1) Register\n2) Login");
                string task = Console.ReadLine();
                switch (task)
                {
                    case "1":
                        Register(auth);
                        break;
                    case "2":
                        LogIn(auth);
                        break;
                }
                if (Auth.LoggedIn != null)
                {
                    if (Auth.LoggedIn.role == Role.admin) adminView(auth, collection);
                    if (Auth.LoggedIn.role == Role.customer) customerView(auth, collection);
                    break;
                }
            }
        }
            static void Main(string[] args)
            {
                Collection collection = new Collection();
                PermissionProxy perProxy = new PermissionProxy(collection);
                LoggerProxy logProxy = new LoggerProxy(perProxy);
                Auth auth = new Auth();

                Authorization(auth, logProxy);
            }
        
    }
}
