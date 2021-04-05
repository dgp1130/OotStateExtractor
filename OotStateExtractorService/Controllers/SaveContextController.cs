using DevelWoutACause.OotStateExtractor.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevelWoutACause.OotStateExtractor.Service.Controllers {
    [ApiController]
    public class SaveContextController : ControllerBase {
        /** Returns the current `SaveContext` serialized as JSON. */
        [Route("api/v1/save-context/get")]
        [HttpGet]
        public ActionResult<SaveContext> Get(
            [FromServices] EventWithLatest<SaveContext> saveContext
        ) {
            Response.ContentType = "application/json";
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return saveContext.Value;
        }

        [Route("api/v1/save-context/stream")]
        [HttpGet]
        public async Task Stream(
            [FromServices] EventWithLatest<SaveContext> saveContext,
            CancellationToken cancellationToken
        ) {
            Response.ContentType = "text/event-stream";
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            EventHandler<SaveContext> onEmit = async (sender, saveCtx) => {
                var serialized = JsonConvert.SerializeObject(saveCtx, new JsonSerializerSettings {
                    Formatting = Formatting.Indented,
                });
                var data = string.Join("\n", serialized.Split('\n').Select((line) => $"data: {line}"));
                await Response.WriteAsync($"{data}\n\n");
                await Response.Body.FlushAsync();
            };

            // Send current state to client immediately.
            onEmit(saveContext, saveContext.Value);

            // Emit changes to client as they happen.
            saveContext.OnEmit += onEmit;

            // Hold connection until client cancels the request.
            while (!cancellationToken.IsCancellationRequested) {
                await Task.Delay(100);
            }

            // Stop listening for changes.
            saveContext.OnEmit -= onEmit;
        }
    }
}
