using Contracts.Dto;

namespace Contracts.ImageProcessingEngine.Commands
{
    public interface ICommandSetParser
    {
        #region Methods

        IProcessorCommand[] GetCommandSet(string source);
        IProcessorCommand[] GetCommandSet(IEngineCommandDto commandDto);

        #endregion
    }
}