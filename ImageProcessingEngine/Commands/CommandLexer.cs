using System;
using System.IO;
using System.Linq;

namespace ImageProcessingEngine.Commands
{
    internal static class CommandLexer
    {
        #region Constants

        private const string Extension = ".txt";
        private const char Delimiter   = ';';

        #endregion

        #region Methods

        internal static string[] Tokenize(string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));

            if (source.Contains(Delimiter))
                return source.Split(Delimiter);

            if (Path.GetExtension(source) == Extension && File.Exists(source))
                return File.ReadAllLines(source);

            return new [] { source };
        }

        #endregion
    }
}