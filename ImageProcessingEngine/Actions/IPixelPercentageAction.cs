namespace ImageProcessingEngine.Actions
{
    public unsafe interface IPixelPercentageAction
    {
        #region Methods

        void Process(byte* imgPtr, int i, double percentage);

        #endregion
    }
}