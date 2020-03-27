using Contracts.ImageProcessingEngine.Commands;
using System.Drawing;

namespace Contracts.ImageProcessingEngine.Processors
{
    public interface IImageProcessor
    {
        #region Methods

        void Process(Bitmap imgSource, IProcessorCommand command);

        #endregion
    }
}