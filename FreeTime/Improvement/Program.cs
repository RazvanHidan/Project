using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Improvement
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassLibrary.User user = new ClassLibrary.User();
            Console.WriteLine("Enter User Name:");
            user.Name = Console.ReadLine();

            ClassLibrary.Event action = new ClassLibrary.Event();
            Console.WriteLine("Add your message:");
            string message = Console.ReadLine();
            action.Add(user.Name, message);

            string text=action.List(user.Name);
            Console.WriteLine("Message from {0} : {1}", user.Name, text);
            Console.ReadKey();
        }
    }
}
