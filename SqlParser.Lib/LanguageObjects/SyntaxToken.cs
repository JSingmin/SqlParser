using System;

namespace SqlParser.Lib.LanguageObjects
{
    public class SyntaxToken
    {
        public SyntaxToken(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Invalid value given for SyntaxToken", nameof(value));

            this.Value = value;
        }

        public string Value { get; }
    }
}
