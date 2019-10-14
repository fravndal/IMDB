using Microsoft.Extensions.Configuration;
using System.IO;

namespace Persistence
{
    public class DbConnection
    {
        public string GetDbString()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddUserSecrets<IMDBDbContext>()
               .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            var settingsConfig = new SettingsConfig();
            configuration.GetSection("MySettings").Bind(settingsConfig);

            return configuration.GetConnectionString("IMDBDb");
        }
    }
}
