using DevelWoutACause.OotStateExtractor.Common;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DevelWoutACause.OotStateExtractor.Service
{
    public class Server {
        public static void Start(EventWithLatest<SaveContext> latestSaveContext) {
            Start(latestSaveContext, new string[] { });
        }

        public static void Start(EventWithLatest<SaveContext> latestSaveContext, string[] args) {
            CreateWebHostBuilder(args).ConfigureServices((serviceCollection) => {
                serviceCollection.AddScoped((serviceProvider) => latestSaveContext);
                serviceCollection.AddMvcCore().AddJsonOptions((options) => {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });
            }).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
