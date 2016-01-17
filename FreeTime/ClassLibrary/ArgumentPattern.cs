namespace ClassLibrary
{
    public class ArgumentPattern
    {
        private readonly string pattern; 

        public ArgumentPattern(string pattern)
        {
            this.pattern = pattern;
        }

        public bool IsValue()
        {
            return pattern.Length > 0 && pattern[0] == '<' && pattern[pattern.Length - 1] == '>';
        }

        public bool IsOptional()
        {
            return pattern.Length > 0 && pattern[0] == '[' && pattern[pattern.Length - 1] == ']';
        }

        public bool IsVariableOptional()
        {
            return pattern.Length > 0 && pattern.StartsWith("[--") && pattern.EndsWith("]");
        }
    }
}
