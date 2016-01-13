namespace ClassLibrary
{
    using System;

    public class InvalidArgument : Exception
    {
        public InvalidArgument(string mesage) : base(mesage)
        {
        }
    }
}