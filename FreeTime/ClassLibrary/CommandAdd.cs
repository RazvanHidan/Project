namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class CommandAdd : Command
    {
        private string command;

        public CommandAdd()
        {
            command = "add [--project:<project>] [--date:<date>] [--enddate:<enddate>] <message>";
        }

        public string Execute(Arguments arg, Stream stream)
        {
            var repository = new RepositoryXML(stream);
            var activity=new Dictionary<string,string> ();
            foreach (var element in command.Split(' '))
                if (arg[element] != "")
                {
                    var start = element.IndexOf('<') + 1;
                    var end = element.IndexOf('>');
                    if (start!=0)
                    {
                        activity.Add(element.Substring(start, end - start), arg[element]);
                    }
                }
            repository.Add(new Activity(activity));   
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