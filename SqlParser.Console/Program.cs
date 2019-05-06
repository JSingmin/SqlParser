using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SqlParser.Lib.Readers;
using SqlParser.Lib.Services;

namespace SqlParser.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0) throw new ArgumentException("File Name required as argument");

            try
            {
                IEnumerable<string> statements = await FileReader.Read(args[0]);
                foreach(string statement in statements)
                {
                    var syntaxTree = SqlParseService.BuildSyntaxTree(statement);
                    syntaxTree.DisplayTree(System.Console.WriteLine);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Error parsing SQL file: {e.Message}");
                System.Console.WriteLine(e.StackTrace);
            }
        }
    }
}
