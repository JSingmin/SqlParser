using System;
using System.Collections.Generic;
using SqlParser.Lib.LanguageObjects;
using SqlParser.Lib.TreeBuilders;
using Xunit;

namespace SqlParser.Lib.Tests.TreeBuilders
{
    public class SyntaxTreeBuilderTests
    {
        [Fact]
        public void PassingNullTokensListThrowsException()
        {
            Assert.Throws<ArgumentException>(() => SyntaxTreeBuilder.Build(null));
        }

        [Fact]
        public void PassingEmptyTokensListThrowsException()
        {
            var tokens = new List<SyntaxToken>();
            Assert.Throws<ArgumentException>(() => SyntaxTreeBuilder.Build(tokens));
        }

        [Fact]
        public void BuildsSyntaxTreeUseExample()
        {
            var tokens = new List<SyntaxToken>
            {
                OperationTokens.Use,
                new SyntaxToken("database1")
            };

            var result = SyntaxTreeBuilder.Build(tokens);
            var expectedResult = new SyntaxNode(OperationTokens.Use)
            {
                Left = new SyntaxNode(new SyntaxToken("database1"))
            };

            Assert.True(AreSyntaxTreesEqual(expectedResult, result));
        }

        [Fact]
        public void BuildsSyntaxTreeSelectExample()
        {
            var tokens = new List<SyntaxToken>
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

            var result = SyntaxTreeBuilder.Build(tokens);
            var expectedResult = new SyntaxNode(OperationTokens.Select)
            {
                Left = new SyntaxNode(new SyntaxToken("id, name, address")),
                Right = new SyntaxNode(OperationTokens.From)
                {
                    Left = new SyntaxNode(new SyntaxToken("users")),
                    Right = new SyntaxNode(OperationTokens.Where)
                    {
                        Left = new SyntaxNode(OperationTokens.Is)
                        {
                            Left = new SyntaxNode(new SyntaxToken("is_customer")),
                            Right = new SyntaxNode(new SyntaxToken("NOT NULL"))
                        },
                        Right = new SyntaxNode(OperationTokens.OrderBy)
                        {
                            Left = new SyntaxNode(new SyntaxToken("created"))
                        }
                    }
                }
            };

            Assert.True(AreSyntaxTreesEqual(expectedResult, result));
        }

        [Fact]
        public void BuildsSyntaxTreeInsertExample()
        {
            var tokens = new List<SyntaxToken>
            {
                OperationTokens.InsertInto,
                new SyntaxToken("user_notes (id, user_id, note, created)"),
                OperationTokens.Values,
                new SyntaxToken("(1, 1, \"Note 1\", NOW())")
            };

            var result = SyntaxTreeBuilder.Build(tokens);
            var expectedResult = new SyntaxNode(OperationTokens.InsertInto)
            {
                Left = new SyntaxNode(new SyntaxToken("user_notes (id, user_id, note, created)")),
                Right = new SyntaxNode(OperationTokens.Values)
                {
                    Left = new SyntaxNode(new SyntaxToken("(1, 1, \"Note 1\", NOW())"))
                }
            };

            Assert.True(AreSyntaxTreesEqual(expectedResult, result));
        }

        [Fact]
        public void CorrectlyTokenisesDeleteExample()
        {
            var tokens = new List<SyntaxToken>
            {
                OperationTokens.Delete,
                OperationTokens.From,
                new SyntaxToken("database2.logs"),
                OperationTokens.Where,
                new SyntaxToken("id"),
                OperationTokens.LessThan,
                new SyntaxToken("1000")
            };

            var result = SyntaxTreeBuilder.Build(tokens);
            var expectedResult = new SyntaxNode(OperationTokens.Delete)
            {
                Right = new SyntaxNode(OperationTokens.From)
                {
                    Left = new SyntaxNode(new SyntaxToken("database2.logs")),
                    Right = new SyntaxNode(OperationTokens.Where)
                    {
                        Left = new SyntaxNode(OperationTokens.LessThan)
                        {
                            Left = new SyntaxNode(new SyntaxToken("id")),
                            Right = new SyntaxNode(new SyntaxToken("1000"))
                        }
                    }
                }
            };

            Assert.True(AreSyntaxTreesEqual(expectedResult, result));
        }

        private static bool AreSyntaxTreesEqual(SyntaxNode expected, SyntaxNode result)
        {
            if (expected == null && result == null) return true;

            return string.Equals(expected?.Token.Value, result?.Token.Value)
                && AreSyntaxTreesEqual(expected.Left, result.Left)
                && AreSyntaxTreesEqual(expected.Right, result.Right);
        }
    }
}
