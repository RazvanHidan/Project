using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class RepositoryHTML 
    {
        private Stream stream;

        public RepositoryHTML(Stream stream)
        {
            this.stream = stream;
        }

        public void Add(IEnumerable<Activity> activities)
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                sw.WriteLine("<html><head><style>table, th, td {border: 1px solid black;}</style></head><body><table style = \"width:100%\">");
                foreach (var activity in activities)
                {
                    sw.WriteLine("<tr>");
                    foreach (var element in activity.List().Values)
                    {
                        sw.WriteLine($"<td> {element}  </td>");
                    }
                    sw.WriteLine("</tr>");
                }
                sw.WriteLine("</table></body></html>");
            }
        }

        public IEnumerable<Activity> List()
        {
            throw new NotImplementedException();
        }
    }
}
