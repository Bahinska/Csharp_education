using System.Collections.Generic;
using System.ComponentModel.Design;


namespace vp1_sequence
{
    internal class Program
    {
        static void taskRand()
        {
            Console.WriteLine("Enter size of array");
            var size = Helpers.positiveIntInput();
            (int min ,int max) = Helpers.rangeInput();

            Random rand = new Random();
            List<int> randomList = new List<int>();
            for (int i = 0; i < size; i++)
            {
                randomList.Add(rand.Next(min, max + 1));
            }

            Console.Write("List: ");
            Helpers.PrintList(randomList);
            var result = new List<int>(procesing(randomList));
            Console.Write("Result: ");
            Helpers.PrintList(result);

        }

        static void taskUser()
        {
            Console.WriteLine("Enter size of array");
            var size = Helpers.positiveIntInput();
            Console.WriteLine("Input nums from 1000 to 9999");
            List<int> list = new List<int>();
            for(int i = 0; i<size; i++)
            {
                int num = Helpers.positiveIntInput();
                if (Helpers.isInRange(num, 1000, 9999)) 
                {
                    list.Add(num);
                }
                else
                {
                    Console.WriteLine("Wrong input. Continue in range [1000;9999]");
                    i--;
                }
            }
            Console.Write("List: ");
            Helpers.PrintList(list);
            var result = new List<int>(procesing(list));
            Console.Write("Result: ");
            Helpers.PrintList(result);
        }
        static List<int> procesing(List<int> list) 
        {
            var result = new List<int>(list);
            foreach(int num in list) 
            {
                string val = num.ToString();
                char x = val[0];
                char y = val[1];
                if ((val[2] == x) && (val[3] == y) || (val[2] == y && val[3] == x))
                {
                    continue;
                }
                else
                {
                    result.Remove(num);
                    result.Add(0);
                }
            }
            return result;
        }
        static void menu()
        {
            while (true) 
            {
                Console.WriteLine("Enter 1 for your array\nEnter 2 for random array\nEnter 3 to exit");
                string choice = Console.ReadLine();
                if(choice=="1")
                {
                    taskUser();
                }
                if (choice == "2")
                {
                    taskRand();
                }
                if (choice == "3")
                {
                    break;
                }
            }
        }
        static void Main(string[] args)
        {
            menu();
        }
    }
}