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

        public static TheoryData<PetsTestCase> AddPetTestCases;
        public static TheoryData<PetsTestCase> GetPetTestCases;

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
            AddPetTestCases = new TheoryData<PetsTestCase>
                              {
                                  new PetsTestCase
                                  {
                                      Pet = new Pet
                                            {
                                                Name = "Test pet name",
                                                Status = PetStatus.available,
                                                Category = new Category()
                                                           {
                                                               Id = 999999999,
                                                               Name = "Test categoty"
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
            GetPetTestCases = new TheoryData<PetsTestCase>
                              {
                                  new PetsTestCase
                                  {
                                      Pet = new Pet
                                            {
                                                Name = "Test pet name",
                                                Status = PetStatus.available,
                                                Category = new Category()
                                                           {
                                                               Id = 999999999,
                                                               Name = "Test categoty"
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
        public async Task AddPetTest(PetsTestCase testCase)
        {
            Pet actualPet = await _client.AddPet(testCase.Pet);

            actualPet.Should().BeEquivalentTo(testCase.Pet, options => options.Excluding(o=> o.Id));

            createdPetId = actualPet.Id;
        }

        [Theory]
        [MemberData(nameof(GetPetTestCases))]
        public async Task GetPetTest(PetsTestCase testCase)
        {
            Pet testPet = await _client.AddPet(testCase.Pet);
            Pet actualPet = await _client.GetPet(testPet.Id);

            actualPet.Should().BeEquivalentTo(testCase.Pet, options => options.Excluding(o=>o.Id));
            actualPet.Should().BeEquivalentTo(testPet);
            createdPetId = testPet.Id;
        }



        public async ValueTask DisposeAsync()
        {
            await _client.DeletePet(createdPetId);
        }

        #endregion
    }
}
