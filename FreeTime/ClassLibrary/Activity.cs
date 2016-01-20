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
        private string message;
        private string date;
        private string guid;

        public Activity()
        {
            message = null;
            date = null;
            guid = null;
        }

        public Activity(string message)
        {
            DateTostring(DateTime.UtcNow);
            this.message = message;
            guid = Guid.NewGuid().ToString();
            guid = guid.Substring(0,8);
        }

        public Dictionary<string,string> List()
        {
            var activity = new Dictionary<string, string>();
            activity.Add("id", guid);
            activity.Add("date", date);
            activity.Add("message", message);
            return activity;
        }

        public void ChangeDate(string newDate)
        {
            DateTostring(DateTime.Parse(newDate));
        }

        public void Change()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void ExtractFromString(string line,string separator)
        {
            var element = line.Split(new string[] { separator }, StringSplitOptions.None);
            guid = element[0];
            date = element[1];
            message = element[2];
        }

        private void DateTostring(DateTime date)
        {
            //this.date = date.ToString("G", CultureInfo.InvariantCulture);
            this.date = date.ToString();
        }
    }
}
