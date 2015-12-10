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
            if (args.Contains("add") || args.Contains("list")||args.Contains("change"))
            {
                ClassLibrary.Content activity = new ClassLibrary.Content(path);

                if (args.Length > 1)
                {
                    if (args[0] == "add")
                    {
                        activity.Add(args[1]);
                    }
                    else if (args[0] == "list" && args[1] == "week")
                    {
                        Console.WriteLine(activity.ListWeek());
                    }
                    else if (args[0] == "change")
                    {
                        if ((args.Contains("-message") || args.Contains("-m")) 
                            && (args.Contains("-date") || args.Contains("-d")))
                            activity.Change(int.Parse(args[1]),args[3],args[5]);
                        else if (args.Contains("-message") || args.Contains("-m"))
                            activity.ChangeMessage(int.Parse(args[1]), args[3]);
                        else if (args.Contains("-date") || args.Contains("-d"))
                            activity.ChangeDate(int.Parse(args[1]), args[3]);
                    }
                }

                if (args.Length == 1 && args[0] == "list")
                {
                    Console.WriteLine(activity.List());
                }
            }
            else
                Console.WriteLine("Use add, list,list week,change -m/-message or -d/-date");
        }
    }
}
