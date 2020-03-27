using Contracts.ImageProcessingEngine.Commands;
using Contracts.ImageProcessingEngine.Processors;
using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessingEngine.Processors
{
    public class ImageProcessorRunner : IImageProcessor
    {
        #region Fields

        private static readonly IDictionary<string, IImageProcessor> Processors =
            new Dictionary<string, IImageProcessor>
            {
                { ProcessorMode.Single,     new SingleProcessor()     },
                { ProcessorMode.Multi,      new MultiProcessor()      },
                { ProcessorMode.Channel,    new ChannelProcessor()    },
                { ProcessorMode.Percentage, new PercentageProcessor() },
                { ProcessorMode.Binary,     new BinaryProcessor()     }
            };

        #endregion

        #region Methods

        public void Process(Bitmap imgSource, IProcessorCommand command)
        {
            Processors[command.Mode]
                .Process(imgSource, command);
        }

        #endregion
    }
}