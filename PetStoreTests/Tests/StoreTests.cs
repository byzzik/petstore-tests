namespace PetStoreTests.Tests
{
    using System;
    using System.Threading.Tasks;

    using Client;

    using Fixtures;

    using FluentAssertions;

    using Models;

    using Xunit;

    public class StoreTests : IClassFixture<ServiceFixture>, IAsyncDisposable
    {
        #region Fields

        private readonly IPetStoreClient _cliemt;
        private ulong? _creaqtedPetId;
        private ulong? _createdOrderId;
        private static readonly Order _defaultOrderModel;
        private static readonly Pet _defaultPetModel;

        #endregion

        #region Constructors

        public StoreTests(ServiceFixture fixture)
        {
            _cliemt = fixture.PetStoreClient ?? throw new ArgumentNullException(nameof(PetStoreClient));
        }

        static StoreTests()
        {
            _defaultOrderModel = new Order
                                 {
                                     Complete = false,
                                     Quantity = 23,
                                     ShipDate = DateTimeOffset.MaxValue.Date,
                                     Status = OrderStatus.placed
                                 };

            _defaultPetModel = new Pet
                               {
                                   Name = "Test pet name",
                                   Status = PetStatus.available,
                                   Category = new Category
                                              {
                                                  Id = 999999999,
                                                  Name = "Test categoty"
                                              },
                                   Tags = new[]
                                          {
                                              new Tag
                                              {
                                                  Id = 888888888,
                                                  Name = "Test Tag"
                                              }
                                          },
                                   PhotoUrls = new[] { "Test Photo URL" }
                               };
        }

        #endregion

        #region Methods

        [Fact]
        public async Task AddOrderTest()
        {
            Pet testPet = await _cliemt.AddPet(_defaultPetModel);
            _creaqtedPetId = testPet.Id;
            _defaultOrderModel.PetId = testPet.Id;

            Order order = await _cliemt.AddOrder(_defaultOrderModel);
            _createdOrderId = order.Id;

            order.Should().BeEquivalentTo(_defaultOrderModel, options => options.Excluding(o => o.Id));
            order.PetId.Should().Be(testPet.Id);
        }

        [Fact]
        public async Task DeleteOrderTest()
        {
            Order order = await _cliemt.AddOrder(_defaultOrderModel);
            _createdOrderId = order.Id;

            ApiResponse deleteOrderResponse = await _cliemt.DeleteOrder(order.Id);
            deleteOrderResponse.Code.Should().Be(200);

            Order deletedOrder = await _cliemt.GetOrder(order.Id);

            deletedOrder.Should().BeNull();
        }

        [Fact]
        public async Task GetInventoryTest()
        {
            Inventory inventory = await _cliemt.GetInventories();
            inventory.Should().NotBeNull();
        }

        [Fact]
        public async Task GetNonExistedOrderTest()
        {
            await _cliemt.DeleteOrder(1);
            Order order = await _cliemt.GetOrder(1);
            order.Should().BeNull();
        }

        [Fact]
        public async Task GetOrderTest()
        {
            Order order = await _cliemt.AddOrder(_defaultOrderModel);
            _createdOrderId = order.Id;

            Order getOrderresponse = await _cliemt.GetOrder(order.Id);

            getOrderresponse.Should().BeEquivalentTo(order);
        }

        #endregion

        #region IAsyncDisposable

        public async ValueTask DisposeAsync()
        {
            await _cliemt.DeleteOrder(_createdOrderId);
            await _cliemt.DeletePet(_creaqtedPetId);
        }

        #endregion
    }
}
