using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SqlParser.Lib.Readers
{
    public static class FileReader
    {
        private const string _seperator = ";";

        public static string DefaultSeperator { get; } = _seperator;

        public static async Task<IEnumerable<string>> Read(string fileName, string seperator = _seperator)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("Invalid file name provided", nameof(fileName));
            if (string.IsNullOrEmpty(seperator)) throw new ArgumentException("Invalid statement seperator specified", nameof(seperator));
            if (!File.Exists(fileName)) throw new FileNotFoundException("Could not find file", fileName);

            using (StreamReader sr = new StreamReader(fileName))
            {
                string fileContents = await sr.ReadToEndAsync();
                return fileContents.Split(seperator).Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => CleanString(s));
            }
        }

        private static string CleanString(string value)
        {
            return Regex.Replace(value, @"\s+", " ").Trim();
        }
    }
}
