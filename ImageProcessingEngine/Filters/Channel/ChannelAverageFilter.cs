using ImageProcessingEngine.Actions;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Channel
{
    [SecurityCritical]
    internal class ChannelAverageFilter : IPixelChannelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i, ARGBChannel channel)
        {
            try
            {
                var index = (int)channel;
                *(imgPtr + (i + index)) = (byte)((*(imgPtr + i) 
                    + *(imgPtr + (i + 1)) 
                    + *(imgPtr + (i + 2))) 
                    / 3);
            }
            catch {   }
        }

        #endregion
    }
}