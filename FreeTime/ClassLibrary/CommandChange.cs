using System;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    public class CommandChange : Command
    {
        private string command;

        public CommandChange()
        {
            command = "change <id> [--message:<message>] [--date:<date>]";
        }

        public string Execute(Arguments arg, Stream stream)
        {
            var change = "";
            var repository = new RepositoryText(stream);
            if (arg["[--date:<date>]"] != "")
            {
                repository.ChangeDate(arg["<id>"], arg["[--date:<date>]"]);
                change += " date";
            }
            if (arg["[--message:<message>]"] != "")
            {
                repository.ChangeMessage(arg["<id>"], arg["[--message:<message>]"]);
                change += " message";
            }
            return "Cange" + change;
        }

        public string Info()
        {
            var info = new StringBuilder();
            info.Append(command);
            info.Append(' ', 55 - command.Length);
            info.Append("Changes the activity message, date or both.");
            info.Append(Environment.NewLine);
            return info.ToString();
        }

        public string Value() => command;
    }
}