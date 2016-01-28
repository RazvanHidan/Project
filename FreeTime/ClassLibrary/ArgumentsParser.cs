using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ArgumentsParser
    {
        string result = "";

        public ArgumentsParser(string[] args, RepositoryText repository)
        {
            ParseArguments(args, repository);
        }

        private void ParseArguments(string[] args, RepositoryText repository)
        {
            if (args.Length == 0)
                throw new ArgumentMissing();
            else
            {
                if (args[0] == "add")
                {
                    var arg = new Arguments("add <message>", args);
                    repository.Add(new Activity(args[1]));
                }
                else if (args[0] == "list")
                {
                    var arg = new Arguments("list [week] [-csv] [-html]", args);
                    var list = repository.List();
                    if (arg["[-html]"] == "true" || arg["[-csv]"] == "true")
                    {
                        if (arg["[-html]"] == "true")
                        {
                            var HTML_path = Directory.GetCurrentDirectory() + @"\RazvanHTML.html";
                            var streamHTML = File.Open(HTML_path, FileMode.Create);
                            var html = new DocumentHTML(streamHTML);
                            html.Add(list);
                        }
                        else if (arg["[-csv]"] == "true")
                        {
                            var CSV_path = Directory.GetCurrentDirectory() + @"\RazvanCSV.csv";
                            var streamCSV = File.Open(CSV_path, FileMode.Create);
                            var csv = new DocumentCSV(streamCSV);
                            csv.Add(list);
                        }
                    }
                    else
                    {
                        if (arg["[week]"] == "true")
                            list = repository.ListWeek();
                        foreach (var activity in list)
                        {
                            foreach (var element in activity.List().Values)
                                result += $"{element} ";
                            result += Environment.NewLine;
                        }
                    }
                }
                else if (args[0] == "change")
                {
                    var arg = new Arguments("change <id> [--date<date>] [--message<message>]", args);
                    if (arg["[--date<date>]"] != "")
                        repository.ChangeDate(arg["<id>"], args[2]);
                    if (arg["[--message<message>]"] != "")
                        repository.ChangeMessage(arg["<id>"], args[2]);
                }
                else if (args[0] == "del")
                {
                    repository.DeleteActivity(args[1]);
                }
                else if (args[0] == "help")
                {
                    result += "These shell commands are defined internally.  Type `help' to see this list.";
                    var nl = Environment.NewLine;
                    result += $"{nl}Available commands:{nl}add <message>{nl}list [week] [-csv] [-html]{nl}change <id> [--message<message>] [--date<date>]";
                }
                else
                {
                    throw new InvalidArgument($"{args[0]} is invalid");
                }
            }
        }

        public string Result()
        {
            return result;
        }
    }
}
