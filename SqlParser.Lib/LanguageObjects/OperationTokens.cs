using System;
using System.Collections.Generic;

namespace SqlParser.Lib.LanguageObjects
{
    public static class OperationTokens
    {
        public static OperationToken Delete { get; } = new OperationToken("DELETE", 2, TokenMatcher.WholeOperationIndexFinder);

        public static OperationToken From { get; } = new OperationToken("FROM", 2, TokenMatcher.WholeOperationIndexFinder);

        public static OperationToken Equal { get; } = new OperationToken("=", 1, TokenMatcher.DefaultIndexFinder);

        public static OperationToken InsertInto { get; } = new OperationToken("INSERT INTO", 2, TokenMatcher.WholeOperationIndexFinder);

        public static OperationToken Is { get; } = new OperationToken("IS", 1, TokenMatcher.WholeOperationIndexFinder);

        public static OperationToken LessThan { get; } = new OperationToken("<", 1, TokenMatcher.DefaultIndexFinder);

        public static OperationToken OrderBy { get; } = new OperationToken("ORDER BY", 2, TokenMatcher.WholeOperationIndexFinder);

        public static OperationToken Select { get; } = new OperationToken("SELECT", 2, TokenMatcher.WholeOperationIndexFinder);

        public static OperationToken Use { get; } = new OperationToken("USE", 2, TokenMatcher.WholeOperationIndexFinder);

        public static OperationToken Values { get; } = new OperationToken("VALUES", 2, TokenMatcher.WholeOperationIndexFinder);

        public static OperationToken Where { get; } = new OperationToken("WHERE", 2, TokenMatcher.WholeOperationIndexFinder);

        public static IReadOnlyCollection<OperationToken> AllTokens { get; } = new List<OperationToken>()
        {
            Delete,
            Equal,
            From,
            InsertInto,
            Is,
            LessThan,
            OrderBy,
            Select,
            Use,
            Values,
            Where
        }.AsReadOnly();
    }
}
