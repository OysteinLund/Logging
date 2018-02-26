using log4net;
using log4net.Config;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace AzureLoggingApplicationInsights.Controllers
{
    public class ValuesController : ApiController
    {
        public static ILog Logger { get; set; }

        public ValuesController()
        {
            var Logger = log4net.LogManager.GetLogger("aiAppender"); // this.GetType()                                   
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            // https://jan-v.nl/post/using-application-insights-in-your-log4net-application
            /*
            var tc = new TelemetryClient();

            var log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            XmlConfigurator.Configure();
            tc.TrackEvent("Some event (7)");                            // WORKS

            log.Info("Some informational message. (7)");                // FAILS
            log.Warn("Some warning message (7)");
            log.Error("Some error message (7)");
            log.Debug("Some debug message. (7)");

            tc.Flush();
            */
            var Logger = log4net.LogManager.GetLogger("aiAppender"); // this.GetType()                                   
            Logger.Info(String.Format("Info - api/values/{0}",id.ToString()));
            Logger.Warn(String.Format("Warn - api/values/{0}", id.ToString()));
            Logger.Error(String.Format("Error - api/values/{0}", id.ToString()));            

            var fileLogger = log4net.LogManager.GetLogger("RollingFileAppender");
            fileLogger.Info(String.Format("Info - api/values/{0}", id.ToString()));
            fileLogger.Warn(String.Format("Warn - api/values/{0}", id.ToString()));
            fileLogger.Error(String.Format("Error - api/values/{0}", id.ToString()));

            TelemetryClient telemetryClient = new TelemetryClient();
            var tm = new TraceTelemetry("Trace component call", SeverityLevel.Information);
            var dic = new Dictionary<string, string>();
            dic.Add("Post", DateTime.Now.ToString());
            telemetryClient.TrackTrace("Forgot password url", dic);


            var telemetry = new TraceTelemetry("Trace component call", SeverityLevel.Verbose);
            telemetry.Properties.Add("component", "componentName");
            telemetry.Properties.Add("method", "methodname");
            telemetry.Properties.Add("properties", "propertyvalue");
            telemetry.Properties.Add("guid", Guid.NewGuid().ToString());            

            telemetryClient.TrackTrace(telemetry);



            // - Works 
            /*
            TelemetryClient telemetryClient = new TelemetryClient();
            var telemetry = new TraceTelemetry("Trace component call", SeverityLevel.Verbose);
            telemetry.Properties.Add("component", "componentName");
            telemetry.Properties.Add("method", "method");            
            telemetry.Properties.Add("properties", "properties");

            telemetryClient.TrackTrace(telemetry);
            */

            return String.Format("value {0}",id);
            
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
