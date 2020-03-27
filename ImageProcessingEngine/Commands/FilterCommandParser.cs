using Contracts.ImageProcessingEngine.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ImageProcessingEngine.Commands
{
    internal static class FilterCommandParser
    {
        #region Fields

        private static readonly Regex CommandExpression =
            new Regex(@"^(blue|red|green|grayscale|negative|variance|stddev)|(fuzzygrayscale|blur)|(cmax|cinvert|czero|cvariance|cstddev|caverage)\s*:\s*'([argb])'|(light|dark)\s*:\s*'([0-9]+(?:\.[0-9]+)?)'|(binary)$",
            RegexOptions.Compiled);

        private static readonly IDictionary<int, Func<Match, ProcessorCommand>> Commands =
            new Dictionary<int, Func<Match, ProcessorCommand>>
            {
                { 1, m => new ProcessorCommand(ProcessorMode.Single, m.GetValue(1))                    },
                { 2, m => new ProcessorCommand(ProcessorMode.Multi, m.GetValue(2))                     },
                { 3, m => new ProcessorCommand(ProcessorMode.Channel, m.GetValue(3), m.GetValue(4))    },
                { 5, m => new ProcessorCommand(ProcessorMode.Percentage, m.GetValue(5), m.GetValue(6)) },
                { 7, m => new ProcessorCommand(ProcessorMode.Binary, m.GetValue(7))                    }
            };

        #endregion

        #region Methods

        internal static IProcessorCommand ParseLine(string command)
        {
            if (string.IsNullOrEmpty(command))
                throw new ArgumentNullException(nameof(command));

            var match = CommandExpression.Match(command.ToLower());
            if (!match.Success)
                throw new InvalidFormatException(nameof(match));

            var index = GetGroupIndex(match);
            if (index == default(int))
                throw new InvalidFormatException(nameof(match));

            return Commands[index](match);
        }

        private static int GetGroupIndex(Match match)
        {
            return Commands
                .Keys
                .FirstOrDefault(x => match.Groups[x].Success);
        }

        #endregion
    }
}