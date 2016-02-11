﻿namespace ClassLibrary
{
    using System;
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

        public bool IsValid(string arg)
        {
            return name == $"[{arg}]";
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