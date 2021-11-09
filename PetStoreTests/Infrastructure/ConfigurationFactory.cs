using System.IO;
using Microsoft.Extensions.Configuration;

namespace PetStoreTests.Infrastructure
{
    public class ConfigurationFactory
    {
        #region Methods

        public static IConfiguration CreateConfiguration()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true).AddEnvironmentVariables().Build();
            return configuration;
        }

        #endregion
    }
}