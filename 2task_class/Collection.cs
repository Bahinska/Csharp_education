using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vp2_class
{
    public class Collection
    {
        private List<Product> lst = new List<Product>();
        public List<string> idList = new List<string>();

        public Collection(params Product[] products)
        {
            foreach (Product product in products)
            {
                Append(product);
            }
        }
        public Product this[int i]
        {
            get { return lst[i]; }
            set { lst[i] = value; }
        }

        public int Count
        {
            get { return lst.Count; }
        }
        public void Append(Product product)
        {
            if (!idList.Contains(product.Id))
            {
                lst.Add(product);
                idList.Add(product.Id);
            }
            else
            {
                throw new ArgumentException("Two elements with the same id cannot exist!");
            }
        }
        public void Delete(string id)
        {
            bool removed = false;
            foreach (Product product in lst.ToList())
            {
                if (product.Id == id)
                {
                    lst.Remove(product);
                    idList.Remove(product.Id);
                    removed = true;
                    break;
                }
            }
            if (!removed)
            {
                throw new ArgumentException("No product with such ID found");
            }
        }
        public Product GetById(string id)
        {
            Product result = lst.FirstOrDefault(x => x.Id == id);
            if (result == null) throw new ArgumentException("No elements with such id");
            return result;

        }

        public void Edit(Product productToEdit, string parameter)
        {
            try
            {
                if (parameter.ToLower() == "id")
                {
                    Console.WriteLine("Enter new ", parameter);

                    string newId = Console.ReadLine();
                    newId = Helper.IdValid(newId) ? newId : throw new ArgumentException("It's not proper id.");
                    if (idList.Contains(newId))
                    {
                        throw new ArgumentException("Mistake! Value with this id already exists");
                    }
                    else
                    {
                        idList.Remove(productToEdit.Id);
                        idList.Add(newId);
                        productToEdit.Id = newId;
                    }
                }
                else
                {
                    Product.edit_product(productToEdit, parameter);
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Collection Search(string searchString)
        {
            Collection foundProducts = new Collection();

            foreach (Product product in lst)
            {
                if (product.Search(searchString))
                {
                    foundProducts.Append(product);
                }
            }
            return foundProducts;
        }

        public void Sort(string field)
        {
            string proper = char.ToUpper(field[0]) + field.Substring(1);
            lst = lst.OrderBy(prod => prod.GetType().GetProperty(proper).GetValue(prod, null)).ToList();
        }

        public void WriteToTxt(string fileName, string mode = "w")
        {
            var local = Helper.ConvertFileToLocalPath(fileName);
            using (StreamWriter writer = new StreamWriter(local, mode == "w" ? false : true))
            {
                foreach (Product product in lst)
                {
                    writer.WriteLine(product.ToString());
                }
            }
        }
        public void WriteToJson(string fileName)
        {
            var localPath = Helper.ConvertFileToLocalPath(fileName);

            var temp = new List<Dictionary<string, object>>();
            foreach (var ob in lst)
            {
                var dictionary = ob.get_dictionary();
                dictionary["price"] = dictionary["price"].ToString();
                temp.Add(dictionary);
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var json = JsonSerializer.Serialize(temp, options);
            File.WriteAllText(localPath, json);
        }
        public void Show()
        {
            foreach(Product element in lst)
            {
                Console.WriteLine(element.ToString());
            }
        }
        public void ReadJsonFile(string fileName)
        {
            Helper.FileNameValidation(fileName, "json");
            string path = Helper.ConvertFileToLocalPath(fileName);
            var json = File.ReadAllText(path);
            var file = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);
            foreach (var product in file)
            {
                try
                {
                    if (!idList.Contains(product["id"]))
                    {
                        product["price"] = product["price"].ToString();
                        lst.Add(new Product(product));
                        idList.Add(product["id"]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }

        }
    }
}