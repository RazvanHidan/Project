namespace ClassLibrary
{
    using System.IO;

    public interface Command
    {
        string Execute(Arguments arg, Stream stream);
        string Value();
        string Info();
    }
}
