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
            command = "add [--project:<project>] <message>";
        }

        public string Execute(Arguments arg, Stream stream)
        {
            var repository = new RepositoryText(stream);
            if (arg["[--project:<project>]"] != "")
                repository.Add(new Activity(arg["<message>"], arg["[--project:<project>]"]));
            else
                repository.Add(new Activity(arg["<message>"]));
            return "Added a new activity";
        }

        public string Info()
        {
            var info = new StringBuilder();
            var newLine = Environment.NewLine;
            info.Append(command + newLine);
            info.Append($"       Add a new activity.{newLine}{newLine}");
            return info.ToString();
        }

        public string Value() => command;
    }
}