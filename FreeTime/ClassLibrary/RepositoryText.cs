namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

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
            var sw = new StreamWriter(stream);
            sw.WriteLine(string.Join("][", activity.List().Values));
            sw.Flush();
        }

        public IEnumerable<Activity> List()
        {
            stream.Position = 0;
            var sr = new StreamReader(stream);
            var contents = sr.ReadToEnd().Split(new string[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in contents)
            {
                yield return ExtractFromString(line, "]["); ;
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
                if (activity.List()[$"id"] == id)
                    continue;
                content.AppendLine(string.Join("][", activity.List().Values));
            }
            stream.SetLength(0);
            AddToStream(content.ToString());
        }

        public void ChangeProject(string id, string newProject)
        {
            foreach (var activity in List())
            {
                if (activity.List()[$"id"] == id)
                {
                    var message = activity.List()["message"];
                    var date = activity.List()["date"];
                    DeleteActivity(id);
                    Add(new Activity(id, date, message, newProject));
                    break;
                }
            }
        }

        public void ChangeDate(string id, string newDate)
        {
            foreach (var activity in List())
            {
                if (activity.List()[$"id"] == id)
                {
                    var message = activity.List()["message"];
                    var project = activity.List()["project"];
                    DeleteActivity(id);
                    Add(new Activity(id, newDate, message, project));
                    break;
                }
            }
        }

        public void ChangeMessage(string id, string newMessage)
        {
            foreach (var activity in List())
            {
                if (activity.List()[$"id"] == id)
                {
                    var date = activity.List()["date"];
                    var project = activity.List()["project"];
                    var temp = activity;
                    DeleteActivity(id);
                    Add(new Activity(id, date, newMessage, project));
                    break;
                }
            }
        }

        private void AddToStream(string v)
        {
            stream.SetLength(0);
            var sw = new StreamWriter(stream);
            sw.Write(v);
            sw.Flush();
        }

        private Activity ExtractFromString(string line, string separator)
        {
            var element = line.Split(new string[] { separator }, StringSplitOptions.None);
            return new Activity(element[0], element[2], element[3], element[1]);
        }
    }
}