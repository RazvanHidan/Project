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
        public void Add(string add,string path)
        {
            using (StreamWriter sw = (!File.Exists(path)) ? File.CreateText(path) : File.AppendText(path))
            {
                DateTime date = DateTime.Now;
                string formatDate = "MM/dd/yyyy HH:mm:ss ";
                sw.WriteLine(date.ToString(formatDate) + add);
            }
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

            if (File.Exists(path))
            {
                using (StreamReader file = new StreamReader(path))
                {
                    bool list = false;
                    DateTime dateValue;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] separete = line.Split(' ');
                        if ((separete.Length > 1) && (DateTime.TryParse(separete[0] + " " + separete[1], out dateValue)))
                        {
                            if (dateValue.CompareTo(DateTime.Now.AddDays(-7)) == 1)
                            {
                                list = true;
                            }
                        }
                        else
                        {
                            list = true;
                        }

                        if (list)
                        {
                            result += line + Environment.NewLine;
                        }
                        list = false;
                    }
                }
            }
            DeleteLine(7, path);
            return result;
        }

        private void DeleteLine(int line, string path)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(path))
            {
                int count = 0;
                while (!sr.EndOfStream)
                {
                    count++;
                    if (count != line) 
                    {
                        using (StringWriter sw = new StringWriter(sb))
                        {
                            sw.WriteLine(sr.ReadLine());
                        }
                    }
                    else
                    {
                        sr.ReadLine();
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(path))
            {
               sw.Write(sb.ToString());
            }
        }

        public void ChangeMessage(string message,string path)
        {

        }

        public void ChangeDate(string date, string path)
        {

        }

        public void Change(string message,string date,string path)
        {

        }
    }
}