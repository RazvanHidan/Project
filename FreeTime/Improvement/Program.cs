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
            using (var stream = File.Open(path, FileMode.OpenOrCreate))
            {
                try
                {
                    var command = new Commands(args, stream);
                    Console.WriteLine(command.Execute());
                }
                catch (InvalidArgument e)
                {
                    Console.WriteLine($"Argument \"{ e.Message}\" is not valid");
                    Console.WriteLine(new Help().help);
                }
                catch (ArgumentMissing e)
                {
                    Console.WriteLine($"Argument {e.Message}");
                    Console.WriteLine(new Help().help);
                }
            }
        }
    }
}
