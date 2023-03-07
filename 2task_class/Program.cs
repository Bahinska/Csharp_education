using Microsoft.VisualBasic;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;

namespace vp2_class
{
    internal class Program
    {
        static void menu()
        {
            Collection collection = new Collection();
            while (true)
            {
                string help_message = GetHelpMessage();
                Console.Write(help_message);
                string task = Console.ReadLine();
                switch (task)
                {
                    case "1":
                        Helper.ValidateInput(ReadJson, collection);
                        break;
                    case "2":
                        Helper.ValidateInput(Sort, collection);
                        break;
                    case "3":
                        Helper.ValidateInput(Search, collection);
                        break;
                    case "4":
                        Helper.ValidateInput(Add, collection);
                        break;
                    case "5":
                        Helper.ValidateInput(Delete, collection);
                        break;
                    case "6":
                        Helper.ValidateInput(Edit, collection);
                        break;
                    case "7":
                        Helper.ValidateInput(WriteInTxt, collection);
                        break;
                    case "8":
                        Helper.ValidateInput(WriteInJson, collection);
                        break;
                    case "9":
                        collection.Show();
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;

                }
            }
        }
        static string GetHelpMessage()
        {
            string help_message = new string('*', 51) + "\n  HELP:" + new string(' ', 42) + "\n  Possible commands:" + new string(' ', 29) +
                        "\n  1  - to read from file. " + new string(' ', 25) +
                        "\n  2  - to sort elements. " + new string(' ', 25) +
                        "\n  3  - to search element.  " + new string(' ', 20) +
                        "\n  4  - to add Product to collection. " + new string(' ', 10) +
                        "\n  5  - to del Product from collection.  " + new string(' ', 10) +
                        "\n  6  - to edit Product in collection.  " + new string(' ', 9) +
                        "\n  7  - to write collection elements to txt file.  " +
                        "\n  8  - to write collection elements to json file. " +
                        "\n  9 - to print collection. " + new string(' ', 22) +
                        "\n  exit - to exit.  " + new string(' ', 30) + "\n" +
                        new string('*', 51) + "\n";
            return help_message;
        }

        static void ReadJson(Collection collection)
        {
            Console.WriteLine("Enter file name:");
            string file = Helper.FileNameValidation(Console.ReadLine(), "json");
            if (Helper.FileExist(file))
            {
                collection.ReadJsonFile(file);
            }
            else
            {
                throw new ArgumentException("File does not exist!");
            }
        }
        static void Sort(Collection collection)
        {
            string[] possible = { "title", "image_url", "price", "created_at", "updated_at", "description", "id" };
            Console.Write("Enter field for which you want to sort:\nPOSSIBLE: id, title, image_url, price, created_at, updated_at, description:\n");
            string input = Console.ReadLine();
            if (possible.Contains(input.ToLower()))
            {
                collection.Sort(input);
            }
            else
            {
                throw new ArgumentException("Incorrect sort request");
            }
        }
        static void Search(Collection collection)
        {
            Console.Write("Search query: ");
            string request = Console.ReadLine();
            var result = collection.Search(request);
            if (result.Count > 0)
            {
                Console.WriteLine("Results of search:\n");
                for(int i=0; i<result.Count; i++)
                {
                    Console.WriteLine(result[i].ToString());
                }
            }
            else
            {
                Console.WriteLine("Nothing was found :(");
            }
        }
        static void Add(Collection collection)
        {
            Dictionary<string, string> d = Product.Input_product("id", "title", "image_url", "price", "created_at", "updated_at", "description");
            try
            {
            collection.Append(new Product(d));
            Console.WriteLine("Successfully added!\n");
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Delete(Collection collection)
        {
            Console.WriteLine("Element to delete id: ");
            string id = Console.ReadLine();
            id = Helper.IdValid(id) ? id : throw new ArgumentException("It's not proper id.");
            collection.Delete(id);
            Console.WriteLine("Successfully deleted!\n");
        }
        static void Edit(Collection collection)
        {
            Console.WriteLine("Which Product you want to edit? (input id) ");
            string edit_id = Console.ReadLine();
            edit_id = Helper.IdValid(edit_id) ? edit_id : throw new ArgumentException("It's not proper id.");
            if (!collection.idList.Contains(edit_id))
            {
                throw new ArgumentException("Mistake! Value with this id does not exist");
            }

            string[] possible = new string[] { "title", "image_url", "price", "created_at", "updated_at", "description", "id" };
            Console.WriteLine("Enter field for which you want to edit: \n" +
                           "POSSIBLE: id, title, image_url, price, created_at, updated_at, description:\n");
            string parameter = Console.ReadLine();
            if (possible.Contains(parameter.ToLower()))
            {
                Product el_to_edit = collection.GetById(edit_id);
                collection.Edit(el_to_edit, parameter);
            }
            else
            {
                throw new ArgumentException("Incorrect edit request");
            }
        }
        static public void WriteInTxt(Collection collection)
        {
            Console.WriteLine("Enter file name:");
            string file = Helper.FileNameValidation(Console.ReadLine(), "txt");
            if (Helper.FileExist(file))
            {
                collection.WriteToTxt(file);
            }
            else
            {
                throw new ArgumentException("File does not exist!");
            }
        }

        static public void WriteInJson(Collection collection)
        {
            Console.WriteLine("Enter file name:");
            string file = Helper.FileNameValidation(Console.ReadLine(), "json");
            if (Helper.FileExist(file))
            {
                collection.WriteToJson(file);
            }
            else
            {
                throw new ArgumentException("File does not exist!");
            }
        }

        static void Main(string[] args)
        {
            menu();
        }
    }
}

