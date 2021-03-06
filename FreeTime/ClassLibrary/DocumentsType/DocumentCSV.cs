﻿namespace ClassLibrary
{
    using System.Collections.Generic;
    using System.IO;

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
            var sw = new StreamWriter(stream);
            
            foreach (var activity in activities)
            {
                if (stream.Length == 0)
                    AddHeader(activity);
                foreach (var element in activity.List().Values)
                {
                    sw.Write($" {element} ,");
                }
                sw.WriteLine();
            }
            sw.Flush();
        }

        private void AddHeader(Activity activity)
        {
            stream.Seek(0, SeekOrigin.End);
            var sw = new StreamWriter(stream);
            sw.WriteLine(string.Join(" ; ", activity.List().Keys));
            sw.Flush();
        }
    }
}
