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
                            var html = new RepositoryHTML(streamHTML);
                            html.Add(list);
                        }
                        else if(arg["[-csv]"] == "true")
                        {
                            var CSV_path = Directory.GetCurrentDirectory() + @"\RazvanCSV.csv";
                            var streamCSV = File.Open(CSV_path, FileMode.Create);
                            var csv = new RepositoryCSV(streamCSV);
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

                }
                else if (args[0] == "help")
                {

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
