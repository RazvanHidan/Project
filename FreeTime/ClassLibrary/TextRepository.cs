using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class TextRepository
    {
        private Stream stream;

        public TextRepository(Stream stream)
        {
            this.stream = stream;
        }

        public void Add(Activity activity)
        {
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine(string.Join(" ",activity.Read()));
            sw.Flush();
        }

        public string List()
        {
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            return sr.ReadToEnd();
        }
    }
}
