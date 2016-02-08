namespace ClassLibrary
{
    using System.Collections.Generic;
    using System.IO;

    public class DocumentHTML
    {
        private Stream stream;

        public DocumentHTML(Stream stream)
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
    }
}
