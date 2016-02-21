namespace ClassLibrary
{
    using System;
    using System.IO;
    using System.Text;

    public class CommandClear : Command
    {
        private string command;

        public CommandClear()
        {
            command = "clear";
        }

        public string Execute(Arguments arg, Stream stream)
        {
            var repository = new RepositoryXML(stream);
            var activities = repository.List();
            foreach(var activity in activities)
            {
                repository.Delete(activity.List()["id"]);
            }
            return "Clear all activities";
        }

        public string Info()
        {
            var info = new StringBuilder();
            var newLine = Environment.NewLine;
            info.Append(command + newLine);
            info.Append($"Clear all activities{newLine}{newLine}");
            return info.ToString();
        }

        public string Value() => command;
    }
}
