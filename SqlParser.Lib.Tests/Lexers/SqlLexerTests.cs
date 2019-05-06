using System;
using System.Collections.Generic;
using SqlParser.Lib.LanguageObjects;
using SqlParser.Lib.Lexers;
using Xunit;

namespace SqlParser.Lib.Tests.Lexers
{
    public class SqlLexerTests
    {
        [Fact]
        public void PassingNullOperationTokensThrowsException()
        {
            Assert.Throws<ArgumentException>(() => SqlLexer.Tokenise("", null));
        }

        [Fact]
        public void PassingEmptyOperationTokensThrowsException()
        {
            var operationTokens = new List<OperationToken>().AsReadOnly();
            Assert.Throws<ArgumentException>(() => SqlLexer.Tokenise("", operationTokens));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void PassingEmptyStatementReturnsNoTokens(string statement)
        {
            var expectedResult = new List<SyntaxToken>(0);
            Assert.Equal(expectedResult, SqlLexer.Tokenise(statement, OperationTokens.AllTokens));
        }

        [Theory]
        [InlineData("USE database1")]
        public void CorrectlyTokenisesUseExample(string statement)
        {
            var result = SqlLexer.Tokenise(statement, OperationTokens.AllTokens);
            var expectedResult = new List<SyntaxToken>
            {
                OperationTokens.Use,
                new SyntaxToken("database1")
            };

            Assert.Equal(expectedResult.Count, result.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Value, result[i].Value);
            }
        }

        [Theory]
        [InlineData("SELECT id, name, address FROM users WHERE is_customer IS NOT NULL ORDER BY created")]
        public void CorrectlyTokenisesSelectExample(string statement)
        {
            var result = SqlLexer.Tokenise(statement, OperationTokens.AllTokens);
            var expectedResult = new List<SyntaxToken>
            {
                OperationTokens.Select,
                new SyntaxToken("id, name, address"),
                OperationTokens.From,
                new SyntaxToken("users"),
                OperationTokens.Where,
                new SyntaxToken("is_customer"),
                OperationTokens.Is,
                new SyntaxToken("NOT NULL"),
                OperationTokens.OrderBy,
                new SyntaxToken("created")
            };

            Assert.Equal(expectedResult.Count, result.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Value, result[i].Value);
            }
        }

        [Theory]
        [InlineData("INSERT INTO user_notes (id, user_id, note, created) VALUES (1, 1, \"Note 1\", NOW())")]
        public void CorrectlyTokenisesInsertExample(string statement)
        {
            var result = SqlLexer.Tokenise(statement, OperationTokens.AllTokens);
            var expectedResult = new List<SyntaxToken>
            {
                OperationTokens.InsertInto,
                new SyntaxToken("user_notes (id, user_id, note, created)"),
                OperationTokens.Values,
                new SyntaxToken("(1, 1, \"Note 1\", NOW())")
            };

            Assert.Equal(expectedResult.Count, result.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Value, result[i].Value);
            }
        }

        [Theory]
        [InlineData("DELETE FROM database2.logs WHERE id < 1000")]
        [InlineData("DELETE FROM database2.logs WHERE id<1000")]
        public void CorrectlyTokenisesDeleteExample(string statement)
        {
            var result = SqlLexer.Tokenise(statement, OperationTokens.AllTokens);
            var expectedResult = new List<SyntaxToken>
            {
                OperationTokens.Delete,
                OperationTokens.From,
                new SyntaxToken("database2.logs"),
                OperationTokens.Where,
                new SyntaxToken("id"),
                OperationTokens.LessThan,
                new SyntaxToken("1000")
            };

            Assert.Equal(expectedResult.Count, result.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Value, result[i].Value);
            }
        }
    }
}
