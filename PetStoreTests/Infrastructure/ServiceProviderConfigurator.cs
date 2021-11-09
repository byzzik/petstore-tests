using System;
using Microsoft.Extensions.DependencyInjection;
using PetStoreTests.Client;
using PetStoreTests.Configuration;

namespace PetStoreTests.Infrastructure
{
    public class ServiceProviderConfigurator
    {
        #region Methods

        public static IServiceProvider CreateServiceProvider()
        {
            var configuration = ConfigurationFactory.CreateConfiguration();
            var services = new ServiceCollection();

            services.AddOptions()
                .Configure<PetStoreClientConfiguration>(configuration.GetSection(nameof(PetStoreClientConfiguration)));

            services.AddHttpClient<IPetStoreClient, PetStoreClient>();

            return services.BuildServiceProvider();
        }

        #endregion
    }
}