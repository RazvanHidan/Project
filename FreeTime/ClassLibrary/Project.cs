namespace ClassLibrary
{
    using System.Collections.Generic;

    public class Project
    {
        public string name;
        public int count { get; set; }
        public string duration { get; set; }

        public Project(Activity activity)
        {
            this.name = activity.List()["project"];
            this.duration = activity.List()["duration"];
            count = 1;
        }

        public Dictionary<string,string> List()
        {
            var proj = new Dictionary<string, string>();
            proj.Add("name", name);
            proj.Add("count", count.ToString());
            proj.Add("duration", duration);
            return proj;
        }
    }
}