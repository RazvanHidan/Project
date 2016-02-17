namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;

    public class Activity
    {
        private Dictionary<string, string> activity=new Dictionary<string, string>();

        public Activity(string message, string project = "")
        {
            var date = DateTime.UtcNow;
            var guid = Guid.NewGuid().ToString().Substring(0, 8);
            activity.Add($"id", guid);
            activity.Add($"project", project);
            activity.Add($"date", date.ToString());
            activity.Add($"enddate", date.ToString());
            activity.Add($"message", message);
            activity.Add($"duration", ActivityDuration(date, date));
        }

        public Activity(Dictionary<string, string> activity)
        {
            var date = DateTime.UtcNow.ToString();
            var guid = Guid.NewGuid().ToString().Substring(0, 8);
            this.activity.Add($"id", guid);
            this.activity.Add($"project", "");
            this.activity.Add($"date", date);
            this.activity.Add($"enddate", date);
            this.activity.Add($"message","");
            foreach (var key in activity.Keys)
                this.activity[key] = activity[key];
            ValidateDate();
        }

        public Activity(Activity oldActivity,Dictionary<string,string> newActivityElements)
        {
            activity = oldActivity.List();
            foreach (var key in newActivityElements.Keys)
                activity[key] = newActivityElements[key];
            ValidateDate();
        }

        public Dictionary<string, string> List()
        {
            return activity;
        }

        private void ValidateDate()
        {
            var temp = new Dictionary<string, string>();
            foreach (var key in activity.Keys)
                temp.Add(key, activity[key]);
            foreach (var key in temp.Keys)
            {
                if (key.Contains("date"))
                {
                    DateTime tempDate;
                    if (!DateTime.TryParse(activity[key], out tempDate))
                        throw new InvalidFormat($"{activity[key]} is not a valid format of date");
                    activity[key] = tempDate.ToString();
                }
            }
            activity["duration"] = ActivityDuration(DateTime.Parse(activity["date"]), DateTime.Parse(activity["enddate"]));
            if (activity["duration"].Contains("-")) 
                throw new InvalidFormat($"Activity start in {activity["date"]} and end in {activity["enddate"]}");
        }
        
        private static string ActivityDuration(DateTime start, DateTime end)
        {
            var duration = (end - start).TotalMinutes;
            return $"{Math.Truncate(duration / 60)}h {duration % 60}min";
        }
    }
}