using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vp5_state_pattern
{
    internal class Program
    {
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
                try
                {
                    Console.Write(Helper.AdminHelpMessage);
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
                            Show(collection);
                            break;
                        case "9":
                            user.getById(collection);
                            break;
                        case "10":
                            manageDrafts(collection);
                            break;
                        case "11":
                            manageModerations(collection);
                            break;
                        case "12":
                            LogOut(auth);
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                if (Auth.LoggedIn == null) break;

            }
            Authorization(auth, collection);
        }
        static void managerView(Auth auth, ICollection collection)
        {
            User user = Auth.LoggedIn;
            while (true)
            {
                try
                {
                    Console.Write(Helper.ManagerHelpMessage);
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
                            user.Edit(collection);
                            break;
                        case "6":
                            user.WriteInJson(collection);
                            break;
                        case "7":
                            Show(collection);
                            break;
                        case "8":
                            user.getById(collection);
                            break;
                        case "9":
                            manageDrafts(collection);
                            break;
                        case "10":
                            LogOut(auth);
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                if (Auth.LoggedIn == null) break;
            }
            Authorization(auth, collection);
        }
        static void customerView(Auth auth, ICollection collection)
        {
            User user = Auth.LoggedIn;
            while (true)
            {
                try
                {
                    Console.Write(Helper.CustomerHelpMessage);
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
                            Show(collection);
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
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                if (Auth.LoggedIn == null) break;
            }
            Authorization(auth, collection);
        }
        static void manageModerations(ICollection collection)
        {
            var modetationCollection = new Collection();

            foreach(var product in collection)
            {
                if (product.State is ModerationState)
                {
                    Console.WriteLine(product);
                    modetationCollection.Add(product);
                }
            }
            while (true)
            {
                Console.WriteLine("1 - Publish specific \n2 - Publish all\n3 - return");
                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        Console.WriteLine("Which item tou want to publish? (id)");
                        string Id  = Console.ReadLine();
                        Id = Helper.IdValid(Id) ? Id : throw new ArgumentException("It's not proper id.");
                        var prod = modetationCollection.GetById(Id);

                        prod.State.NextStage();
                        Console.WriteLine("Item published!");
                        break;
                    case "2":
                        foreach(var p in modetationCollection){p.State.NextStage();}
                        Console.WriteLine("All items published!");
                        break;
                     case "3":
                        break;
                }
                break;
            }
        }

        static void manageDrafts(ICollection collection)
        {
            var draftCollection = new Collection();

            foreach (var product in collection)
            {
                if (product.State is DraftState)
                {
                    Console.WriteLine(product);
                    draftCollection.Add(product);
                }
            }
            while (true)
            {
                Console.WriteLine("1 - Check specific \n2 - Check all\n3 - return");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Which item you want sent to check? (id)");
                        string Id = Console.ReadLine();
                        Id = Helper.IdValid(Id) ? Id : throw new ArgumentException("It's not proper id.");
                        var prod = draftCollection.GetById(Id);
                        prod.State.NextStage();
                        Console.WriteLine("Item sent to check!");
                        break;
                    case "2":
                        foreach(var p in draftCollection){p.State.NextStage();}
                        Console.WriteLine("All items sent to check!");
                        break;
                    case "3":
                        break;
                }
                break;
            }
        }
        static void Show(ICollection collection)
        {
            foreach (var product in collection)
            {
                if (product.State is PublishedState)
                {
                    Console.WriteLine(product);
                }
            }
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
                    if (Auth.LoggedIn.role == Role.manager) managerView(auth, collection);
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
