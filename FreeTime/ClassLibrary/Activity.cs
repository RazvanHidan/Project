using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Activity
    {
        private string guid;
        private string project;
        private string date;
        private string message;

        public Activity()
        {
            message = null;
            date = null;
            guid = null;
        }

        public Activity(string message,string project="n/a")
        {
            DateTostring(DateTime.UtcNow);
            this.message = message;
            guid = Guid.NewGuid().ToString().Substring(0, 8);
            this.project = project;
        }

        public Activity(string id,string date,string message,string project)
        {
            guid = id;
            this.project = project;
            this.date= date;
            this.message = message;
        }

        public Dictionary<string,string> List()
        {
            var activity = new Dictionary<string, string>();
            activity.Add("id", guid);
            activity.Add("project", project);
            activity.Add("date", date);
            activity.Add("message", message);
            return activity;
        }

        public void ChangeDate(string newDate)
        {
            DateTostring(DateTime.Parse(newDate));
        }

        public void ChangeMessage(string newMessage)
        {
            message=newMessage;
        }

        public void ExtractFromString(string line,string separator)
        {
            var element = line.Split(new string[] { separator }, StringSplitOptions.None);
            guid = element[0];
            project = element[1];
            date = element[2];
            message = element[3];
        }

        private void DateTostring(DateTime date)
        {
            this.date = date.ToString();
        }
    }
}
