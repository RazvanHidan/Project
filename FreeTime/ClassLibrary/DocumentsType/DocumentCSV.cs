using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DocumentCSV 
    {
        private Stream stream;

        public DocumentCSV(Stream stream)
        {
            this.stream = stream;
        }

        public void Add(IEnumerable< Activity> activities)
        {
            stream.Seek(0, SeekOrigin.End);
            StreamWriter sw = new StreamWriter(stream);
            if (stream.Length == 0)
                AddHeader();
            foreach (var activity in activities)
            {
                foreach (var element in activity.List().Values)
                {
                    sw.Write($" {element} ,");
                }
                sw.WriteLine();
            }
            sw.Flush();
        }

        private void AddHeader()
        {
            var activity = new Activity();
            stream.Seek(0, SeekOrigin.End);
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine(string.Join(" ; ", activity.List().Keys));
            sw.Flush();
        }
    }
}
