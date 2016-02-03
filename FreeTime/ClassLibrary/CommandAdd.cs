using System;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    public class CommandAdd : Command
    {
        private string command;

        public CommandAdd()
        {
            command = "add <message>";
        }

        public string Execute(Arguments arg, Stream stream)
        {
            var repository = new RepositoryText(stream);
            repository.Add(new Activity(arg["<message>"]));
            return "Added a new activity";
        }

        public string Info()
        {
            var info = new StringBuilder();
            info.Append(command);
            info.Append(' ', 55 - command.Length);
            info.Append("Add a new activity.");
            info.Append(Environment.NewLine);
            return info.ToString();
        }

        public string Value() => command;
    }
}