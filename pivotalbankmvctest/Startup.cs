using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Owin;
using System;
using System.Configuration;
using System.IO;

[assembly: OwinStartupAttribute(typeof(pivotalbankmvctest.Startup))]
namespace pivotalbankmvctest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // reset connection string if required
            SetConnectionString();
            
            ConfigureAuth(app);
        }

        /// <summary>
        /// Cannot rely on the default connection string in CF - can use CF services or build the connection string for the app
        /// based on values in env var set by CF
        /// </summary>
        private static void SetConnectionString()
        {
            JObject jsonConfig = null;

            bool local = false;
            if (local)
            {
                // read json from local text file
                var jsonString = string.Empty;
                var filePath = @"C:\Users\begim\Documents\Visual Studio 2017\Projects\pivotalbankmvctest\pivotalbankmvctest\vcap_services.json";

                using (StreamReader file = File.OpenText(filePath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    jsonConfig = (JObject)JToken.ReadFrom(reader);
                }
            }
            else
            {
                // cf will provide in Env var
                var vcapconfig = Environment.GetEnvironmentVariable("VCAP_SERVICES", EnvironmentVariableTarget.Process);
                if (!string.IsNullOrEmpty(vcapconfig))
                {
                    // read and process the json from the CF env var config
                    using (TextReader tr = new StringReader(vcapconfig))
                    using (JsonTextReader reader = new JsonTextReader(tr))
                    {
                        jsonConfig = (JObject)JToken.ReadFrom(reader);
                    }
                    Console.WriteLine("read vcap_services from env var");
                }
                else
                {
                    Console.WriteLine("Oops: Cannot read VCAP_SERVICES from env var");
                }
            }

            string sqlAzureConnectionString = string.Empty;
            if (jsonConfig != null)
            {
                Console.WriteLine("have json from vcap_services from env var, building cnn");
                //build from CF config
                var hostName = jsonConfig["azure-sqldb"][0]["credentials"]["hostname"].Value<string>();
                var dbLogin = jsonConfig["azure-sqldb"][0]["credentials"]["databaseLogin"].Value<string>();
                var dbLoginPassword = jsonConfig["azure-sqldb"][0]["credentials"]["databaseLoginPassword"].Value<string>();
                var dbTcpPort = jsonConfig["azure-sqldb"][0]["credentials"]["port"].Value<string>();
                var dbName = jsonConfig["azure-sqldb"][0]["credentials"]["name"].Value<string>(); //SqlServerName

                sqlAzureConnectionString = $"Server=tcp:{hostName},{dbTcpPort};Database={dbName};User ID ={dbLogin};Password={dbLoginPassword};Trusted_Connection=False;Encrypt=True;";
            }

            // either use the sql cnn we've built or defer to the default connection string in config
            Startup.GlobalDbConnectionString = !string.IsNullOrEmpty(sqlAzureConnectionString)
                                    ? sqlAzureConnectionString
                                    : ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
