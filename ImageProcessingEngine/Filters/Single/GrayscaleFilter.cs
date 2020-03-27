using ImageProcessingEngine.Actions;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Single
{
    [SecurityCritical]
    public class GrayscaleFilter : ISinglePixelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i)
        {
            try
            {
                var avg = (byte)(*(imgPtr + i) * 0.114
                               + *(imgPtr + (i + 1)) * 0.587
                               + *(imgPtr + (i + 2)) * 0.299);

                *(imgPtr + i) = avg;
                *(imgPtr + (i + 1)) = avg;
                *(imgPtr + (i + 2)) = avg;
            }
            catch {   }
        }

        #endregion
    }
}