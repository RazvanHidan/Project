using System;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    public class CommandHelp : Command
    {
        private string command;

        public CommandHelp()
        {
            command = "help";
        }

        public string Execute(Arguments arg, Stream stream)
        {
            return new Help().help;
        }

        public string Info()
        {
            var info = new StringBuilder();
            info.Append(command);
            info.Append(' ', 55 - command.Length);
            info.Append("Provides Help information for available commands.");
            info.Append(Environment.NewLine);
            return info.ToString();
        }

        public string Value() => command;
    }
}