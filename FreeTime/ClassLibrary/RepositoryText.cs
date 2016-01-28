using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class RepositoryText
    {
        private Stream stream;

        public RepositoryText(Stream stream)
        {
            this.stream = stream;
        }

        public void Add(Activity activity)
        {
            stream.Seek(0, SeekOrigin.End);
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine(string.Join("][",activity.List().Values));
            sw.Flush();
        }

        public IEnumerable<Activity> List()
        {
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string[] contents = sr.ReadToEnd().Split(new string[] { "\r\n" }, 
                StringSplitOptions.RemoveEmptyEntries);
            foreach(string line in contents)
            {
                var activity = new Activity();
                activity.ExtractFromString(line,"][");
                yield return activity;
            }
        }

        public IEnumerable<Activity> ListWeek()
        {
            foreach (var activity in List())
            {
                var act = activity.List();
                DateTime activityDate;
                if (DateTime.TryParse(act["date"], out activityDate))
                {
                    if (activityDate.CompareTo(DateTime.UtcNow.AddDays(-7)) == 1)
                        yield return activity;
                }
            }
        }
        public void DeleteActivity(string id)
        {
            var content = new StringBuilder();
            foreach (var activity in List())
            {
                if (activity.List()["id"] == id)
                    continue;
                content.AppendLine(string.Join("][", activity.List().Values));
            }
            stream.SetLength(0);    
            AddToStream(content.ToString());
        }

        private void AddToStream(string v)
        {
            stream.SetLength(0);
            StreamWriter sw = new StreamWriter(stream);
            sw.Write(v);
            sw.Flush();
        }

        public void ChangeDate(string id, string newDate)
        {
            foreach (var activity in List())
            {
                if (activity.List()["id"] == id)
                {
                    Add(new Activity(newDate, activity.List()["message"]));
                    break;
                }
            }
            DeleteActivity(id);
        }

        public void ChangeMessage(string id, string newMessage)
        {
            foreach (var activity in List())
            {
                if (activity.List()["id"] == id)
                {
                    Add(new Activity(activity.List()["date"], newMessage));
                    break;
                }
            }
            DeleteActivity(id);
        }
    }
}
