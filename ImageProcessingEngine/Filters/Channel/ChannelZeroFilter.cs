using ImageProcessingEngine.Actions;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Channel
{
    [SecurityCritical]
    internal class ChannelZeroFilter : IPixelChannelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i, ARGBChannel channel)
        {
            try
            {
                var index = (int)channel;
                *(imgPtr + (i + index)) = 0;
            }
            catch {   }
        }

        #endregion
    }
}