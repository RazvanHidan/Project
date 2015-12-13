using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class TextRepository
    {
        private Stream stream;

        public TextRepository(Stream stream)
        {
            this.stream = stream;
        }

        public void Add(Activity activity)
        {
            stream.Seek(0, SeekOrigin.End);
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine(string.Join(" ",activity.Read()));
            sw.Flush();
        }

        public string List()
        {
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            return sr.ReadToEnd();
        }

        public string ListWeek()
        {
            string line;
            string result = "";
            stream.Position = 0;
            StreamReader file = new StreamReader(stream);
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
            return result;
        }
    }
}
