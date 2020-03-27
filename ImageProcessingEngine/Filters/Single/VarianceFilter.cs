using ImageProcessingEngine.Actions;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Single
{
    [SecurityCritical]
    internal class VarianceFilter : ISinglePixelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i)
        {
            try
            {
                var avg = (byte)((*(imgPtr + i) + *(imgPtr + (i + 1)) + *(imgPtr + (i + 2))) / 3);
                var variance = (byte)(
                    ((*(imgPtr + i) - avg) * (*(imgPtr + i) - avg)
                    + (*(imgPtr + (i + 1)) - avg) * (*(imgPtr + (i + 1)) - avg)
                    + (*(imgPtr + (i + 2)) - avg) * (*(imgPtr + (i + 2)) - avg))
                    / 3);

                *(imgPtr + i) = variance;
                *(imgPtr + (i + 1)) = variance;
                *(imgPtr + (i + 2)) = variance;
            }
            catch {   }
        }

        #endregion
    }
}