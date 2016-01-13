using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class CommandArgument : Argument
    {
        private readonly string command;
        private bool value;

        public CommandArgument(string command)
        {
            this.value = false;
            this.command = command;
        }

        public void Parse(string arg)
        {
            if (command != arg)
                throw new InvalidArgument($"{arg} is invalid");
            value = true;
        }

        public void SaveValue(IDictionary<string, string> store)
        {
            store.Add(command, value.ToString().ToLower());
        }
    }
}
