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
            sw.WriteLine(string.Join(" ",activity.List()));
            sw.Flush();
        }

        public IEnumerable<Activity> List()
        {
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string[] contents = sr.ReadToEnd().Split(new string[] { "\r\n" }, 
                StringSplitOptions.RemoveEmptyEntries);
            foreach(var line in contents)
            {
                var activity = new Activity();
                activity.ExtractFromString(line);
                yield return activity;
            }
        }

        public IEnumerable<Activity> ListWeek()
        {
            foreach(var activity in List())
            {
                foreach(var date in activity.List())
                {
                    DateTime activityDate;
                    if (DateTime.TryParse(date,out activityDate))
                    {
                        if(activityDate.CompareTo(DateTime.Now.AddDays(-7)) == 1)
                        {
                            yield return activity;
                        }
                    }
                }
            }
        }

        public void ChangeDate(int activityNumber, string newDate)
        {
            int aux = 0;
            foreach (var activity in List())
            {
                aux++;
                if (aux == activityNumber)
                {
                    activity.ChangeDate(newDate);
                }
            }
        }
    }
}
