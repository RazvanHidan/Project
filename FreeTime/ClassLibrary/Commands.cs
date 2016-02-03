

namespace ClassLibrary
{
    using System.Collections.Generic;
    using System.IO;

    public class Commands
    {
        private Command command;
        private string[] args;
        private Stream stream;
        private readonly Dictionary<string, Command> dictonary = new Dictionary<string, Command>
        {
            {"add",new CommandAdd() },
            {"list",new CommandList() },
            {"change", new CommandChange() },
            {"help",new CommandHelp() }
        };

        public Commands(string[] args,Stream stream)
        {
            this.stream = stream;
            this.args = args;
            CheckThatThereAreArguments(args);
            ParseArguments(args);
        }

        private void ParseArguments(string[] args)
        {
            if (dictonary.ContainsKey(args[0]))
                command=dictonary[args[0]];
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
