namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;

    public class Activity
    {
        private string guid;
        private string project;
        private string date;
        private string message;

        public Activity(string message, string project = "n/a")
        {
            date = DateTime.UtcNow.ToString();
            this.message = message;
            guid = Guid.NewGuid().ToString().Substring(0, 8);
            this.project = project;
        }

        public Activity(string id, string date, string message, string project)
        {
            guid = id;
            this.project = project;
            this.date = date;
            this.message = message;
        }

        public Dictionary<string, string> List()
        {
            var activity = new Dictionary<string, string>();
            activity.Add($"id", guid);
            activity.Add($"project", project);
            activity.Add($"date", date);
            activity.Add($"message", message);
            return activity;
        }

        
    }
}