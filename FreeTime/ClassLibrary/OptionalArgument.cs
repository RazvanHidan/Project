using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class OptionalArgument : Argument
    {
        private readonly string name;
        private bool value;

        public OptionalArgument(string name)
        {
            this.name = name;
            this.value = false;
        }

        public void Parse(string arg)
        {
            value = name == $"[{arg}]";
        }

        public void SaveValue(IDictionary<string, string> store)
        {
            store.Add(name, value.ToString().ToLower());
        }
    }

}
