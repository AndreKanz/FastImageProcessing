using Contracts.ImageProcessingEngine.Commands;
using Contracts.ImageProcessingEngine.Processors;
using ImageProcessingEngine.Actions;
using ImageProcessingEngine.Filters.Channel;
using System.Collections.Generic;
using System.Drawing;
using System.Security;
using System.Threading.Tasks;

namespace ImageProcessingEngine.Processors
{
    [SecurityCritical]
    internal unsafe class ChannelProcessor : IImageProcessor
    {
        private static readonly IDictionary<string, IPixelChannelAction> Actions =
            new Dictionary<string, IPixelChannelAction>
            {
                { FilterMode.CMax,      new ChannelMaxFilter()               },
                { FilterMode.CInvert,   new ChannelInvertFilter()            },
                { FilterMode.CZero,     new ChannelZeroFilter()              },
                { FilterMode.CAverage,  new ChannelAverageFilter()           },
                { FilterMode.CVariance, new ChannelVarianceFilter()          },
                { FilterMode.CStdDev,   new ChannelStandardDeviationFilter() }
            };

        #region Methods

        public void Process(Bitmap imgSource, IProcessorCommand command)
        {
            if (imgSource == null || !Actions.ContainsKey(command.Command))
                return;

            var action = Actions[command.Command];
            var channel = EnumUtil.Convert<ARGBChannel>(command.Parameter.ToUpper());
            var data = imgSource.GetBitmapData();
            var bof = data.GetBof();
            var pixelBytes = imgSource.GetPixelBytes();
            var byteCount = pixelBytes * data.Width;

            Parallel.For(0, data.Height, y =>
            {
                var currentPointer = bof + y * data.Stride;
                for (var x = 0; x < byteCount; x += pixelBytes)
                    action.Process(currentPointer, x, channel);
            });
            imgSource.UnlockBits(data);
        }

        #endregion
    }
}