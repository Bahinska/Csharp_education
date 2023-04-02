using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp4_proxy
{
    internal class PermissionProxy : ICollection
    {
        private readonly ICollection _collection;
        public PermissionProxy(Collection collection)
        {
            _collection= collection;
        }
        public Product Append()
        {
            if (CheckAccess()) return _collection.Append();
            else throw new Exception("Access denyed!");
        }
        public string Delete()
        {
            if (CheckAccess()) return _collection.Delete();
            else throw new Exception("Access denyed!");
        }

        public Dictionary<string,string> Edit()
        {
            if (CheckAccess()) return _collection.Edit();
            else throw new Exception("Access denyed!");
        }

        public Product getById()
        {
            return _collection.getById();
        }

        public string ReadJsonFile()
        {
            if (CheckAccess()) return _collection.ReadJsonFile();
            else throw new Exception("Access denyed!");
        }

        public string Search()
        {
             return _collection.Search();
        }

        public void Show()
        {
            _collection.Show();
        }

        public string Sort()
        {
            return _collection.Sort();
        }
        public string WriteToJson()
        {
            if (CheckAccess()) return _collection.WriteToJson();
            else throw new Exception("Access denyed!");
        }
        private bool CheckAccess() 
        {
            return Auth.LoggedIn.role == Role.admin; 
        }

    }
}
