using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NLog;

namespace Kufar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GlobalDiagnosticsContext.Set("configDir", "C:\\Logs");
            GlobalDiagnosticsContext.Set("connectionString", "Server=GODYLA;Database=SiteKufar1;Trusted_Connection=True; ");



            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
            
                BuildWebHost(args).Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                //logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
              .ConfigureLogging(logging =>
              {
                  logging.ClearProviders();
                  logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
              })
                .UseNLog()
                .Build();
    }
}
