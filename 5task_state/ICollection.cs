using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp5_state_pattern
{
    public interface ICollection : IEnumerable<Product>
    {
        Product Append();
        int Size();
        Product this[int i] { get;set; }
        string Delete();
        Product getById();
        Dictionary<string, string> Edit();
        string Search();
        string Sort();
        string WriteToJson();
        void deleteByID(string id);
        void Show();
        string ReadJsonFile();
    }
}
