using log4net;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AzureLoggingApplicationInsights
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static ILog Logger { get; set; }
        protected void Application_Start()
        {

            // TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings["applicationInsights"];            
            TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = true;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Getting a specific appender (works).
            log4net.Config.XmlConfigurator.Configure();          // Can configure an FileInfo object (file)
            Logger = log4net.LogManager.GetLogger("aiAppender"); // this.GetType()

            // see MobileManager
            /*
             * Global.asax                      LoggingConfiguration.Configure();
             * LoggingConfiguration.cs          public static void Configure(ILoggingConfigurator inputLog4NetConfigurator = null, IConfigurationParameterStore inputStore = null)
             *                                  <add key="LogConfigurationFile" value="log4Net.internal.config" />
             * LoggingConfigurator.cs           public void Configure(string filename)
             *                                  XmlConfigurator.Configure(new FileInfo(filePath));
             *                                  --> Once XmlConfigurator.Configure() is called the logging object is available.
            */

            /* Other Information:
             * In addition to the answer from @JohnGardner, you can instead add a line to your AssemblyInfo.cs file as so: -
             * [assembly: log4net.Config.XmlConfigurator(Watch = true)]
            */
        }
    }
}
