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
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(action);
                }
            }

            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(action);
                }
            }
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
