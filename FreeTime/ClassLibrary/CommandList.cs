using System;
using System.IO;

namespace ClassLibrary
{
    public class CommandList : Command
    {
        private string command;

        public CommandList()
        {
            command = "list [week] [csv] [html]";
        }

        public string Execute(Arguments arg, Stream stream)
        {
            var builder = new System.Text.StringBuilder();
            var list = new RepositoryText(stream).List();

            if (arg["[week]"] == "true")
                list = new RepositoryText(stream).ListWeek();
            else if (arg["[html]"] == "true")
            {
                var HTML_path = Directory.GetCurrentDirectory() + @"\RazvanHTML.html";
                var streamHTML = File.Open(HTML_path, FileMode.Create);
                var html = new DocumentHTML(streamHTML);
                html.Add(list);
                return "HTML Document create";
            }
            else if (arg["[csv]"] == "true")
            {
                var CSV_path = Directory.GetCurrentDirectory() + @"\RazvanCSV.csv";
                var streamCSV = File.Open(CSV_path, FileMode.Create);
                var csv = new DocumentCSV(streamCSV);
                csv.Add(list);
                return "CSV Document create";
            }
            foreach (var activity in list)
            {
                foreach (var element in activity.List().Values)
                    builder.Append($"{element} ");
                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }

        public string Value() => command;
    }
}