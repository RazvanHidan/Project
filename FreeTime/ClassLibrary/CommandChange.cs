using System;
using System.IO;

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

        public string Value() => command;
    }
}