using System;
using System.Collections.Generic;
using System.Linq;
using SqlParser.Lib.LanguageObjects;

namespace SqlParser.Lib.TreeBuilders
{
    public static class SyntaxTreeBuilder
    {
        public static SyntaxNode Build(IList<SyntaxToken> tokens)
        {
            if (tokens == null || tokens.Count == 0) throw new ArgumentException("No Tokens found", nameof(tokens));
            
            SyntaxNode rootNode = new SyntaxNode(tokens[0]);
            SyntaxNode currentNode = rootNode;

            for (int i = 1; i < tokens.Count(); i++)
            {
                var token = tokens[i];
                if (token is OperationToken)
                {
                    currentNode = PlaceOperationToken(token as OperationToken, currentNode);
                }
                else
                {
                    PlaceToken(token, rootNode);
                }
            }

            return rootNode;
        }

        private static SyntaxNode PlaceOperationToken(OperationToken token, SyntaxNode currentNode)
        {
            var newNode = new SyntaxNode(token);

            var currentOperationToken = (OperationToken)currentNode.Token;
            if (token.Precedence < currentOperationToken.Precedence)
            {
                var leftOperationToken = currentNode.Left?.Token as OperationToken;
                if (leftOperationToken != null)
                {
                    newNode.Right = currentNode.Left;
                }
                else
                {
                    newNode.Left = currentNode.Left;
                }

                currentNode.Left = newNode;
                return currentNode;
            }

            if (currentNode.Right == null)
            {
                currentNode.Right = newNode;
                return newNode;
            }

            var rightOperationToken = currentNode.Right.Token as OperationToken;
            if (rightOperationToken != null && token.Precedence < rightOperationToken.Precedence)
            {
                newNode.Left = currentNode.Left;
                currentNode.Left = newNode;
                return currentNode;
            }

            return currentNode;
        }

        private static void PlaceToken(SyntaxToken token, SyntaxNode rootNode)
        {
            var newNode = new SyntaxNode(token);
            var placed = false;
            var currentNode = rootNode;

            while (!placed)
            {
                if (currentNode.Right != null)
                {
                    currentNode = currentNode.Right;
                    continue;
                }

                if (currentNode.Left == null)
                {
                    currentNode.Left = newNode;
                    placed = true;
                    continue;
                }

                if (!(currentNode.Left.Token is OperationToken))
                {
                    currentNode.Right = newNode;
                    placed = true;
                    continue;
                }

                currentNode = currentNode.Left;
            }
        }
    }
}
