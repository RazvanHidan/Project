namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class CommandChange : Command
    {
        private string command;

        public CommandChange()
        {
            command = "change <id> [--project:<project>] [--date:<date>] [--enddate:<enddate>] [--message:<message>]";
        }

        public string Execute(Arguments arg, Stream stream)
        {
            var change = "";
            var repository = new RepositoryXML(stream);
            var activity = new Dictionary<string, string>();
            foreach (var element in command.Split(' '))
                if (arg[element] != ""&&element.StartsWith("[--"))
                {
                    var start = element.IndexOf('<') + 1;
                    var end = element.IndexOf('>');
                    if (start != 0)
                    {
                        activity.Add(element.Substring(start, end - start), arg[element]);
                    }
                }
            foreach (var element in activity)
            {
                repository.Change(arg["<id>"], element.Key, element.Value);
                change += element.Key.ToUpper() + " ";
            }
            return "Change " + change;
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