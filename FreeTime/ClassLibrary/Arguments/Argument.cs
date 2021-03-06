﻿namespace ClassLibrary
{
    using System.Collections.Generic;

    public interface Argument
    {
        void Parse(string arg);
        void SaveValue(IDictionary<string, string> store);
        bool IsValid(string arg);
    }
}