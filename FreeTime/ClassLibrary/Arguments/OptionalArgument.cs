namespace ClassLibrary
{
    using System.Collections.Generic;

    public class OptionalArgument : Argument
    {
        private readonly string name;
        private bool value;

        public OptionalArgument(string name)
        {
            this.name = name;
            this.value = false;
        }

        public bool IsOptioanArgument() => !value;

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