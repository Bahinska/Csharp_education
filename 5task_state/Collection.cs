using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace vp5_state_pattern
{
    public class Collection : ICollection
    {
        private List<Product> lst = new List<Product>();
        public List<string> idList = new List<string>();

        public Collection(params Product[] products)
        {
            foreach (Product product in products)
            {
                append(product);
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
        public Product Append()
        {
            Dictionary<string, string> d = Product.Input_product("id", "title", "image_url", "price", "created_at", "updated_at", "description");
            var product = new Product(d);
            if (!idList.Contains(product.Id))
            {
                lst.Add(product);
                idList.Add(product.Id);
                Console.WriteLine("Successfully added!\n");
            }
            else
            {
                throw new ArgumentException("Two elements with the same id cannot exist!");
            }
            return product;
        }
        public void Add(Product product)
        {
            lst.Add(product);
            idList.Add(product.Id);
        }
        public void Remove(Product product)
        {
            lst.Remove(product);
            idList.Remove(product.Id);
        }
        public string Delete()
        {
            Console.WriteLine("Element to delete id: ");
            string id = Console.ReadLine();
            id = Helper.IdValid(id) ? id : throw new ArgumentException("It's not proper id.");
            bool removed = false;
            foreach (Product product in lst.ToList())
            {
                if (product.Id == id)
                {
                    lst.Remove(product);
                    idList.Remove(product.Id);
                    removed = true;
                    Console.WriteLine("Successfully deleted!\n");
                    break;
                }
            }
            if (!removed)
            {
                throw new ArgumentException("No product with such ID found");
            }
            return id;
        }
        public Product GetById(string id)
        {
            Product result = lst.FirstOrDefault(x => x.Id == id);
            if (result == null) throw new ArgumentException("No elements with such id");
            return result;

        }

        public Dictionary<string,string> Edit()
        {
            var data = new Dictionary<string,string>();
            Console.WriteLine("Which Product you want to edit? (input id) ");
            string edit_id = Console.ReadLine();
            edit_id = Helper.IdValid(edit_id) ? edit_id : throw new ArgumentException("It's not proper id.");
            if (!idList.Contains(edit_id))
            {
                throw new ArgumentException("Mistake! Value with this id does not exist");
            }
            data["id"] = edit_id;
            string[] possible = new string[] { "title", "image_url", "price", "created_at", "updated_at", "description", "id" };
            Console.WriteLine("Enter field for which you want to edit: \n" +
                           "POSSIBLE: id, title, image_url, price, created_at, updated_at, description:\n");
            string parameter = Console.ReadLine();
            if (possible.Contains(parameter.ToLower()))
            {
                data["parameter"] = parameter;
                Product productToEdit = this.GetById(edit_id);
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
                            data["oldValue"] = productToEdit.Id;
                            data["newValue"] = newId;
                            idList.Remove(productToEdit.Id);
                            idList.Add(newId);
                            productToEdit.Id = newId;
                        }
                    }
                    else
                    {
                        Console.Write("Enter new " + parameter + ": ");
                        string new_val = Console.ReadLine();
                        var dict = productToEdit.get_dictionary();
                        Dictionary<string, string> new_prod_dict = dict.ToDictionary(k => k.Key, k => k.Value.ToString());
                        data["oldValue"] = productToEdit.Id;
                        data["newValue"] = new_val;
                        this.deleteByID(productToEdit.Id);
                        new_prod_dict[parameter.ToLower()] = new_val;
                        this.append(new Product(new_prod_dict));
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                throw new ArgumentException("Incorrect edit request");
            }
            return data;
        }
        public void deleteByID(string id)
        {
            foreach (Product product in lst.ToList())
            {
                if (product.Id == id)
                {
                    lst.Remove(product);
                    idList.Remove(product.Id);
                }
            }
        }
        private void append(Product product)
        {
            lst.Add(product);
            idList.Add(product.Id);
        }
        public string Search()
        {
            Console.Write("Search query: ");
            string searchString = Console.ReadLine();
            Collection foundProducts = new Collection();
            foreach (Product product in lst)
            {
                if (product.Search(searchString))
                {
                    foundProducts.append(product);
                }
            }
            if (foundProducts.Count > 0)
            {
                Console.WriteLine("Results of search:\n");
                for (int i = 0; i < foundProducts.Count; i++)
                {
                    Console.WriteLine(foundProducts[i].ToString());
                }
            }
            else
            {
                Console.WriteLine("Nothing was found :(");
            }
            return searchString;
        }

        public string Sort()
        {
            string[] possible = { "title", "image_url", "price", "created_at", "updated_at", "description", "id" };
            Console.Write("Enter field for which you want to sort:\nPOSSIBLE: id, title, image_url, price, created_at, updated_at, description:\n");
            string field = Console.ReadLine();
            if (possible.Contains(field.ToLower()))
            {
                string proper = char.ToUpper(field[0]) + field.Substring(1);
                lst = lst.OrderBy(prod => prod.GetType().GetProperty(proper).GetValue(prod, null)).ToList();
            }
            else
            {
                throw new ArgumentException("Incorrect sort request");
            }
            return field;
        }

        public string WriteToTxt()
        {
            Console.WriteLine("Enter file name:");
            string fileName = Helper.FileNameValidation(Console.ReadLine(), "txt");
            if (Helper.FileExist(fileName))
            {
                if (Helper.FileExist(fileName))
                {
                    var local = Helper.ConvertFileToLocalPath(fileName);
                    using (StreamWriter writer = new StreamWriter(local, true))
                    {
                        foreach (Product product in lst)
                        {
                            writer.WriteLine(product.ToString());
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("File does not exist!");
                }
            }
            else
            {
                throw new ArgumentException("File does not exist!");
            }
            return fileName;
        }
        public string WriteToJson()
        {
            Console.WriteLine("Enter file name:");
            string file = Helper.FileNameValidation(Console.ReadLine(), "json");
            if (Helper.FileExist(file))
            {
                var localPath = Helper.ConvertFileToLocalPath(file);

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
            else
            {
                throw new ArgumentException("File does not exist!");
            }
            return file;
        }
        public void Show()
        {
            foreach (Product element in lst)
            {
                Console.WriteLine(element.ToString());
            }
        }
        public string ReadJsonFile()
        {
            Console.WriteLine("Enter file name:");
            string file = Helper.FileNameValidation(Console.ReadLine(), "json");
            if (Helper.FileExist(file))
            {
                string path = Helper.ConvertFileToLocalPath(file);
                var json = File.ReadAllText(path);
                var file_data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);
                foreach (var product in file_data)
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
            else
            {
                throw new ArgumentException("File does not exist!");
            }
            return file;
        }

        public bool Contains(string id)
        {
            return idList.Contains(id);
        }

        public Product getById()
        {
            Console.WriteLine("Input Id:");
            string id = Console.ReadLine();
            id = Helper.IdValid(id) ? id : throw new ArgumentException("It's not proper id.");
            Product result = lst.FirstOrDefault(x => x.Id == id);
            if(result != null) Console.WriteLine(result.ToString());
            if (result == null) throw new ArgumentException("No elements with such id");
            return result;
        }

        public int Size()
        {
            return lst.Count;
        }
        private IEnumerable<Product> Events()
        {
            for (int i = 0; i < lst.Count; i++)
            {
                yield return lst[i];
            }
        }
        public IEnumerator<Product> GetEnumerator()
        {
            return Events().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
