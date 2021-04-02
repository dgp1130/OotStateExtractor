using DevelWoutACause.OotStateExtractor.Common;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevelWoutACause.OotStateExtractor.Service
{
    public class Server {
        public static void Start(LatestEmission<SaveContext> latestSaveContext) {
            Start(latestSaveContext, new string[] { });
        }

        public static void Start(LatestEmission<SaveContext> latestSaveContext, string[] args) {
            CreateWebHostBuilder(args).ConfigureServices((serviceCollection) => {
                serviceCollection.AddScoped((serviceProvider) => latestSaveContext.Value);
            }).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
