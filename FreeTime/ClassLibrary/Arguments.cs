using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Arguments
    {
        private List<string> arg;

        public Arguments(List<string> arg)
        {
            this.arg = arg;
        }

        public string Parse()
        {
            if (arg[0] == "list")
                return "true";
            else if (arg[1] == "week")
                return "true";
            else
                return "false";
        }

        public string Paarse()
        {
            if (arg[0] == "add")
            {
                return "true";
            }
            else if (arg[0] == "list")
            {
                if (arg.Count == 2 && arg[1] == "week")
                {
                    return "true";
                }
                else
                {
                    return "true";
                }
            }
            else return "false";
        }
    

        public string this[List<string> arg]
        {
            get
            {
                return Parse();
            }
        }
    }
}
