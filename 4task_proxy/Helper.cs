using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace vp4_proxy
{
    internal class Helper
    {
        public static bool IdValid(string ID)
        {
            Regex regex = new Regex("^[0-9]{9}$");
            return regex.IsMatch(ID);
        }
        public static bool NameValid(string name)
        {
            Regex regex = new Regex("[A-Z][A-Za-z]{2,25}(\\s[A-Z][A-Za-z]{2,25})?$");
            return regex.IsMatch(name);
        }
        public static bool EmailValid(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }
        public static bool UrlValid(string url)
        {
            Regex regex = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
            return regex.IsMatch(url);
        }
        public static bool PasswordValid(string password) 
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
        }
        public static bool IsPositiveFloat(string num)
        {
            float res;
            return (float.TryParse(num, out res) && res > 0);
        }
        public static DateTime DateValidation(string d)
        {
            Regex regex = new Regex("(?:0[1-9]|[12][0-9]|3[01])[-](?:0[1-9]|1[012])[-](?:19\\d{2}|20[01][0-9]|20[2][0-2])");
            if (regex.IsMatch(d))
            {
                string[] dateParts = d.Split('-');
                int day = int.Parse(dateParts[0]);
                int month = int.Parse(dateParts[1]);
                int year = int.Parse(dateParts[2]);
                return new DateTime(year, month, day);
            }
            else
            {
                throw new ArgumentException("It's not date.");
            }
        }
        public static string FileNameValidation(string filename, string end = "txt")
        {
            if (!filename.EndsWith("." + end))
            {
                throw new ArgumentException("Incorrect filename, should end with ." + end + ".");
            }
            return filename;
        }
        public static bool FileExist(string file)
        {
            return File.Exists(ConvertFileToLocalPath(file));
        }
        public static string ConvertFileToLocalPath(string file)
        {
            return "C:\\Users\\Professional\\source\\repos\\vp2_class\\vp2_class\\" + file;
        }
        public static void ValidateInput(Action<Collection> func, Collection m)
        {
            while (true)
            {
                try
                {
                    func(m);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try one more time!");
                    continue;
                }
            }
        }
        public static void CompareDates(DateTime less, DateTime more)
        {
            if (less > more)
            {
                throw new ArgumentException("Incorrect date!");
            }
        }
        public static bool isInRange(int toCheck, int min, int max)
        {
            return (toCheck >= min && toCheck <= max);
        }
        public static int positiveIntInput()
        {
            int num;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out num) && num > 0) break;
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
            return num;
        }
        public static (int, int) rangeInput()
        {
            int a, b;
            while (true)
            {
                Console.WriteLine("Enter range [a b]");
                string input = Console.ReadLine();
                string[] nums = input.Split(' ');

                if (nums.Length != 2)
                { continue; }
                if (int.TryParse(nums[0], out a) &&
                    int.TryParse(nums[1], out b) &&
                    a < b &&
                    isInRange(a, 1000, 9999) &&
                    isInRange(b, 1000, 9999))
                {
                    break;
                }
                else
                { Console.WriteLine("Wrong range input"); }
            }
            return (a, b);
        }
        public static void PrintList(List<int> numbers)
        {
            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();
        }
    }
}

