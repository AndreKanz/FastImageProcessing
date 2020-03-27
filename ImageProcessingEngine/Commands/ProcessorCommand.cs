using Contracts.ImageProcessingEngine.Commands;

namespace ImageProcessingEngine.Commands
{
    public class ProcessorCommand : IProcessorCommand
    {
        #region Properties

        public string Mode      { get; }
        public string Command   { get; }
        public string Parameter { get; }

        #endregion

        #region Constructors

        public ProcessorCommand(string mode, string command)
        {
            Mode = mode;
            Command = command;
        }

        public ProcessorCommand(string mode, string command, string parameter)
        {
            Mode = mode;
            Command = command;
            Parameter = parameter;
        }

        #endregion
    }
}