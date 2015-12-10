using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    public class Activity
    {
        public string date { get; set; }
        public string message { get; set; }
        public int count;

        public Activity(string message)
        {
            var datenow = DateTime.Now;
            date = datenow.ToString("G", CultureInfo.InvariantCulture);
            this.message = message;
            count = 1;
        }
    }
}
