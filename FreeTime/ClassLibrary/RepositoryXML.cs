namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class RepositoryXML
    {
        private Stream stream;

        public RepositoryXML(Stream stream)
        {
            this.stream = stream;
        }

        public void Add(Activity activity)
        {
            if (stream.Length != 0)
                stream.Seek(-13, SeekOrigin.End);
            else
                stream.Seek(0, SeekOrigin.End);
            var sw = new StreamWriter(stream);
            if (stream.Length == 0)
            {
                sw.WriteLine("<repository>");
            }

            sw.WriteLine("<activity>");
            foreach (var element in activity.List())
            {
                sw.WriteLine($"<{element.Key}>{element.Value}</{element.Key}>");
            }
            sw.WriteLine("</activity>");
            sw.Write("</repository>");
            sw.Flush();
        }

        public IEnumerable<Activity> List()
        {
            stream.Position = 0;
            XmlDataDocument xmldoc = new XmlDataDocument();
            xmldoc.Load(stream);

            foreach(XmlNode node in xmldoc.GetElementsByTagName("activity"))
            {
                var dictonary = new Dictionary<string, string>();
                foreach (XmlElement childNode in node.ChildNodes)
                    dictonary.Add(childNode.Name, childNode.InnerText);
                yield return new Activity(dictonary);
            }
        }

        public IEnumerable<Activity> ListWeek()
        {
            foreach (var activity in List())
            {
                DateTime activityDate;
                if (DateTime.TryParse(activity.List()["date"], out activityDate))
                {
                    if (activityDate.CompareTo(DateTime.UtcNow.AddDays(-7)) == 1)
                        yield return activity;
                }
            }
        }

        public void Change(string id,string elementName,string elementNewValue)
        {
            DateTime tempDadte;
            bool madeChange= false;
            if (elementName.Contains("date"))
            {
                if (!DateTime.TryParse(elementNewValue, out tempDadte))
                    throw new InvalidFormat($"{elementNewValue} is not a valid format of date");
                else
                    elementNewValue = tempDadte.ToString();
            }

            stream.Position = 0;
            var xmldoc = new XmlDataDocument();
            xmldoc.Load(stream);
            XmlNodeList eleMENTES = xmldoc.SelectNodes("activity");
            foreach (XmlNode node in xmldoc.SelectNodes("//activity"))
            {
                if (node.SelectSingleNode("id").InnerText == id)
                {
                    node.SelectSingleNode($"{elementName}").InnerText = elementNewValue;
                    stream.SetLength(0);
                    xmldoc.Save(stream);
                    madeChange = true;
                }
            }
            if (!madeChange)
                throw new InvalidID($"{id} is not found");
        }
    }
}
