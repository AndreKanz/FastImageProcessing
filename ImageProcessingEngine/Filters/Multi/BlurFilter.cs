using ImageProcessingEngine.Actions;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Multi
{
    [SecurityCritical]
    internal class BlurFilter : IMultiPixelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, byte* beforePtr, byte* afterPtr, int i, int before, int after)
        {
            try
            {
                var avgR = (byte)((*(imgPtr + (before + 2)) + *(imgPtr + (after + 2)) + *(beforePtr + (i + 2)) + *(afterPtr + (i + 2))) / 4);
                var avgG = (byte)((*(imgPtr + (before + 1)) + *(imgPtr + (after + 1)) + *(beforePtr + (i + 1)) + *(afterPtr + (i + 1))) / 4);
                var avgB = (byte)((*(imgPtr + before) + *(imgPtr + after) + *(beforePtr + i) + *(afterPtr + i)) / 4);

                *(imgPtr + i) = avgB;
                *(imgPtr + (i + 1)) = avgG;
                *(imgPtr + (i + 2)) = avgR;
            }
            catch {   }
        }

        #endregion
    }
}