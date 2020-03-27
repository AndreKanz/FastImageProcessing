using Contracts.Dto;

namespace FastImageApi
{
    public class EngineCommandDto : IEngineCommandDto
    {
        #region Properties

        public string CommandString { get; set; }
        public string Base64Image   { get; set; }

        #endregion
    }
}