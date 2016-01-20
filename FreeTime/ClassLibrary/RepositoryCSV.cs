using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class RepositoryCSV 
    {
        private Stream stream;

        public RepositoryCSV(Stream stream)
        {
            this.stream = stream;
        }

        public void Add( Activity activity)
        {
            stream.Seek(0, SeekOrigin.End);
            StreamWriter sw = new StreamWriter(stream);
            if(stream.Length==0)
                sw.WriteLine(string.Join(" ; ", activity.List().Keys));
            sw.WriteLine(string.Join(" ; ", activity.List().Values));
            sw.Flush();
        }

        public IEnumerable<Activity> List()
        {
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string[] contents = sr.ReadToEnd().Split(new string[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in contents)
            {
                var activity = new Activity();
                activity.ExtractFromString(line," ; ");
                yield return activity;
            }
        }
    }
}
