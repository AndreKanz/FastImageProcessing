using Contracts.Dto;
using Contracts.ImageProcessingEngine.Commands;
using System;
using System.IO;
using System.Linq;

namespace ImageProcessingEngine.Commands
{
    public class CommandSetParser : ICommandSetParser
    {
        #region Methods

        public IProcessorCommand[] GetCommandSet(string source)
        {
            try
            {
                return GetCommands(source);
            }
            catch (Exception ex)
            {
                if (IsHandeled(ex))
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                    return null;
                }
                else
                    throw;
            }
        }

        public IProcessorCommand[] GetCommandSet(IEngineCommandDto commandDto)
        {
            try
            {
                return GetCommands(commandDto.CommandString);
            }
            catch (Exception ex)
            {
                if (IsHandeled(ex))
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                    return null;
                }
                else
                    throw;
            }
        }

        private static bool IsHandeled(Exception ex)
        {
            return ex is ArgumentNullException 
                || ex is InvalidFormatException 
                || ex is ArgumentOutOfRangeException 
                || ex is FileNotFoundException;
        }

        private static IProcessorCommand[] GetCommands(string source)
        {
            return CommandLexer
                .Tokenize(source)
                .Select(FilterCommandParser.ParseLine)
                .ToArray();
        }

        #endregion
    }
}