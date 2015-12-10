using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary
{
    public class TextFile
    {
        private Stream path;

        public TextFile(Stream path)
        {
            this.path = path;
        }

        public void Add(string message)
        {
            Activity add = new Activity(message);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("{0} {1}", add.date, add.message);
            }   
        }
    }
}
