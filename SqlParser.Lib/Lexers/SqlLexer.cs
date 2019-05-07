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
            // find the index of all provided operation tokens, and select the earliest operation token found
            var firstOperationFound = operationTokens
                .ToDictionary(t => t, t => t.IndexOf(statement))
                .Where(o => o.Value >= 0)
                .OrderBy(o => o.Value)
                .FirstOrDefault();

            if (!firstOperationFound.Equals(default(KeyValuePair<OperationToken, int>)))
            {
                // if the statement doesn't start with the found operation token,
                // add the part before the found index as a non-operation token
                if (firstOperationFound.Value > 0)
                {
                    tokens.Add(new SyntaxToken(statement.Substring(0, firstOperationFound.Value).TrimEnd()));
                }

                // Add the operation token, and recursively tokenise the remainder of the statement
                tokens.Add(firstOperationFound.Key);
                return TokeniseStatement(
                    statement.Substring(firstOperationFound.Value + firstOperationFound.Key.Value.Length).TrimStart(),
                    tokens,
                    operationTokens);
            }

            // No operation tokens were found, the remainder of the statement is a non-operation.
            tokens.Add(new SyntaxToken(statement));
            return tokens;
        }
    }
}
