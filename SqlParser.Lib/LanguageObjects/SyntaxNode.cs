using System;

namespace SqlParser.Lib.LanguageObjects
{
    public class SyntaxNode
    {
        public SyntaxNode(SyntaxToken token)
        {
            this.Token = token;
        }

        public SyntaxToken Token { get; }
        public SyntaxNode Left { get; set; }
        public SyntaxNode Right { get; set; }

        public void DisplayTree(Action<string> printer)
        {
            printer(this.Token.Value);
            this.Left?.DisplayTree(printer);
            this.Right?.DisplayTree(printer);
        }
    }
}
