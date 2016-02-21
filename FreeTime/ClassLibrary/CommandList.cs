namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class CommandList : Command
    {
        private string command;

        public CommandList()
        {
            command = "list [week] [csv] [html] [projects]";
        }

        public string Execute(Arguments arg, Stream stream)
        {
            if (stream.Length != 0)
            {
                var builder = new StringBuilder();
                var list = new RepositoryXML(stream).List();

                if (arg["[week]"] == "true")
                    list = new RepositoryXML(stream).ListWeek();
                else if (arg["[html]"] == "true")
                {
                    var pathHTML = Directory.GetCurrentDirectory() + @"\RazvanHTML.html";
                    var streamHTML = File.Open(pathHTML, FileMode.Create);
                    var html = new DocumentHTML(streamHTML);
                    html.Add(list);
                    return "HTML Document create";
                }
                else if (arg["[csv]"] == "true")
                {
                    var pathCSV = Directory.GetCurrentDirectory() + @"\RazvanCSV.csv";
                    var streamCSV = File.Open(pathCSV, FileMode.Create);
                    var csv = new DocumentCSV(streamCSV);
                    csv.Add(list);
                    return "CSV Document create";
                }
                else if (arg["[projects]"] == "true")
                {
                    var projects = new RepositoryXML(stream).ListProject();
                    foreach (var project in projects)
                    {
                        if (builder.Length == 0)
                            SpacingProj(builder, project, true);
                        SpacingProj(builder, project);
                    }
                    return builder.ToString();
                }

                foreach (var activity in list)
                {
                    if (builder.Length == 0)
                        Spacing(builder, activity, true);
                    Spacing(builder, activity);
                }
                return builder.ToString();
            }
            else
                throw new RepositoryEmty("Data Base is emty");
        }
        
        public string Info()
        {
            var info = new StringBuilder();
            var newLine = Environment.NewLine;
            info.Append(command + newLine);
            info.Append($"       Displays all/last week activities or export HTML or CSV format Documents.{newLine}{newLine}");
            return info.ToString();
        }

        public string Value() => command;

        private static void Spacing(StringBuilder builder, Activity activity, bool addHeader = false)
        {
            var numberOfSpace = new Dictionary<string, int>
            {
                {$"id", 12},
                {$"project", 14},
                {$"date", 22},
                {$"enddate", 22},
                {$"duration",12 },
                {$"message", 100}
            };
            foreach (var element in activity.List())
            {
                var appendString = addHeader ? element.Key.ToUpper() : element.Value;
                if (numberOfSpace[element.Key] - appendString.Length > 2)
                {
                    Append(builder, appendString, numberOfSpace[element.Key] - appendString.Length);
                }
                else
                {
                    Append(builder, appendString.Substring(0, numberOfSpace[element.Key] - 6) + "...", 3);
                }
            }
            builder.Append(Environment.NewLine);
        }

        private static void SpacingProj(StringBuilder builder, Project project, bool addHeader = false)
        {
            var numberOfSpace = new Dictionary<string, int>
            {
                {$"name", 25},
                {$"count", 12},
                {$"duration", 25},
            };
            foreach (var element in project.List())
            {
                var appendString = addHeader ? element.Key.ToUpper() : element.Value;
                if (numberOfSpace[element.Key] - appendString.Length > 2)
                {
                    Append(builder, appendString, numberOfSpace[element.Key] - appendString.Length);
                }
                else
                {
                    Append(builder, appendString.Substring(0, numberOfSpace[element.Key] - 6) + "...", 3);
                }
            }
            builder.Append(Environment.NewLine);
        }

        private static void Append(StringBuilder builder, string appendString, int numberOfSpace)
        {
            builder.Append(appendString);
            builder.Append(' ', numberOfSpace);
        }
    }
}