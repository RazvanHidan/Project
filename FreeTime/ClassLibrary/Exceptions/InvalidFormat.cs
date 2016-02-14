namespace ClassLibrary
{
    using System;

    public class InvalidFormat : Exception
    {
        public InvalidFormat(string mesage) : base(mesage)
        {
        }
    }
}