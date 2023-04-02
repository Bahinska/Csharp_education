using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp4_proxy
{
    public interface ICollection
    {
        Product Append();
        string Delete();
        Product getById();
        Dictionary<string, string> Edit();
        string Search();
        string Sort();
        string WriteToJson();
        void Show();
        string ReadJsonFile();
    }
}
