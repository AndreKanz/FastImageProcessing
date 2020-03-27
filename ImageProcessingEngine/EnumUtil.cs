using System;

namespace ImageProcessingEngine
{
    internal static class EnumUtil
    {
        #region Methods

        internal static TEnum Convert<TEnum>(string text)
            where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }

        #endregion
    }
}