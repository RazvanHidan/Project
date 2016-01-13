using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class ArgumentValue : Argument
    {
        private readonly string name;
        private string value;

        public ArgumentValue(string name)
        {
            this.name = name;
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
