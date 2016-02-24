namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ArgumentsConverter
    {
        private string[] args;

        public ArgumentsConverter(string [] args)
        {
            this.args = args;
        }

        public string[] Conversion()
        {
            var result = new List<string>();
            for (int i = 0; i < args.Length; i++) 
            {
                if (i == args.Length - 1)
                    result.Add(args[i]);
                else if (args[i].StartsWith("--") && !args[i].Contains(":") && !args[i + 1].StartsWith("--"))
                {
                    result.Add($"{args[i]}:{args[i + 1]}");
                    i++;
                }
                else
                    result.Add(args[i]);
            }
            return result.ToArray();
        }
    }
}
