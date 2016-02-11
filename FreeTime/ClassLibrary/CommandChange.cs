namespace ClassLibrary
{
    using System;
    using System.IO;
    using System.Text;

    public class CommandChange : Command
    {
        private string command;

        public CommandChange()
        {
            command = "change <id> [--project:<project>] [--date:<date>] [--message:<message>]";
        }

        public string Execute(Arguments arg, Stream stream)
        {
            var change = "";
            var repository = new RepositoryXML(stream);
            if (arg["[--date:<date>]"] != "")
            {
                repository.Change(arg["<id>"], "date", arg["[--date:<date>]"]);
                change += " date";
            }
            if (arg["[--message:<message>]"] != "")
            {
                repository.Change(arg["<id>"], "message", arg["[--message:<message>]"]);
                change += " message";
            }

            if (arg["[--project:<project>]"] != "")
            {
                repository.Change(arg["<id>"], "project", arg["[--project:<project>]"]);
                change += " project";
            }
            return "Cange" + change;
        }

        public string Info()
        {
            var info = new StringBuilder();
            var newLine = Environment.NewLine;
            info.Append(command + newLine);
            info.Append($"       Changes the activity message, date or both.{newLine}{newLine}");
            return info.ToString();
        }

        public string Value() => command;
    }
}