using System.Text.RegularExpressions;

namespace ImageProcessingEngine.Commands
{
    internal static class RegexExtensions
    {
        #region Methods

        internal static string GetValue(this Match match, int group)
        {
            return match.Groups[group].Value;
        }

        #endregion
    }
}