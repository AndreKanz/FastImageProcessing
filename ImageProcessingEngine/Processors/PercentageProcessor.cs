using Contracts.ImageProcessingEngine;
using Contracts.ImageProcessingEngine.Commands;
using Contracts.ImageProcessingEngine.Processors;
using ImageProcessingEngine.Actions;
using ImageProcessingEngine.Filters.Percentage;
using System.Collections.Generic;
using System.Drawing;
using System.Security;
using System.Threading.Tasks;

namespace ImageProcessingEngine.Processors
{
    [SecurityCritical]
    internal unsafe class PercentageProcessor : IImageProcessor
    {
        #region Fields

        private static readonly IDictionary<string, IPixelPercentageAction> Actions =
            new Dictionary<string, IPixelPercentageAction>
            {
                { FilterMode.Light, new LightFilter() },
                { FilterMode.Dark,  new DarkFilter()  }
            };

        #endregion

        #region Methods

        public void Process(Bitmap imgSource, IProcessorCommand command)
        {
            if (imgSource == null || !Actions.ContainsKey(command.Command))
                return;

            var action = Actions[command.Command];
            var percentage = double.Parse(command.Parameter);
            var data = imgSource.GetBitmapData();
            var bof = data.GetBof();
            var pixelBytes = imgSource.GetPixelBytes();
            var byteCount = pixelBytes * data.Width;

            Parallel.For(0, data.Height, y =>
            {
                var currentPointer = bof + y * data.Stride;
                for (var x = 0; x < byteCount; x += pixelBytes)
                    action.Process(currentPointer, x, percentage);
            });
            imgSource.UnlockBits(data);
        }

        #endregion
    }
}