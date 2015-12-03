using System;
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
            string path = Directory.GetCurrentDirectory() + "\\" + "Razvan.txt";
            if (args.Contains("add") || args.Contains("list"))
            {
                ClassLibrary.Activity activity = new ClassLibrary.Activity();

                if (args.Length > 1)
                {
                    if (args[0] == "add")
                    {
                        activity.Add(args[1], path);
                    }
                    else if (args[0] == "list" && args[1] == "week")
                    {
                        Console.WriteLine(activity.ListWeek(path));
                    }
                }

                if (args.Length == 1 && args[0] == "list")
                {
                    Console.WriteLine(activity.List(path));
                }
            }
            else
                Console.WriteLine("Use add, list or list week");
            Console.ReadKey();
        }
    }
}
