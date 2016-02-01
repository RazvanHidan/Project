using System;
using System.IO;

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

        public string Value() => command;
    }
}