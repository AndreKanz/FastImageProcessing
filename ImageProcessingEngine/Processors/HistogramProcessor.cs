using Contracts.ImageProcessingEngine.Processors;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading.Tasks;

namespace ImageProcessingEngine.Processors
{
    [SecurityCritical]
    public unsafe class HistogramProcessor : IHistogramProcessor
    {
        #region Methods

        public IDictionary<Color, int> Process(Bitmap imgSource)
        {
            var data = imgSource.GetBitmapData();
            var bof = data.GetBof();
            var pixelBytes = imgSource.GetPixelBytes();
            var byteCount = pixelBytes * data.Width;
            var histogram = new ConcurrentDictionary<Color, int>();
            Parallel.For(0, data.Height, y =>
            {
                var currentPointer = bof + y * data.Stride;
                for (var x = 0; x < byteCount; x += pixelBytes)
                    ScanPixel(histogram, currentPointer, x);
            });

            imgSource.UnlockBits(data);
            return histogram;
        }

        [HandleProcessCorruptedStateExceptions]
        private static void ScanPixel(ConcurrentDictionary<Color, int> histogram, byte* currentPointer, int x)
        {
            try
            {
                histogram
                    .AddOrUpdate(
                        Color.FromArgb(
                            *(currentPointer + (x + 2)),
                            *(currentPointer + (x + 1)),
                            *(currentPointer + x)),
                        1,
                        (c, n) => n + 1);
            }
            catch {   }
        }

        #endregion
    }
}