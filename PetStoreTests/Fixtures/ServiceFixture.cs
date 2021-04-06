namespace PetStoreTests.Fixtures
{
    using System;

    using Client;

    using Infrastructure;

    using Microsoft.Extensions.DependencyInjection;

    public class ServiceFixture
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider = ServiceProviderConfigurator.CreateServiceProvider();

        #endregion

        public IPetStoreClient PetStoreClient => _serviceProvider.GetRequiredService<IPetStoreClient>();
    }
}
