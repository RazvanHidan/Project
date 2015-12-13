using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Activity:IElement<string>
    {
        private string message;
        private string date;

        public void Create(string message)
        {
            DateTostring(DateTime.Now);
            this.message = message;
        }

        private void DateTostring(DateTime date)
        {
            this.date = date.ToString("G", CultureInfo.InvariantCulture);
        }

        public IEnumerable<string> Read()
        {
            return StringToArray();
        }

        private string[] StringToArray()
        {
            return JoinDateAndMessage().Split(' ');
        }

        private string JoinDateAndMessage()
        {
            return string.Join(" ", date, message);
        }

        public void Change()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
