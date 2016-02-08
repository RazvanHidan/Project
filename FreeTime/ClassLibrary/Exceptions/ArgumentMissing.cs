namespace ClassLibrary
{
    using System;
    public class ArgumentMissing : Exception
    {
        public ArgumentMissing()
        {
        }

        public ArgumentMissing(string message) : base(message)
        {
        }
    }
}