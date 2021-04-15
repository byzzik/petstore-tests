namespace PetStoreTests.Tests
{
    using System;
    using System.Threading.Tasks;

    using Client;

    using Fixtures;

    using FluentAssertions;

    using Models;

    using TestCases;

    using Xunit;

    public class PetStoreTests : IClassFixture<ServiceFixture>, IAsyncDisposable
    {
        #region Fields

        public static TheoryData<AddPetTestCase> AddPetTestCases;

        private readonly IPetStoreClient _client;
        private ulong? createdPetId;

        #endregion

        #region Constructors

        public PetStoreTests(ServiceFixture fixture)
        {
            _client = fixture.PetStoreClient ?? throw new ArgumentNullException(nameof(PetStoreTests));
        }

        static PetStoreTests()
        {
            AddPetTestCases = new TheoryData<AddPetTestCase>
                              {
                                  new AddPetTestCase
                                  {
                                      Pet = new Pet
                                            {
                                                Name = "iwrestled",
                                                Status = PetStatus.available,
                                                Category = new Category()
                                                           {
                                                               Id = 999999999,
                                                               Name = "abearonce"
                                                           },
                                                Tags = new[]
                                                       {
                                                           new Tag()
                                                           {
                                                               Id = 888888888,
                                                               Name = "Test Tag"
                                                           }
                                                       },
                                                PhotoUrls = new string[] { "Test Photo URL" }
                                            }
                                  }
                              };
        }

        #endregion

        #region Methods

        [Theory]
        [MemberData(nameof(AddPetTestCases))]
        public async Task AddPetTests(AddPetTestCase testCase)
        {
            Pet actualPet = await _client.AddPet(testCase.Pet);

            actualPet.Should().BeEquivalentTo(testCase.Pet, options => options.Excluding(o=> o.Id));

            createdPetId = actualPet.Id;
        }

        public async ValueTask DisposeAsync()
        {
            await _client.DeletePet(createdPetId);
        }

        #endregion
    }
}
