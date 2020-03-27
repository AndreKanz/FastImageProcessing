using ImageProcessingEngine.Actions;
using System;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Channel
{
    [SecurityCritical]
    internal class ChannelStandardDeviationFilter : IPixelChannelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i, ARGBChannel channel)
        {
            try
            {
                var index = (int)channel;
                var avg = (byte)((*(imgPtr + i) + *(imgPtr + (i + 1)) + *(imgPtr + (i + 2))) / 3);
                var s = (byte)Math.Sqrt(
                    ((*(imgPtr + i) - avg) * (*(imgPtr + i) - avg)
                    + (*(imgPtr + (i + 1)) - avg) * (*(imgPtr + (i + 1)) - avg)
                    + (*(imgPtr + (i + 2)) - avg) * (*(imgPtr + (i + 2)) - avg))
                    / 3);
                *(imgPtr + (i + index)) = s;
            }
            catch {   }
        }

        #endregion
    }
}