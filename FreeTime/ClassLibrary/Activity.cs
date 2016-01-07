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

        public Activity()
        {
            message = null;
            date = null;
        }

        public Activity(string message)
        {
            DateTostring(DateTime.Now);
            this.message = message;
        }

        public IEnumerable<string> List()
        {
            return StringToArray();
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

        public void ExtractFromString(string strin)
        {
            date = strin.Substring(0, 19);
            message = strin.Substring(20);
        }

        private void DateTostring(DateTime date)
        {
            this.date = date.ToString("G", CultureInfo.InvariantCulture);
        }

        private string[] StringToArray()
        {
            return new string[2] { date, message };
        }
    }
}
