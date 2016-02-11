namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;

    public class ArgumentValue : Argument
    {
        private readonly string name;
        private string value = "false";

        public ArgumentValue(string name)
        {
            this.name = name;
        }

        public bool IsValid(string arg)
        {
            return false;
        }

        public void Parse(string arg)
        {
            if (arg == string.Empty)
                throw new ArgumentMissing($"{name} is missing");
            value = arg;
        }

        public void SaveValue(IDictionary<string, string> store)
        {
            store.Add(name, value);
        }
    }
}