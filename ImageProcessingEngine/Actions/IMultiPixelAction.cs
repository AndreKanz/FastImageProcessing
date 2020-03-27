namespace ImageProcessingEngine.Actions
{
    public unsafe interface IMultiPixelAction
    {
        #region Methods

        void Process(byte* imgPtr, byte* beforePtr, byte* afterPtr, int i, int before, int after);

        #endregion
    }
}