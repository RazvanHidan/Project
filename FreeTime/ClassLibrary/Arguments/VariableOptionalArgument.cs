namespace ClassLibrary
{
    using System.Collections.Generic;

    internal class VariableOptionalArgument : Argument
    {
        private readonly string name;
        private string value;

        public VariableOptionalArgument(string name)
        {
            this.name = name;
        }

        public bool IsOptioanArgument() => value == "" ? true : false;

        public void Parse(string arg)
        {
           if (arg == string.Empty || !(arg.StartsWith(name.Substring(1, 3))))
                value = $"";
            else
            {
                value = arg.Substring(arg.IndexOf(':') + 1);
            }   
        }

        public void SaveValue(IDictionary<string, string> store)
        {
            store.Add(name, value);
        }
    }
}