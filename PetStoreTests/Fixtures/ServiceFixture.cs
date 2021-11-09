using System;
using Microsoft.Extensions.DependencyInjection;
using PetStoreTests.Client;
using PetStoreTests.Infrastructure;

namespace PetStoreTests.Fixtures
{
    public class ServiceFixture
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider = ServiceProviderConfigurator.CreateServiceProvider();

        #endregion

        public IPetStoreClient PetStoreClient => _serviceProvider.GetRequiredService<IPetStoreClient>();
    }
}