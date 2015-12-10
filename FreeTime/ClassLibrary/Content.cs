using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary
{
    public class Content

    {
        private string path;

        public Content(string path)
        {
            this.path = path;
        }

        public void Add(string message)
        {
            using (StreamWriter sw = (!File.Exists(path)) ? File.CreateText(path) : File.AppendText(path))
            {
                var activity = new Activity(message);
                sw.WriteLine("{0} {1}",activity.date,activity.message);
            }
        }

        public string List()
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else return "File not exist";
        }

        public string ListWeek()
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
            return result;
        }

        private void DeleteLine(int line)
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

        public void ChangeMessage(int line, string message)
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
                        using (StringWriter sw = new StringWriter(sb))
                        {
                            string[] separete = sr.ReadLine().Split(' ');
                            Array.Resize(ref separete, 3);
                            separete[2] = message;
                            sw.WriteLine(string.Join(" ", separete));
                        }
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(sb.ToString());
            }
        }

        public void ChangeDate(int line, string date)
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
                        using (StringWriter sw = new StringWriter(sb))
                        {
                            string[] separete = sr.ReadLine().Split(' ');
                            separete[0] = date.Split(' ')[0];
                            separete[1] = date.Split(' ')[1];
                            sw.WriteLine(string.Join(" ",separete));
                        }
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(sb.ToString());
            }
        }

        public void Change(int line,string message,string date)
        {
            ChangeMessage(line, message);
            ChangeDate(line, date);
        }
    }
}