using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orleans.Hosting;
using Grains;

namespace OrleansTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseOrleans(siloBuilder =>
                {
                    siloBuilder
                    //.ConfigureApplicationParts(apm => apm.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                    .UseLocalhostClustering();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                });
    }
}
