namespace ClassLibrary
{
    using System;
    public class InvalidID : Exception
    {
        public InvalidID(string message) : base(message)
        {
        }
    }
}