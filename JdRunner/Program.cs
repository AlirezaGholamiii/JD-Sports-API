using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
           .UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
                       .ReadFrom.Configuration(hostingContext.Configuration)
                       .Enrich.FromLogContext()
                       .WriteTo.Console()
                       .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
            )
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
                   string envUrls = Environment.GetEnvironmentVariable("J_D_Runner_ENV_URLS", EnvironmentVariableTarget.Machine);
                   if (String.IsNullOrEmpty(envUrls))
                   {
                       throw new Exception("Undefined System Environment Variable(J_D_Runner_ENV_URLS)");
                   }
                   string[] urls = envUrls.Split(',');

                   webBuilder.UseUrls(urls);
               }).UseWindowsService();
    }
}
