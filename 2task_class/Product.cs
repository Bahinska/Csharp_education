using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vp2_class
{
    public class Product
    {
        private string title;
        private string id;
        private string image_url;
        private float price;
        private DateTime created_at;
        private DateTime updated_at;
        private string description;


        public string Title
        {
            get { return title; }
            set { title = Helper.NameValid(value)? value: throw new ArgumentException("It's not proper title.");}
        }
        public string Id
        {
            get { return id; }
            set { id = Helper.IdValid(value.ToString())? value: throw new ArgumentException("It's not proper id.");}
        }
        public string Image_url
        {
            get { return image_url; }
            set { image_url = Helper.UrlValid(value)?value: throw new ArgumentException("It's not proper url.");}
        }
        public float Price
        {
            get { return price; }
            set { price = Helper.IsPositiveFloat(value.ToString()) ? (float)value : throw new ArgumentException("It's not proper price.");}
        }

        public DateTime Created_at
        {
            get { return created_at; }
            set { created_at = Helper.DateValidation(value.ToString("dd-MM-yyyy")); }
        }

        public DateTime Updated_at
        {
            get { return updated_at; }
            set { updated_at = Helper.DateValidation(value.ToString("dd-MM-yyyy")); }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public Product(Dictionary<string, string> properties)
        {
            string er = "";
            foreach (KeyValuePair<string, string> prop in properties)
            {
                try
                {
                    switch (prop.Key)
                    {
                        case "id":
                            Id = prop.Value;
                            break;
                        case "title":
                            Title = prop.Value;
                            break;
                        case "image_url":
                            Image_url = prop.Value;
                            break;
                        case "price":
                            Price = float.Parse(prop.Value);
                            break;
                        case "created_at":
                            Created_at = DateTime.Parse(prop.Value);
                            break;
                        case "updated_at":
                            Updated_at = DateTime.Parse(prop.Value);
                            break;
                        case "description":
                            Description = prop.Value;
                            break;
                    }
                }
                catch (FormatException e)
                {
                    er += "\n -- " + e.Message;
                }
                catch (Exception e)
                {
                    er += "\n -- " + e.Message;
                }
            }
            try
            {
                Helper.CompareDates(this.created_at, this.updated_at);
            }
            catch (Exception e)
            {
                er += "\n -- " + e.Message;
            }
            if (er.Length > 0)
            {
                throw new Exception("Element can't be created" + er);
            }
        }

        public bool Search(string searchString)
        {
            var dict = this.get_dictionary();
            return dict.Values.Any(val => val.ToString().Contains(searchString));
        }
    
        public static Dictionary<string, string> Input_product(params string[] args)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            foreach (string prop in args)
            {
                Console.Write(prop + " : ");
                d[prop] = Console.ReadLine();
            }
            return d;
        }
        public static void edit_product(Product el_to_edit, string parameter)
        {
            Console.Write("Enter new " + parameter + ": ");
            string proper = char.ToUpper(parameter[0]) + parameter.Substring(1);
            string new_val = Console.ReadLine();

            PropertyInfo propertyInfo = el_to_edit.GetType().GetProperty(proper);
            var old_val = el_to_edit.GetType().GetProperty(proper).GetValue(el_to_edit, null);
            propertyInfo.SetValue(el_to_edit, Convert.ChangeType(new_val, propertyInfo.PropertyType), null);

            if (parameter == "created_at" || parameter == "updated_at")
            {
                try
                {
                    Helper.CompareDates(el_to_edit.created_at, el_to_edit.updated_at);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    propertyInfo.SetValue(el_to_edit, Convert.ChangeType(old_val, propertyInfo.PropertyType), null);
                }
            }
        }

        public Dictionary<string, object> get_dictionary()
        {
            var dictionary = new Dictionary<string, object>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
            {
                dictionary.Add(property.Name.ToLower(), property.GetValue(this));
            }
            return dictionary;
        }
        public override string ToString()
        {
            return "Product:\n" + string.Join("\n", get_dictionary().Select(kv => $"{kv.Key}: {kv.Value}")) + "\n";
        }
    }
}