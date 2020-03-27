using ImageProcessingEngine.Actions;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Multi
{
    [SecurityCritical]
    internal class FuzzyGrayscaleFilter : IMultiPixelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, byte* beforePtr, byte* afterPtr, int i, int before, int after)
        {
            try
            {
                var rangeAvg = (byte)((*(imgPtr + i) + *(imgPtr + (i + 1)) + *(imgPtr + (i + 1)) + *(imgPtr + (i + 2)) + *(imgPtr + after) + *(imgPtr + (after + 1)) + *(imgPtr + (after + 2)) + *(imgPtr + before) + *(imgPtr + (before + 1)) + *(imgPtr + (before + 2))
                    + *(beforePtr + i) + *(beforePtr + (i + 1)) + *(beforePtr + (i + 2)) + *(beforePtr + after) + *(beforePtr + (after + 1)) + *(beforePtr + (after + 2)) + *(beforePtr + before) + *(beforePtr + (before + 1)) + *(beforePtr + (before + 2))
                    + *(afterPtr + i) + *(afterPtr + (i + 1)) + *(afterPtr + (i + 2)) + *(afterPtr + after) + *(afterPtr + (after + 1)) + *(afterPtr + (after + 2)) + *(afterPtr + before) + *(afterPtr + (before + 1)) + *(afterPtr + (before + 2))) / 27);

                *(imgPtr + i) = rangeAvg;
                *(imgPtr + (i + 1)) = rangeAvg;
                *(imgPtr + (i + 2)) = rangeAvg;
            }
            catch {   }
        }

        #endregion
    }
}