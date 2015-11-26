using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class User
    {
        string userName;

        public string Name
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }
    }
}
