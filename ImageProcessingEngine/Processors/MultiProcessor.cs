using Contracts.ImageProcessingEngine.Commands;
using Contracts.ImageProcessingEngine.Processors;
using ImageProcessingEngine.Actions;
using ImageProcessingEngine.Filters.Multi;
using System.Collections.Generic;
using System.Drawing;
using System.Security;
using System.Threading.Tasks;

namespace ImageProcessingEngine.Processors
{
    [SecurityCritical]
    internal unsafe class MultiProcessor : IImageProcessor
    {
        #region Fields

        private static readonly IDictionary<string, IMultiPixelAction> Actions =
            new Dictionary<string, IMultiPixelAction>
            {
                { FilterMode.Blur,           new BlurFilter()           },
                { FilterMode.FuzzyGrayscale, new FuzzyGrayscaleFilter() }
            };

        #endregion

        #region Methods

        public void Process(Bitmap imgSource, IProcessorCommand command)
        {
            if (imgSource == null || !Actions.ContainsKey(command.Command))
                return;

            var action = Actions[command.Command];
            var data = imgSource.GetBitmapData();
            var bof = data.GetBof();
            var pixelBytes = imgSource.GetPixelBytes();
            var byteCount = pixelBytes * data.Width;
            Parallel.For(0, data.Height, y =>
            {
                var currentPointer = bof + y * data.Stride;
                var beforePointer = bof + (y - 1) * data.Stride;
                var afterPointer = bof + (y + 1) * data.Stride;
                for (var x = 0; x < byteCount - 9; x += pixelBytes)
                    action.Process(currentPointer, beforePointer, afterPointer, x, x - pixelBytes, x + pixelBytes);
            });
            imgSource.UnlockBits(data);
        }

        #endregion
    }
}