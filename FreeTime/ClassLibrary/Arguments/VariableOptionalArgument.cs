namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;

    internal class VariableOptionalArgument : Argument
    {
        private readonly string name;
        private string value;

        public VariableOptionalArgument(string name)
        {
            this.name = name;
        }

        public bool IsValid(string arg)
        {
            return value!="false";
        }

        public void Parse(string arg)
        {
            if (arg == string.Empty || !(arg.StartsWith(name.Substring(1, 3))))
                value = $"false";
            else
            {
                if (arg.IndexOf(":") != -1 && name.Substring(1,name.IndexOf(':'))==arg.Substring(0,arg.IndexOf(':')+1))
                    value = arg.Substring(arg.IndexOf(':') + 1);
                else
                    throw new InvalidArgument($"{arg}");
            }   
        }

        public void SaveValue(IDictionary<string, string> store)
        {
            store.Add(name, value);
        }
    }
}