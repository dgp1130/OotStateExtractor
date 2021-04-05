using DevelWoutACause.OotStateExtractor.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DevelWoutACause.OotStateExtractor.Service.Controllers {
    [ApiController]
    public class SaveContextController : ControllerBase {
        /** Returns the current `SaveContext` serialized as JSON. */
        [Route("api/v1/save-context/get")]
        [HttpGet]
        public ActionResult<SaveContext> Get(
            [FromServices] LatestEmission<SaveContext> saveContext
        ) {
            Response.ContentType = "application/json";
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return saveContext.Value;
        }

        [Route("api/v1/save-context/stream")]
        [HttpGet]
        public async Task Stream([FromServices] LatestEmission<SaveContext> saveContext) {
            Response.ContentType = "text/event-stream";
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            while (true) {
                var serialized = JsonConvert.SerializeObject(saveContext.Value, new JsonSerializerSettings {
                    Formatting = Formatting.Indented,
                });
                var data = string.Join("\n", serialized.Split('\n').Select((line) => $"data: {line}"));
                await Response.WriteAsync($"{data}\n\n");
                await Response.Body.FlushAsync();
                await Task.Delay(5000);
            }
        }
    }
}
