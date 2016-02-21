namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;

    public class Project
    {
        public string name;
        public int count;
        public TimeSpan duration;
        public List<Activity> activities = new List<Activity>();
        
        public Project(Activity activity)
        {
            var act = activity.List();
            name = act["project"];
            count = 1;
            duration = DateTime.Parse(act["enddate"]) - DateTime.Parse(act["date"]);
            activities.Add(activity);
        }

        public Dictionary<string,string> List()
        {
            var proj = new Dictionary<string, string>();
            proj.Add("name", name);
            proj.Add("count", count.ToString());
            proj.Add("duration", ProjectDuration());
            return proj;
        }

        public void AddActivity(Activity activity)
        {
            count++;
            duration+=(DateTime.Parse(activity.List()["enddate"]) - DateTime.Parse(activity.List()["date"]));
            activities.Add(activity);
        }

        private string ProjectDuration()
        {
            if (duration.Days != 0)
                return $"{duration.Days}d {duration.ToString("hh")}h {duration.ToString("mm")}m";
            return $"{duration.ToString("hh")}h {duration.ToString("mm")}m";
        }
    }
}