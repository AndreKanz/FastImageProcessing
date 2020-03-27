using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessingEngine
{
    internal static class BitmapExtensions
    {
        #region Methods

        internal static BitmapData GetBitmapData(this Bitmap image)
        {
            return image
                .LockBits(
                    new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadWrite, image.PixelFormat);
        }

        internal static int GetPixelBytes(this Bitmap imgSource)
        {
            return Image.GetPixelFormatSize(imgSource.PixelFormat) / 8;
        }

        internal unsafe static byte* GetBof(this BitmapData data)
        {
            return (byte*)data.Scan0;
        }

        #endregion
    }
}