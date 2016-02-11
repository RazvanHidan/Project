namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;

    internal class FalseArgument : Argument
    {
        private string name;
        private string value;

        public FalseArgument(string name)
        {
            this.name = name;
        }

        public bool IsValid(string arg)
        {
            return false;
        }

        public void Parse(string arg)
        {
            value = "false";
        }

        public void SaveValue(IDictionary<string, string> store)
        {
            store.Add(name,value);
        }
    }
}