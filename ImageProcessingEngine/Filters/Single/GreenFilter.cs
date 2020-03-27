﻿using ImageProcessingEngine.Actions;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ImageProcessingEngine.Filters.Single
{
    [SecurityCritical]
    internal class GreenFilter : ISinglePixelAction
    {
        #region Methods

        [HandleProcessCorruptedStateExceptions]
        public unsafe void Process(byte* imgPtr, int i)
        {
            try
            {
                *(imgPtr + i) = 0;
                *(imgPtr + (i + 2)) = 0;
            }
            catch {   }
        }

        #endregion
    }
}