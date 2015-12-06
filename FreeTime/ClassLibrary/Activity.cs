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
            DateTime date = DateTime.Now;
            string formatDate = "dd/MM/yyyy HH:mm:ss ";
            sw.WriteLine(date.ToString(formatDate)+ add);
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

        public string ListWeek(string path)
        {
            string line;
            string result = "";
            DateTime date;

            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    string[] separete = line.Split(' ');
                    date = Convert.ToDateTime(separete[0] + " " + separete[1] + " " + separete[2]);
                    if (date.CompareTo(DateTime.Now.AddDays(-7))==1)
                    {
                        result += line + Environment.NewLine;
                    }
                }
                file.Close();
            }
            return result;
        } 
    }
}