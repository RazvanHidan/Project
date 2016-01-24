using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ArgumentsParser
    {
        public ArgumentsParser(string[] args, RepositoryText repository)
        {
            ParseArguments(args, repository);
        }

        private void ParseArguments(string[] args, RepositoryText repository)
        {
            if (args[0] == "add")
            {
                var arg = new Arguments("add <message>", args);
                if (arg["add"] != "false" && arg["<message>"] != "fasle")
                    repository.Add(new Activity(args[1]));
            }
        }
    }
}
