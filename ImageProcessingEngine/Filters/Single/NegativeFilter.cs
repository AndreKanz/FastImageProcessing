using ImageProcessingEngine.Actions;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Single
{
    [SecurityCritical]
    internal class NegativeFilter : ISinglePixelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i)
        {
            try
            {
                *(imgPtr + i) = (byte)(255 - *(imgPtr + i));
                *(imgPtr + (i + 1)) = (byte)(255 - *(imgPtr + (i + 1)));
                *(imgPtr + (i + 2)) = (byte)(255 - *(imgPtr + (i + 2)));
            }
            catch {   }
        }

        #endregion
    }
}