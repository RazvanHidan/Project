using System;
using ClassLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Improvement
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory() + @"\Razvan.txt";
            var stream = File.Open(path, FileMode.OpenOrCreate);
            var text = new RepositoryText(stream);
            var parse = new ArgumentsParser(args, text);
            //try
            //{
            //    if (args[0] == "add")
            //    {
            //        text.Add(new Activity(args[1]));
            //    }
            //    else if (args[0] == "list")
            //    {
            //        IEnumerable<Activity> list;
            //        if (args.Length == 2 && args[1] == "week")
            //        {
            //            list = text.ListWeek();
            //        }
            //        else
            //        {
            //            list = text.List();
            //        }
            //        var CSV_path = Directory.GetCurrentDirectory() + @"\RazvanCSV.CSV";
            //        var streamCSV = File.Open(CSV_path, FileMode.Create);
            //        var HTML_path = Directory.GetCurrentDirectory() + @"\RazvanHTML.html";
            //        var streamHTML = File.Open(HTML_path, FileMode.Create);


            //        var html = new RepositoryHTML(streamHTML);
            //        html.Add(list);
            //        foreach (var activity in list)
            //        {
            //            var csv = new RepositoryCSV(streamCSV);
            //            csv.Add(activity);
            //            foreach (var element in activity.List().Values)
            //            {
            //                Console.Write($"{element} ");
            //            }
            //            Console.WriteLine();
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("{0} ", e);
            //}
        }
    }
}
