using System.Collections.Generic;
using System.Drawing;

namespace Contracts.ImageProcessingEngine.Processors
{
    public interface IHistogramProcessor
    {
        #region Methods

        IDictionary<Color, int> Process(Bitmap imgSource);

        #endregion
    }
}