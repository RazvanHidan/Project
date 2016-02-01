using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public interface Command
    {
        string Execute(Arguments arg, Stream stream);
        string Value();
    }
}
