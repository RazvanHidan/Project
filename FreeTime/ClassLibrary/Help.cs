using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Help
    {
        public string help;
        public  Help()
        {
            var nl = Environment.NewLine;
            help = nl+"These shell commands are defined internally.  Type `help' to see this list.";
            help += $"{nl}Available commands:{nl}add <message>{nl}list [week] [-csv] [-html]{nl}change <id> [--message<message>] [--date<date>]";
        }
    }
}
