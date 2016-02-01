using System;
using System.IO;

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

        public string Value() => command;
    }
}