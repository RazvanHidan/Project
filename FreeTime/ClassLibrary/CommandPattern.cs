namespace ClassLibrary
{
    public class CommandPattern
    {
        private string[] args;

        public CommandPattern(string[] args)
        {
            this.args = args;
        }

        public bool IsAdd()
        {
            return args[0] == "add";
        }

        public bool IsList()
        {
            return args[0] == "list";
        }

        public bool IsChange()
        {
            return args[0] == "change";
        }

        public bool IsHelp()
        {
            return args[0] == "help";
        }
    }
}