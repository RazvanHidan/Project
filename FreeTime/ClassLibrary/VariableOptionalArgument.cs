﻿using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    internal class VariableOptionalArgument : Argument
    {
        private readonly string name;
        private string value;

        public VariableOptionalArgument(string name)
        {
            this.name = name;
        }

        public bool IsOptioanArgument()
        {
            if(value=="false")
                return true;
            return false;
        }

        public void Parse(string arg)
        {
           if (arg == string.Empty || !(arg.StartsWith(name.Substring(1, 3))))
                value = "false";
            else
            {
                int indexSpace = arg.IndexOf(' ')+1;
                value = arg.Substring(indexSpace, arg.Length-indexSpace);
            }
                
        }

        public void SaveValue(IDictionary<string, string> store)
        {
            store.Add(name, value);
        }
    }
}