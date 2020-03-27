using ImageProcessingEngine;
using ImageProcessingEngine.Commands;
using ImageProcessingEngine.Processors;

namespace FastImageProcessingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new ProcessorRunner().Run("config.txt", "nussknacker.jpg", "result.jpg");
        }
    }
}
