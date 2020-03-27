using System;
using System.Drawing;

namespace ImageProcessingEngine
{
    public static class ImageExtensions
    {
        #region Methods

        public static string ToBase64(this Image image)
        {
            if (image == null)
                return string.Empty;

            return Convert.ToBase64String(
                image.ToBytes());
        }

        public static Image FromBase64(this string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return null;

            var bytes = Convert.FromBase64String(base64String);
            if (bytes == null)
                return null;

            return bytes.ToImage();
        }

        public static Image ToImage(this byte[] bytes)
        {
            return new ImageConverter()
                .ConvertFrom(bytes) as Image;
        }

        public static byte[] ToBytes(this Image image)
        {
            return (byte[])new ImageConverter()
                .ConvertTo(image, typeof(byte[]));
        }

        #endregion
    }
}