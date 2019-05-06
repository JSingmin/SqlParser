using System;

namespace SqlParser.Lib.LanguageObjects
{
    public class OperationToken : SyntaxToken
    {
        public OperationToken(string tokenValue, byte precedence, Func<string, string, int> indexFinder) : base(tokenValue)
        {
            if (indexFinder == null) throw new ArgumentException("No index search function provided", nameof(indexFinder));

            this.Precedence = precedence;
            this.IndexFinder = indexFinder;
        }

        public byte Precedence { get; }

        public Func<string, string, int> IndexFinder { get; }

        public int IndexOf(string statement)
        {
            return this.IndexFinder(base.Value, statement);
        }
    }
}
