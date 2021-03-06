﻿namespace Improvement
{
    using System;
    using ClassLibrary;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Xml;

    class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory() + @"\Razvan.txt";
            var pathXml = Directory.GetCurrentDirectory() + @"\Razvanss.xml";
            using (var stream = File.Open(pathXml, FileMode.OpenOrCreate))
            {
                try
                {
                    var command = new Commands(new ArgumentsConverter(args).Conversion(), stream);
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
                catch(RepositoryEmty e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
