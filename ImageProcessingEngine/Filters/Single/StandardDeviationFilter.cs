using ImageProcessingEngine.Actions;
using System;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Single
{
    [SecurityCritical]
    internal class StandardDeviationFilter : ISinglePixelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i)
        {
            try
            {
                var avg = (byte)((*(imgPtr + i) + *(imgPtr + (i + 1)) + *(imgPtr + (i + 2))) / 3);
                var stdDev = (byte)Math.Sqrt(
                      ((*(imgPtr + i) - avg) * (*(imgPtr + i) - avg)
                     + (*(imgPtr + (i + 1)) - avg) * (*(imgPtr + (i + 1)) - avg)
                     + (*(imgPtr + (i + 2)) - avg) * (*(imgPtr + (i + 2)) - avg))
                     / 3);

                *(imgPtr + i) = stdDev;
                *(imgPtr + (i + 1)) = stdDev;
                *(imgPtr + (i + 2)) = stdDev;
            }
            catch {   }
        }

        #endregion
    }
}