using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "App.config", Watch = true)]

namespace harjoitus
{
    public class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            log.Debug("Aloitetaan");
            readAppConfig();
            BuildWebHost(args).Run();
        }

/// <summary>
/// Read values from app.config to Constants class
/// </summary>
public static void readAppConfig()
{
    var connectionStringSettings = ConfigurationManager.ConnectionStrings["BirdDBConnectionString"];
    if (connectionStringSettings!=null)
    {
        Constants.ConnectionString=connectionStringSettings.ConnectionString;
    }    
    else 
    {
        log.Error("ConnectionString could not be read!");
    }
    Constants.FirstBird = ConfigurationManager.AppSettings["FirstBird"];
    Constants.SecondtBird = ConfigurationManager.AppSettings["SecondBird"];


}





        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                // .ConfigureLogging((hostingContext, logging) =>
                // {
                //     logging.AddConfiguration(hostingContext.Configuration.GetSection("log4net"));
                //     logging.AddConsole();
                //     logging.AddDebug();
                // })
                .UseStartup<Startup>()
                .Build();
    }
}
