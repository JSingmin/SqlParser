using System;
using System.Collections.Generic;

namespace SqlParser.Lib.LanguageObjects
{
    public static class TokenMatcher
    {
        public static int DefaultIndexFinder(string operation, string statement)
        {
            // Certain tokens do not need to be separated by whitespace (e.g. +, -, *, =).
            // This is the index finder we'd use for these tokens

            return statement.IndexOf(operation, 0, statement.Length, StringComparison.OrdinalIgnoreCase);
        }

        public static int WholeOperationIndexFinder(string operation, string statement)
        {
            // As the nature of SQL is akin to natural languages, most key words are whitepace separated.
            // This index finder takes this into account, looking for the first instance of the token
            // where the token is surrounded on either side by whitespace (or is at the start/end of the statement)

            int statementIndex = 0;
            while (statementIndex < statement.Length)
            {
                int index = statement.IndexOf(operation, statementIndex, statement.Length - statementIndex, StringComparison.OrdinalIgnoreCase);
                if (index < 0 || IsWholeOperation(operation, statement, index))
                {
                    return index;
                }

                statementIndex = index + operation.Length;
            }

            return -1;
        }

        private static bool IsWholeOperation(string operation, string statement, int index)
        {
            bool validBeginning = index == 0 || char.IsWhiteSpace(statement[index - 1]);
            bool validEnding = (index + operation.Length == statement.Length) || char.IsWhiteSpace(statement[index + operation.Length]);

            return validBeginning && validEnding;
        }
    }
}
