using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp1_sequence
{
    public class Helpers
    {
        
        public static bool isInRange(int toCheck, int min, int max)
        {
            return (toCheck >= min && toCheck <= max);
        }
        public static int positiveIntInput()
        {
            int num;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out num) && num > 0)break;
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
