using System;
using System.Collections.Generic;
using SqlParser.Lib.LanguageObjects;
using Xunit;

namespace SqlParser.Lib.Tests.LanguageObjects
{
    public class TokenMatcherTests
    {
        [Theory]
        [InlineData("+", "1 + 1 = 2", 2)]
        [InlineData("+", "1+1=2", 1)]
        [InlineData("1", "1+1=2", 0)]
        [InlineData("A", "A+B=C", 0)]
        [InlineData("C", "a+b=c", 4)]
        [InlineData("-", "1+1=2", -1)]
        public void DefaultIndexFinderCorrectlyFindsIndex(string operation, string statement, int expectedIndex)
        {
            Assert.Equal(expectedIndex, TokenMatcher.DefaultIndexFinder(operation, statement));
        }

        [Theory]
        [InlineData("+", "1 + 1 = 2", 2)]
        [InlineData("+", "1+1=2", -1)]
        [InlineData("A", "CASE A", 5)]
        [InlineData("A", "A CASE", 0)]
        [InlineData("A", "case a", 5)]
        [InlineData("A", "a case", 0)]
        [InlineData("IS", "ISO LIS IS IS", 8)]
        public void WholeOperationIndexFinderCorrectlyFindsIndex(string operation, string statement, int expectedIndex)
        {
            Assert.Equal(expectedIndex, TokenMatcher.WholeOperationIndexFinder(operation, statement));
        }
    }
}
