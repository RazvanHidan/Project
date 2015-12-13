using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    interface IElement<T>
    {
        void Create(T obj);
        IEnumerable<T> Read();
        void Change();
        void Delete();
    }
}
