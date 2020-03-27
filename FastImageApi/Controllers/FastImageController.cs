using ImageProcessingEngine;
using System.Threading.Tasks;
using System.Web.Http;

namespace FastImageApi.Controllers
{
    public class FastImageController : ApiController
    {
        #region Methods

        // GET api/FastImage
        public async Task<string> Get([FromBody] EngineCommandDto commandDto)
        {
            return commandDto == null 
                ?  string.Empty 
                :  await Task.Run(() => new ProcessorRunner().Run(commandDto));
        }

        #endregion
    }
}