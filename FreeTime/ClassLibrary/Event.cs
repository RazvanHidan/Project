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
        private string[] arg;
        public int count;

        public Event()
        {
            arg = null;
            count = 0;
        }

        public void Add(string userName, string action)
        {
            string path = Directory.GetCurrentDirectory() + "\\" + userName + ".txt";
            StreamWriter sw = (!File.Exists(path)) ? File.CreateText(path) : File.AppendText(path);
            sw.WriteLine(DateTime.Now + " " + action);
            sw.Close();
        }

        public string List(string userName)
        {
            string path = Directory.GetCurrentDirectory() + "\\" + userName + ".txt";
            string text = "";
            if (File.Exists(path))
            {
                text = File.ReadAllText(path);
            }
            return text;
        }

        public void AddMessage(string message)
        {
            count++;
            Array.Resize(ref arg,count);
            arg[count-1] = message;
        }
    }
}