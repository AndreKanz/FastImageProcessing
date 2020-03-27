using Contracts.ImageProcessingEngine;
using Contracts.ImageProcessingEngine.Commands;
using Contracts.ImageProcessingEngine.Processors;
using ImageProcessingEngine.Actions;
using ImageProcessingEngine.Filters.Single;
using System.Collections.Generic;
using System.Drawing;
using System.Security;
using System.Threading.Tasks;

namespace ImageProcessingEngine.Processors
{
    [SecurityCritical]
    internal unsafe class SingleProcessor : IImageProcessor
    {
        #region Fields

        private static readonly IDictionary<string, ISinglePixelAction> Actions =
            new Dictionary<string, ISinglePixelAction>
            {
                { FilterMode.Blue,      new BlueFilter()              },
                { FilterMode.Red,       new RedFilter()               },
                { FilterMode.Green,     new GreenFilter()             },
                { FilterMode.Grayscale, new GrayscaleFilter()         },
                { FilterMode.Negative,  new NegativeFilter()          },
                { FilterMode.Variance,  new VarianceFilter()          },
                { FilterMode.StdDev,    new StandardDeviationFilter() }
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
                for (var x = 0; x < byteCount; x += pixelBytes)
                    action.Process(currentPointer, x);
            });
            imgSource.UnlockBits(data);
        }

        #endregion
    }
}