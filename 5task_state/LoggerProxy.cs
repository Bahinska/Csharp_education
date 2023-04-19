using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp5_state_pattern
{
    internal class LoggerProxy : ICollection
    {
        private readonly ICollection _permissionProxy;

        public Product this[int i] { get => _permissionProxy[i]; set => _permissionProxy[i] = value; }

        public LoggerProxy(PermissionProxy permissionProxy) 
        {
            _permissionProxy = permissionProxy;
        }
        public Product Append()
        {
            string logs;
            Product res;
            try
            {
                res = _permissionProxy.Append();
                logs = 
                "Add :" +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                "Product: " +
                "{" + res.ToLog() + "}";
            }
            catch (Exception ex)
            {
                logs =
                "FAILED Add :" +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                ex.Message;
                Console.WriteLine(ex.Message);
                res = null;
            }
            Log(logs);
            return res;
        }

        public string Delete()
        {
            string logs;
            string res;
            try
            {
                res = _permissionProxy.Delete();
                logs =
                "Delete: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                "delete id: " + res.ToString();
            }
            catch (Exception ex)
            {
                logs =
                "FAILED Delete: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                ex.Message;
                Console.WriteLine(ex.Message);
                res = null;
            }
            Log(logs);
            return res;
        }

        public Dictionary<string, string> Edit()
        {
            string logs;
            Dictionary<string, string> res;
            try
            {
                res = _permissionProxy.Edit();
                logs =
                 "Edit: " +
                 Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                 DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                 "Edit_id: " + res["id"] + " " +
                 "Edit_parameter: " + res["parameter"] + " " +
                 "{" + res["oldValue"] + " -> " + res["newValue"] + "}";

            }
            catch (Exception ex)
            {
                logs =
                 "FAILED Edit: " +
                 Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                 DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                ex.Message;
                Console.WriteLine(ex.Message);
                res = null;
            }
            Log(logs);
            return res;
        }

        public Product getById()
        {
            string logs;
            Product res;
            try
            {
                res = _permissionProxy.getById();
                logs =
                "Get by id: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                "Prouct: " + "{" + res.ToLog() + "}";

            }
            catch (Exception ex)
            {
                logs =
                "FAILED Get by id: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                ex.Message;
                Console.WriteLine(ex.Message);
                res = null;

            }
            Log(logs);
            return res;
        }

        public string ReadJsonFile()
        {
            string logs;
            string res;
            try
            {
                res = _permissionProxy.ReadJsonFile();
                logs =
                "Read json file: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                "File: " + res;
            }
            catch(Exception ex)
            {
                logs =
                "FAILED Read json file: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                ex.Message;
                Console.WriteLine(ex.Message);
                res = null;
            }
            Log(logs);
            return res;
        }

        public string Search()
        {
            string logs;
            string res;
            try
            {
                res = _permissionProxy.Search();
                logs =
                "Search: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                "Search_by: " + res;
            }
            catch(Exception ex) 
            {
                logs =
                "FAILED Search: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                ex.Message;
                Console.WriteLine(ex.Message);
                res = null;
            }
            Log(logs);
            return res;
        }

        public void Show()
        {
            _permissionProxy.Show();
            string logs =
                "Show: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " ";
            Log(logs);
        }

        public string Sort()
        {
            string logs;
            string res;
            try
            {
                res = _permissionProxy.Sort();
                logs =
                "Sort: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                "Sort_by:" + res;
            }
            catch (Exception ex)
            {
                logs =
                     "FAILED Sort: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                ex.Message;
                Console.WriteLine(ex.Message);
                res = null;
            }
            Log(logs);
            return res;
        }

        public string WriteToJson()
        {
            string logs;
            string res;
            try
            {
                res = _permissionProxy.WriteToJson();
                logs =
                "Write to json file: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                "File: " + res;
            }
            catch (Exception ex)
            {
                logs =
                    "FAILED Write to json file: " +
                Auth.LoggedIn.firstName + " " + Auth.LoggedIn.lastName + "; " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm") + " " +
                ex.Message;
                Console.WriteLine(ex.Message);
                res = null;
            }
            Log(logs);
            return res;
        }

        public void Log(string Logs)
        {
            string logFilePath = "C:\\Users\\Professional\\source\\repos\\vp4_proxy\\vp4_proxy\\Logs.txt";

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(Logs);
            }
            
        }

        public void deleteByID(string id)
        {
            throw new NotImplementedException();
        }

        public int Size()
        {
            return _permissionProxy.Size();
        }
        private IEnumerable<Product> Events()
        {
            for (int i = 0; i < _permissionProxy.Size(); i++)
            {
                yield return _permissionProxy[i];
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
