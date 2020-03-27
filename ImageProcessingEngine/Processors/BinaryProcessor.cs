using Contracts.ImageProcessingEngine.Commands;
using Contracts.ImageProcessingEngine.Processors;
using ImageProcessingEngine.Commands;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading.Tasks;

namespace ImageProcessingEngine.Processors
{
    [SecurityCritical]
    internal unsafe class BinaryProcessor : IImageProcessor
    {
        #region Methods

        public void Process(Bitmap imgSource, IProcessorCommand command)
        {
            var histogram = GetGrayscaleHistogram(imgSource);
            var median = CalculateMedian(histogram).ToArgb();
            var data = imgSource.GetBitmapData();
            var bof = data.GetBof();
            var pixelBytes = imgSource.GetPixelBytes();
            var byteCount = pixelBytes * data.Width;

            Parallel.For(0, data.Height, y =>
            {
                var currentPointer = bof + y * data.Stride;
                for (var x = 0; x < byteCount; x += pixelBytes)
                    Process(currentPointer, x, median);
            });
            imgSource.UnlockBits(data);
        }

        private static IDictionary<Color, int> GetGrayscaleHistogram(Bitmap imgSource)
        {
            using (var copy = (Bitmap)imgSource.Clone())
            {
                new SingleProcessor()
                    .Process(copy, new ProcessorCommand(ProcessorMode.Single, FilterMode.Grayscale));

                return new HistogramProcessor()
                    .Process(copy);
            }
        }

        private static Color CalculateMedian(IDictionary<Color, int> histogram)
        {
            var data = GetOrderedValues(histogram);
            if (data.Length % 2 == 0)
            {
                var median = GetMedian(histogram, data);
                var next = histogram.GetKey(data[data.Length / 2 - 1]);
                return Color.FromArgb(median.R + next.R, median.G + next.G, median.B + next.B);
            }
            else
                return GetMedian(histogram, data);
        }

        private static int[] GetOrderedValues(IDictionary<Color, int> histogram)
        {
            return histogram
                .Values
                .OrderBy(x => x)
                .ToArray();
        }

        private static Color GetMedian(IDictionary<Color, int> histogram, int[] data)
        {
            return histogram.GetKey(data[data.Length / 2]);
        }

        [HandleProcessCorruptedStateExceptions]
        private static unsafe void Process(byte* imgPtr, int i, int median)
        {
            try
            {
                var color = GetPixelColor(imgPtr, i);
                if (color > median)
                    SetPixel(imgPtr, i, 255);
                else
                    SetPixel(imgPtr, i, 0);
            }
            catch {   }
        }

        private static int GetPixelColor(byte* imgPtr, int i)
        {
            return Color.FromArgb(
                *(imgPtr + (i + 2)),
                *(imgPtr + (i + 1)),
                *(imgPtr + i))
                .ToArgb();
        }

        private static void SetPixel(byte* imgPtr, int i, byte value)
        {
            *(imgPtr + i) = value;
            *(imgPtr + (i + 1)) = value;
            *(imgPtr + (i + 2)) = value;
        }

        #endregion
    }
}