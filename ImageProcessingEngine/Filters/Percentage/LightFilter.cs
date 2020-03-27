using ImageProcessingEngine.Actions;
using System;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Percentage
{
    [SecurityCritical]
    internal class LightFilter : IPixelPercentageAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i, double percentage)
        {
            try
            {
                *(imgPtr + i) = (byte)Math.Min(255, *(imgPtr + i) + 255 * percentage);
                *(imgPtr + (i + 1)) = (byte)Math.Min(255, *(imgPtr + (i + 1)) + 255 * percentage);
                *(imgPtr + (i + 2)) = (byte)Math.Min(255, *(imgPtr + (i + 2)) + 255 * percentage);
            }
            catch {   }
        }

        #endregion
    }
}