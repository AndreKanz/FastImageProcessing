namespace ImageProcessingEngine
{
    internal static class FilterMode
    {
        #region Constants

        #region Single

        internal const string Blue = "blue";
        internal const string Red = "red";
        internal const string Green = "green";
        internal const string Grayscale = "grayscale";
        internal const string Negative = "negative";
        internal const string Variance = "variance";
        internal const string StdDev = "stddev";

        #endregion

        #region Multi

        internal const string Blur = "blur";
        internal const string FuzzyGrayscale = "fuzzygrayscale";

        #endregion

        #region Channel

        internal const string CMax = "cmax";
        internal const string CInvert = "cinvert";
        internal const string CZero = "czero";
        internal const string CAverage = "caverage";
        internal const string CVariance = "cvariance";
        internal const string CStdDev = "cstddev";

        #endregion

        #region Percentage

        internal const string Light = "light";
        internal const string Dark = "dark";

        #endregion

        #endregion
    }
}