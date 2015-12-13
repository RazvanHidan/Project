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
            var activity = new Activity();
            var text = new TextRepository(stream);
            if (args[0] == "add")
            {
                activity.Create(args[1]);
                text.Add(activity);
            }
            else if (args[0] == "list")
            {
                if (args[1] == "week")
                {
                    Console.WriteLine(text.ListWeek());
                }
                else
                    Console.WriteLine(text.List());
            }
        }
    }
}
