namespace PetStoreTests.Infrastructure
{
    using System.IO;

    using Microsoft.Extensions.Configuration;

    public class ConfigurationFactory
    {
        #region Methods

        public static IConfiguration CreateConfiguration()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json", false, true).AddEnvironmentVariables().Build();
            return configuration;
        }

        #endregion
    }
}
