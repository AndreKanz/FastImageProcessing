namespace ImageProcessingEngine.Actions
{
    public unsafe interface ISinglePixelAction
    {
        #region Methods

        void Process(byte* imgPtr, int i);

        #endregion
    }
}