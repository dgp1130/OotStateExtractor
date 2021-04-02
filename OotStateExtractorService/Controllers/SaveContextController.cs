using DevelWoutACause.OotStateExtractor.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevelWoutACause.OotStateExtractor.Service.Controllers {
    [Route("api/v1/save-context")]
    [ApiController]
    public class SaveContextController : ControllerBase {
        /** Returns the current `SaveContext` serialized as JSON. */
        [HttpGet]
        public ActionResult<SaveContext> Get([FromServices] SaveContext saveContext) {
            Response.ContentType = "application/json";
            return saveContext;
        }
    }
}
