using System;
using System.IO;
using System.Text;

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
                if (builder.Length == 0)
                    AddHeader(builder,activity);
                foreach (var element in activity.List().Values)
                {
                    builder.Append($"{element} ");
                    builder.Append(' ', 3);
                }
                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }
        
        public string Info()
        {
            var info = new StringBuilder();
            info.Append(command);
            info.Append(' ', 55 - command.Length);
            info.Append("Displays all/last week activities or export HTML or CSV format Documents");
            info.Append(Environment.NewLine);
            return info.ToString();
        }

        public string Value() => command;

        private static void AddHeader(StringBuilder builder,Activity activity)
        {
            foreach (var key in activity.List().Keys)
            {
                builder.Append(key.ToUpper());
                builder.Append(' ', (activity.List()[key].Length - key.Length) + 4);
            }
                
            builder.Append(Environment.NewLine);
        }
    }
}