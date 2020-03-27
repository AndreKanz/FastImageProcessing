using ImageProcessingEngine.Actions;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Percentage
{
    [SecurityCritical]
    internal class DarkFilter : IPixelPercentageAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i, double percentage)
        {
            try
            {
                *(imgPtr + i) = (byte)(*(imgPtr + i) * (1 - percentage));
                *(imgPtr + (i + 1)) = (byte)(*(imgPtr + (i + 1)) * (1 - percentage));
                *(imgPtr + (i + 2)) = (byte)(*(imgPtr + (i + 2)) * (1 - percentage));
            }
            catch {   }
        }

        #endregion
    }
}