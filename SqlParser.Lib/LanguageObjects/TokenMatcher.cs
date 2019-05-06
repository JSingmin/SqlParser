using System;
using System.Collections.Generic;

namespace SqlParser.Lib.LanguageObjects
{
    public static class TokenMatcher
    {
        public static int DefaultIndexFinder(string operation, string statement)
        {
            return statement.IndexOf(operation, 0, statement.Length, StringComparison.OrdinalIgnoreCase);
        }

        public static int WholeOperationIndexFinder(string operation, string statement)
        {
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
            bool validEnding = (index + operation.Length + 1 == statement.Length) || char.IsWhiteSpace(statement[index + operation.Length]);

            return validBeginning && validEnding;
        }
    }
}
