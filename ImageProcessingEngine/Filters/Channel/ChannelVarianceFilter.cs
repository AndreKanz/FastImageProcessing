using ImageProcessingEngine.Actions;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Channel
{
    [SecurityCritical]
    internal class ChannelVarianceFilter : IPixelChannelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i, ARGBChannel channel)
        {
            try
            {
                var index = (int)channel;
                var avg = (byte)((*(imgPtr + i) + *(imgPtr + (i + 1)) + *(imgPtr + (i + 2))) / 3);
                var v = (byte)(
                    ((*(imgPtr + i) - avg) * (*(imgPtr + i) - avg)
                    + (*(imgPtr + (i + 1)) - avg) * (*(imgPtr + (i + 1)) - avg)
                    + (*(imgPtr + (i + 2)) - avg) * (*(imgPtr + (i + 2)) - avg))
                    / 3);
                *(imgPtr + (i + index)) = v;
            }
            catch {   }
        }

        #endregion
    }
}