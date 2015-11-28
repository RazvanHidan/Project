using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary
{
    public class Activity
    {
        public void Add(string add, string path)
        {
            StreamWriter sw = (!File.Exists(path)) ? File.CreateText(path) : File.AppendText(path);
            sw.WriteLine(DateTime.Now + " " + add);
            sw.Close();
        }

        public string List(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else return "File not exist";
        }
    }
}