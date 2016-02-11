namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;

    public class Arguments
    {
        private List<Argument> schema = new List<Argument>();
        private readonly Dictionary<string, string> argsFound = new Dictionary<string, string>();
        private int firstLineArguments;
        private int schemaLine;

        public Arguments(string schema, string[] arguments)
        {
            this.ParseCommands(schema);
            this.Parse(arguments);
        }

        private void ParseCommands(string commands)
        {
            var command = commands.Split("\n".ToCharArray());
            foreach (var usage in command)
            {
                schemaLine++;
                ParseUsage(usage);
            }
        }

        private void ParseUsage(string usage)
        {
            var terms = usage.Split(" ".ToCharArray());
            firstLineArguments = terms.Length;
            foreach (var term in terms)
            {
                if (schemaLine == 1)
                    ParseTerm(term);
                else
                    schema.Add(new FalseArgument(term));
            }
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
            var argumentList = new List<string>();
            foreach (var arg in arguments)
                argumentList.Add(arg);
            var schemaList = new List<Argument>();
            foreach (var element in schema)
                schemaList.Add(element);
            foreach (var element in schema)
            {
                if (element.GetType() == typeof(VariableOptionalArgument)|| element.GetType() == typeof(OptionalArgument))
                {
                    for(int i=0;i<arguments.Length;i++)
                    {
                        element.Parse(arguments[i]);
                        if (element.IsValid(arguments[i]))
                        {
                            argumentList.RemoveAt(argumentList.IndexOf(arguments[i]));
                            element.SaveValue(argsFound);
                            break;
                        }
                        if (i == arguments.Length - 1)
                            element.SaveValue(argsFound);
                    }
                    schemaList.RemoveAt(schemaList.IndexOf(element));
                }
            }

            for (int i = 0; i < schemaList.Count; i++)
            {
                schemaList[i].Parse(i < argumentList.Count ? argumentList[i] : string.Empty);
                schemaList[i].SaveValue(argsFound);
            }
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
