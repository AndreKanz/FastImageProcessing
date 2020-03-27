using Contracts.Dto;
using ImageProcessingEngine.Commands;
using ImageProcessingEngine.Processors;
using System.Drawing;

namespace ImageProcessingEngine
{
    public class ProcessorRunner
    {
        #region Methods

        public void Run(string source, string path, string targetPath)
        {
            using (var image = Image.FromFile(path) as Bitmap)
            {
                new CommandSetProcessor(
                    new CommandSetParser(),
                    new ImageProcessorRunner())
                .ProcessImage(source, image);

                image.Save(targetPath);
            }
        }

        public string Run(IEngineCommandDto commandDto)
        {
            using (var image = commandDto.Base64Image.FromBase64() as Bitmap)
            {
                new CommandSetProcessor(
                    new CommandSetParser(),
                    new ImageProcessorRunner())
                .ProcessImage(commandDto.CommandString, image);

                return image.ToBase64();
            }
        }

        #endregion
    }
}