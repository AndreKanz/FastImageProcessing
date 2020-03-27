namespace Contracts.ImageProcessingEngine.Commands
{
    public interface IProcessorCommand
    {
        #region Properties

        string Mode      { get; }
        string Command   { get; }
        string Parameter { get; }

        #endregion
    }
}