using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ArgumentMissing:Exception
    {
        public ArgumentMissing()
        {
        }

        public ArgumentMissing(string message) : base(message)
        {
        }
    }
}
