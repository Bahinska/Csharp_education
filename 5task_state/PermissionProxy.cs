using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp5_state_pattern
{
    internal class PermissionProxy : ICollection
    {
        private readonly ICollection _collection;

        public Product this[int i] { get => _collection[i]; set => _collection[i] = value; }

        public PermissionProxy(Collection collection)
        {
            _collection = collection;
        }
        public Product Append()
        {
            if (CheckAccess()) return _collection.Append();
            else throw new Exception("Access denyed!");
        }
        public string Delete()
        {
            if (Auth.LoggedIn.role == Role.admin) return _collection.Delete();
            else throw new Exception("Access denyed!");
        }

        public void deleteByID(string id)
        {
            throw new NotImplementedException();
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
        private IEnumerable<Product> Events()
        {
            for (int i = 0; i < _collection.Size(); i++)
            {
                yield return _collection[i];
            }
        }
        public IEnumerator<Product> GetEnumerator()
        {
            return Events().GetEnumerator();
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
            return Auth.LoggedIn.role == Role.admin || Auth.LoggedIn.role == Role.manager; 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Size()
        {
            return _collection.Size();
        }
    }
}
