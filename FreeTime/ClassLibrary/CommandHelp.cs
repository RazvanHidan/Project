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
            var newLine = Environment.NewLine;
            info.Append(command+newLine);
            info.Append($"       Provides Help information for available commands.{newLine}{newLine}");
            return info.ToString();
        }

        public string Value() => command;
    }
}