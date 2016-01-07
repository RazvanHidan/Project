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
            var path = Directory.GetCurrentDirectory() + "\\" + "Razvan.txt";
            var stream = File.Open(path, FileMode.OpenOrCreate);
            var text = new TextRepository(stream);
            if (args[0] == "add")
            {
                text.Add(new Activity(args[1]));
            }
            else if (args[0] == "list")
            {
                IEnumerable<Activity> list;
                if (args.Length == 2 && args[1] == "week")
                {
                    list = text.ListWeek();
                }
                else
                {
                    list = text.List();
                }
                foreach (var activity in list)
                {
                    foreach (var element in activity.List())
                    {
                        Console.Write(" {0}", element);
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }
    }
}
