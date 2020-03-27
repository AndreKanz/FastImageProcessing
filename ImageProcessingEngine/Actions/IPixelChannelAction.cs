namespace ImageProcessingEngine.Actions
{
    public unsafe interface IPixelChannelAction
    {
        #region Methods

        void Process(byte* imgPtr, int i, ARGBChannel channel);

        #endregion
    }
}