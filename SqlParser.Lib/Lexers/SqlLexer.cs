using System;
using System.Collections.Generic;
using System.Linq;
using SqlParser.Lib.LanguageObjects;

namespace SqlParser.Lib.Lexers
{
    public static class SqlLexer
    {
        public static IList<SyntaxToken> Tokenise(string statement, IReadOnlyCollection<OperationToken> operationTokens)
        {
            if (operationTokens == null || operationTokens.Count == 0) throw new ArgumentException("No operation tokens provided", nameof(operationTokens));
            if (string.IsNullOrWhiteSpace(statement)) return new List<SyntaxToken>(0);

            return TokeniseStatement(statement, new List<SyntaxToken>(), operationTokens);
        }

        private static IList<SyntaxToken> TokeniseStatement(string statement, IList<SyntaxToken> tokens, IReadOnlyCollection<OperationToken> operationTokens)
        {
            var firstOperationFound = operationTokens
                .ToDictionary(t => t, t => t.IndexOf(statement))
                .Where(o => o.Value >= 0)
                .OrderBy(o => o.Value)
                .FirstOrDefault();

            if (!firstOperationFound.Equals(default(KeyValuePair<OperationToken, int>)))
            {
                if (firstOperationFound.Value > 0)
                {
                    tokens.Add(new SyntaxToken(statement.Substring(0, firstOperationFound.Value).TrimEnd()));
                }

                tokens.Add(firstOperationFound.Key);
                return TokeniseStatement(
                    statement.Substring(firstOperationFound.Value + firstOperationFound.Key.Value.Length).TrimStart(),
                    tokens,
                    operationTokens);
            }

            tokens.Add(new SyntaxToken(statement));
            return tokens;
        }
    }
}
