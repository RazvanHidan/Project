namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;

    public class Arguments
    {
        private List<Argument> schema = new List<Argument>();
        private readonly Dictionary<string, string> argsFound = new Dictionary<string, string>();
        private int firstLineArguments;
        private int numberOfOptinalVariable;

        public Arguments(string schema, string[] arguments)
        {
            this.ParseCommands(schema);
            this.Parse(arguments);
        }

        private void ParseCommands(string commands)
        {
            var command = commands.Split("\n".ToCharArray());
            foreach (var usage in command)
                ParseUsage(usage);
        }

        private void ParseUsage(string usage)
        {
            var terms = usage.Split(" ".ToCharArray());
            firstLineArguments = terms.Length;
            foreach (var term in terms)
                ParseTerm(term);
        }

        private void ParseTerm(string term)
        {
            var pattern = new ArgumentPattern(term);
            if (pattern.IsVariableOptional())
                schema.Add(new VariableOptionalArgument(term));
            else if (pattern.IsOptional())
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
                if (i >= firstLineArguments)
                    try
                    {
                        ParseArg(arguments, i);
                    }
                    catch (Exception)
                    {
                        FalseArguments(i);
                        break;
                    }
                else
                {
                    ParseArg(arguments, i);
                }
            }
        }

        private void ParseArg(string[] arguments, int i)
        {
            var k = i - numberOfOptinalVariable;
            schema[i].Parse(k < arguments.Length ? arguments[k] : string.Empty);
            schema[i].SaveValue(argsFound);
            if (schema[i].IsOptioanArgument())
                numberOfOptinalVariable++;
        }

        private void FalseArguments(int i)
        {
            for (int k = i; k < schema.Count; k++)
                schema[k].SaveValue(argsFound);
        }

        private static void CheckThatThereAreArguments(string[] arguments)
        {
            if (arguments.Length == 0)
                throw new ArgumentMissing();
        }

        public string this[string arg] => argsFound[arg];
    }
}
