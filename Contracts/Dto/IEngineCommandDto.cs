namespace Contracts.Dto
{
    public interface IEngineCommandDto
    {
        #region Properties

        string CommandString { get; }
        string Base64Image { get; }

        #endregion
    }
}
