namespace PetStoreTests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Client;

    using Fixtures;

    using FluentAssertions;

    using Models;

    using TestCases;

    using Xunit;

    public class PetsTests : IClassFixture<ServiceFixture>, IAsyncDisposable
    {
        #region Fields

        private static readonly Pet _defaultPetModel;
        public static TheoryData<PetsTestCase> GetPetByStatusTestCases;
        public static TheoryData<PetsTestCase> UpdatePetTestCases;

        private readonly IPetStoreClient _client;
        private ulong? _createdPetId;

        #endregion

        #region Constructors

        public PetsTests(ServiceFixture fixture)
        {
            _client = fixture.PetStoreClient ?? throw new ArgumentNullException(nameof(PetStoreClient));
        }

        static PetsTests()
        {
            _defaultPetModel = new Pet()
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
                                  PhotoUrls = new[] { "Test Photo URL" }
                              };
            GetPetByStatusTestCases = new TheoryData<PetsTestCase>()
                                      {
                                          new PetsTestCase()
                                          {
                                              Pet = _defaultPetModel
                                          },
                                          new PetsTestCase()
                                          {
                                              Pet = new Pet()
                                                    {
                                                        Name = "Test pet name",
                                                        Status = PetStatus.pending,
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
                                                        PhotoUrls = new[] { "Test Photo URL" }
                                                    }
                                          },
                                          new PetsTestCase()
                                          {
                                              Pet = new Pet()
                                                    {
                                                        Name = "Test pet name",
                                                        Status = PetStatus.sold,
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
                                                        PhotoUrls = new[] { "Test Photo URL" }
                                                    }
                                          }
                                      };
            UpdatePetTestCases = new TheoryData<PetsTestCase>
                                 {
                                     new PetsTestCase
                                     {
                                         Pet = _defaultPetModel,
                                         UpdatedPet = new Pet
                                                      {
                                                          Name = "Updated test pet name",
                                                          Status = PetStatus.pending,
                                                          Category = new Category()
                                                                     {
                                                                         Id = 123456,
                                                                         Name = "Updated test categoty"
                                                                     },
                                                          Tags = new[]
                                                                 {
                                                                     new Tag()
                                                                     {
                                                                         Id = 987654,
                                                                         Name = "Updated test Tag"
                                                                     }
                                                                 },
                                                          PhotoUrls = new[] { "Updated test Photo URL" }
                                                      }
                                     }
                                 };
        }

        #endregion

        #region Methods

        [Fact]
        public async Task AddPetTest()
        {
            Pet actualPet = await _client.AddPet(_defaultPetModel);

            actualPet.Should().BeEquivalentTo(_defaultPetModel, options => options.Excluding(o=> o.Id));

            _createdPetId = actualPet.Id;
        }

        [Fact]
        public async Task GetPetByIdTest()
        {
            Pet testPet = await _client.AddPet(_defaultPetModel);
            Pet actualPet = await _client.GetPetById(testPet.Id);

            actualPet.Should().BeEquivalentTo(_defaultPetModel, options => options.Excluding(o=>o.Id));
            actualPet.Should().BeEquivalentTo(testPet);

            _createdPetId = testPet.Id;
        }

        [Fact]
        public async Task GetNonExistedPetByIdTest()
        {
            Pet actualPet = await _client.GetPetById(0);

            actualPet.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(GetPetByStatusTestCases))]
        public async Task GetPetByStatusTest(PetsTestCase testCase)
        {
            Pet testPet = await _client.AddPet(testCase.Pet);
            _createdPetId = testPet.Id;
            List<Pet> petsList = await _client.GetPetByStatus(testCase.Pet.Status);

            petsList.Count.Should().BeGreaterThan(0);
            foreach (Pet pet in petsList)
                pet.Status.Should().Be(testCase.Pet.Status);
        }

        [Theory]
        [MemberData(nameof(UpdatePetTestCases))]
        public async Task UpdatePetTest(PetsTestCase testCase)
        {
            Pet testPet = await _client.AddPet(testCase.Pet);
            testCase.UpdatedPet.Id = testPet.Id;
            Pet updatedPet = await _client.UpdatePet(testCase.UpdatedPet);

            updatedPet.Should().BeEquivalentTo(testCase.UpdatedPet);

            _createdPetId = testPet.Id;
        }

        [Fact]
        public async Task UpdatePetFormTest()
        {
            const string UPDATED_NAME = "Updated name";
            const PetStatus UPDATED_STATUS = PetStatus.sold;
            Pet testPet = await _client.AddPet(_defaultPetModel);
            _createdPetId = testPet.Id;

            await _client.UpdatePet(testPet.Id, UPDATED_NAME, UPDATED_STATUS);

            Pet actualPet = await _client.GetPetById(testPet.Id);

            actualPet.Status.Should().Be(UPDATED_STATUS);
            actualPet.Name.Should().Be(UPDATED_NAME);
        }

        [Fact]
        public async Task UpdateNonExistedPetFormTest()
        {
            ApiResponse updateResponse = await _client.UpdatePet(0, "Test Name", PetStatus.available);
            updateResponse.Code.Should().Be(404);
            updateResponse.Message.Should().Be("not found");
        }

        [Fact]
        public async Task UpdateNonExistedPetTest()
        {
            Pet actualPet = await _client.UpdatePet(_defaultPetModel);
            _createdPetId = actualPet.Id;

            actualPet.Should().BeEquivalentTo(_defaultPetModel, options => options.Excluding(o=>o.Id));
        }

        [Fact]
        public async Task DeletePetTest()
        {
            Pet actualPet = await _client.AddPet(_defaultPetModel);
            _createdPetId = actualPet.Id;

            ApiResponse deletePetResponse = await _client.DeletePet(actualPet.Id);

            Pet deletedPet = await _client.GetPetById(actualPet.Id);

            deletePetResponse.Code.Should().Be(200);
            deletedPet.Should().BeNull();
        }

        public async ValueTask DisposeAsync()
        {
            await _client.DeletePet(_createdPetId);
        }

        #endregion
    }
}
