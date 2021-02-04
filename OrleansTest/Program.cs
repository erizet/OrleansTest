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
        private static string _connectionString =
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OrleansTest;Integrated Security=True;Pooling=False;Max Pool Size=200;MultipleActiveResultSets=True";

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
                    .UseLocalhostClustering();

                    //siloBuilder.AddMemoryGrainStorage("counter");

                    //                    siloBuilder.AddAdoNetGrainStorageAsDefault(options =>
                    siloBuilder.AddAdoNetGrainStorage("counter", options =>
                    {
                        options.Invariant = "System.Data.SqlClient";
                        options.ConnectionString = _connectionString;
                        options.UseJsonFormat = true;
                    });
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                });
    }
}
