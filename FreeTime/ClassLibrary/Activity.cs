namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;

    public class Activity
    {
        /*
        private string guid;
        private string project;
        private string date;
        private string message;
        */
        private Dictionary<string, string> activity=new Dictionary<string, string>();

        public Activity(string message, string project = "n/a")
        {
            var date = DateTime.UtcNow.ToString();
            //this.message = message;
            var guid = Guid.NewGuid().ToString().Substring(0, 8);
            //this.project = project;
            activity.Add($"id", guid);
            activity.Add($"project", project);
            activity.Add($"date", date);
            activity.Add($"message", message);
        }

        
        /*public Activity(string id, string date, string message, string project)
         {
            /*
            guid = id;
            this.project = project;
            this.date = date;
            this.message = message;

            activity["id"] = id;
            activity["project"] = project;
            activity["date"] = date;
            activity["message"] = message;
        }*/

        public Activity(Dictionary<string, string> activity)
        {
            this.activity = activity;
        }

        public Dictionary<string, string> List()
        {
            return activity;
        }
    }
}