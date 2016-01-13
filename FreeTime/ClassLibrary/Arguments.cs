namespace ClassLibrary
{
    using System.Collections.Generic;

    public class Arguments
    {
        private List<Argument> schema = new List<Argument>();
        private Dictionary<string, string> argsFound = new Dictionary<string, string>();

        public Arguments(string schema, string[] arguments)
        {
            this.ParseUsage(schema);
            this.Parse(arguments);
        }

        private void ParseUsage(string usage)
        {
            var terms = usage.Split(" ".ToCharArray());
            foreach (var term in terms)
                ParseTerm(term);
        }

        private void ParseTerm(string term)
        {
            var pattern = new ArgumentPattern(term);
            if (pattern.IsOptional())
                schema.Add(new OptionalArgument(term));
            else if (pattern.IsValue())
                schema.Add(new ArgumentValue(term));
            else
                schema.Add(new CommandArgument(term));
        }

        private void Parse(string[] arguments)
        {
            CheckThatThereAreArguments(arguments);
            ParseArguments(arguments);
        }

        private void ParseArguments(string[] arguments)
        {
            for (int i = 0; i < schema.Count; i++)
            {
                schema[i].Parse(i < arguments.Length ? arguments[i] : string.Empty);
                schema[i].SaveValue(this.argsFound);
            }
        }

        private static void CheckThatThereAreArguments(string[] arguments)
        {
            if (arguments.Length == 0)
                throw new ArgumentMissing();
        }

        public string this[string arg] => argsFound[arg];
    }
}
