using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary
{
    public class Event
    {
        public void Add(string userName, string action)
        {
            string path = @"C:\Users\NavzaR\Documents\Project\" + userName + ".txt";
            StreamWriter sw = (!File.Exists(path)) ? File.CreateText(path) : File.AppendText(path);
            sw.WriteLine(DateTime.Now + " " + action);
            sw.Close(); 
        }

        public string List(string userName)
        {
            string path = @"C:\Users\NavzaR\Documents\Project\" + userName + ".txt";
            string text="";
            if (File.Exists(path))
            {
                text = File.ReadAllText(path);
            }
            return text;
        }
    }
}
