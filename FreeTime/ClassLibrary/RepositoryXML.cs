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
            if (stream.Length==0)
                throw new RepositoryEmty("Data Base is Emty");
            xmldoc.Load(stream);
            var nodes = xmldoc.GetElementsByTagName("activity");
            if(nodes.Count==0)
                throw new RepositoryEmty("Data Base is Emty");
            foreach (XmlNode node in nodes)
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
                if (DateTime.TryParse(activity.List()["enddate"], out activityDate))
                {
                    if (activityDate.CompareTo(DateTime.UtcNow.AddDays(-7)) == 1)
                        yield return activity;
                }
            }
        }

        public void Change(string id,Dictionary<string,string> newActivityElements)
        { 
            var newActivity = new Activity(SelectOneActivity(id), newActivityElements);
            Delete(id);
            Add(newActivity);
        }

        public void Delete(string id)
        {
            ValidateID(id);
            stream.Position = 0;
            var xmldoc = new XmlDataDocument();
            xmldoc.Load(stream);
            xmldoc.DocumentElement.RemoveChild(xmldoc.SelectSingleNode($"//activity[id='{id}']"));
            stream.SetLength(0);
            xmldoc.Save(stream);
        }

        public List<Project> ListProject()
        {
            var projects = new List<Project>();
            foreach(var activity in List())
            {
                int index = projects.FindIndex(projName => projName.name == activity.List()["project"]);
                if (index != -1) 
                {
                    projects[index].AddActivity(activity);
                }
                else
                    projects.Add(new Project(activity));
            }
            return projects;
        }

        private string CalculateDuration(string duration, string v)
        {
            var hh = Int32.Parse(duration.Substring(duration.IndexOf('h') - 2, 2)) + Int32.Parse(v.Substring(v.IndexOf('h') - 2, 2));
            var min = Int32.Parse(duration.Substring(duration.IndexOf('m') - 2, 2)) + Int32.Parse(v.Substring(v.IndexOf('m') - 2, 2));
            hh +=(int) Math.Truncate((decimal)min / 60);
            min = min % 60;
            return $"{hh}h {min}min";
        }

        private Activity SelectOneActivity(string id)
        {
            ValidateID(id);
            foreach (var activity in List())
                if (activity.List()["id"] == id)
                    return activity;
            return default(Activity);
        }

        private void ValidateID(string id)
        {
            foreach (var activity in List())
                if (activity.List()["id"] == id)
                    return;
            throw new InvalidID($"ID \" {id} \" is not found");
        }
    }
}
