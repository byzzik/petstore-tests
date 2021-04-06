namespace PetStoreTests.Infrastructure
{
    using System;

    using Client;

    using Configuration;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class ServiceProviderConfigurator
    {
        #region Methods

        public static IServiceProvider CreateServiceProvider()
        {
            IConfiguration configuration = ConfigurationFactory.CreateConfiguration();
            var services = new ServiceCollection();

            services.AddOptions().Configure<PetStoreClientConfiguration>(configuration.GetSection(nameof(PetStoreClientConfiguration)));

            services.AddSingleton<IPetStoreClient, PetStoreClient>();

            return services.BuildServiceProvider();
        }

        #endregion
    }
}
