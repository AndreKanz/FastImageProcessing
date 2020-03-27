using Contracts.ImageProcessingEngine.Commands;
using Contracts.ImageProcessingEngine.Processors;
using System.Drawing;

namespace ImageProcessingEngine
{
    public class CommandSetProcessor
    {
        #region Fields

        private readonly ICommandSetParser parser;
        private readonly IImageProcessor processor;

        #endregion

        #region Constructors

        public CommandSetProcessor(ICommandSetParser parser, IImageProcessor processor)
        {
            this.parser = parser;
            this.processor = processor;
        }

        #endregion

        #region Methods

        public void ProcessImage(string source, Bitmap image)
        {
            var commandSet = parser.GetCommandSet(source);
            foreach (var command in commandSet)
                processor.Process(image, command);
        }

        #endregion
    }
}
