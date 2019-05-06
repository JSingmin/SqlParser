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

        
    }
}
