﻿using System;
using System.Collections.Generic;
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
            var builder = new StringBuilder();
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
                    Spacing(builder,activity,true);
                Spacing(builder, activity);
            }
            return builder.ToString();
        }
        
        public string Info()
        {
            var info = new StringBuilder();
            var newLine = Environment.NewLine;
            info.Append(command+newLine);
            info.Append($"       Displays all/last week activities or export HTML or CSV format Documents.{newLine}{newLine}");
            return info.ToString();
        }

        public string Value() => command;

        private static void Spacing(StringBuilder builder, Activity activity,bool AddHeader=false)
        {
            var numberOfSpace = new Dictionary<string, int>
            {
                {"id",12 },
                {"project",14 },
                {"date",22 },
                {"message",100}
            };
            foreach (var element in activity.List())
            {
                var appendString = AddHeader ? element.Key.ToUpper() : element.Value;
                if (numberOfSpace[element.Key] - appendString.Length > 2)
                {
                    builder.Append(appendString);
                    builder.Append(' ',numberOfSpace[element.Key] - appendString.Length);
                }
                else
                {
                    builder.Append(appendString.Substring(0, numberOfSpace[element.Key] - 6)+"...");
                    builder.Append(' ', 3);
                }
            }
            builder.Append(Environment.NewLine);
        }
    }
}