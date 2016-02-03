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
            var allinfo = new StringBuilder();
            allinfo.Append($"{nl}These shell commands are defined internally.  Type `help' to see this list.");
            allinfo.Append($"{nl}Available commands:{nl}{nl}");
            allinfo.Append(new CommandAdd().Info());
            allinfo.Append(new CommandChange().Info());
            allinfo.Append(new CommandHelp().Info());
            allinfo.Append(new CommandList().Info());
            help = allinfo.ToString();
        }
    }
}
