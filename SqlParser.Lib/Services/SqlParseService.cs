using System;
using System.Collections.Generic;
using System.Linq;
using SqlParser.Lib.LanguageObjects;
using SqlParser.Lib.Lexers;
using SqlParser.Lib.TreeBuilders;

namespace SqlParser.Lib.Services
{
    public static class SqlParseService
    {
        public static SyntaxNode BuildSyntaxTree(string statement)
        {
            if (string.IsNullOrWhiteSpace(statement)) throw new ArgumentException("No statement to process", nameof(statement));

            var tokens = SqlLexer.Tokenise(statement, OperationTokens.AllTokens);
            var syntaxTree = SyntaxTreeBuilder.Build(tokens);

            return syntaxTree;
        }
    }
}
