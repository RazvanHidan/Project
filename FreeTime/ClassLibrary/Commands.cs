using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Commands
    {
        private Command command;
        private string[] args;
        private Stream stream;

        public Commands(string[] args,Stream stream)
        {
            this.stream = stream;
            this.args = args;
            CheckThatThereAreArguments(args);
            ParseArguments(args);
        }

        private void ParseArguments(string[] args)
        {
            var commandLine = new CommandPattern(args);
            if (commandLine.IsAdd())
                command = new CommandAdd();
            else if (commandLine.IsList())
                command = new CommandList();
            else if (commandLine.IsChange())
                command = new CommandChange();
            else if (commandLine.IsHelp())
                command = new CommandHelp();
            else
                throw new InvalidArgument(args[0]);
        }

        public string Execute() => command.Execute(new Arguments(command.Value(), args), stream);

        private static void CheckThatThereAreArguments(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentMissing();
        }
    }
}
