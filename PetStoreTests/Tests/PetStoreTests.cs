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

    public class PetStoreTests : IClassFixture<ServiceFixture>, IAsyncDisposable
    {
        #region Fields

        public static Pet DefaultPetModel;
        public static TheoryData<PetsTestCase> AddPetTestCases;
        public static TheoryData<PetsTestCase> GetPetByIdTestCases;
        public static TheoryData<PetsTestCase> GetPetByStatusTestCases;
        public static TheoryData<PetsTestCase> UpdatePetTestCases;

        private readonly IPetStoreClient _client;
        private ulong? _createdPetId;

        #endregion

        #region Constructors

        public PetStoreTests(ServiceFixture fixture)
        {
            _client = fixture.PetStoreClient ?? throw new ArgumentNullException(nameof(PetStoreTests));
        }

        static PetStoreTests()
        {
            DefaultPetModel = new Pet()
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
                              };
            AddPetTestCases = new TheoryData<PetsTestCase>
                              {
                                  new PetsTestCase
                                  {
                                      Pet = DefaultPetModel
                                  }
                              };
            GetPetByIdTestCases = new TheoryData<PetsTestCase>
                              {
                                  new PetsTestCase
                                  {
                                      Pet = DefaultPetModel
                                  }
                              };
            GetPetByStatusTestCases = new TheoryData<PetsTestCase>()
                                      {
                                          new PetsTestCase()
                                          {
                                              Pet = DefaultPetModel
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
                                                        PhotoUrls = new string[] { "Test Photo URL" }
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
                                                        PhotoUrls = new string[] { "Test Photo URL" }
                                                    }
                                          }
                                      };
            UpdatePetTestCases = new TheoryData<PetsTestCase>
                                 {
                                     new PetsTestCase
                                     {
                                         Pet = DefaultPetModel,
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
                                                          PhotoUrls = new string[] { "Updated test Photo URL" }
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

            _createdPetId = actualPet.Id;
        }

        [Theory]
        [MemberData(nameof(GetPetByIdTestCases))]
        public async Task GetPetByIdTest(PetsTestCase testCase)
        {
            Pet testPet = await _client.AddPet(testCase.Pet);
            Pet actualPet = await _client.GetPetById(testPet.Id);

            actualPet.Should().BeEquivalentTo(testCase.Pet, options => options.Excluding(o=>o.Id));
            actualPet.Should().BeEquivalentTo(testPet);

            _createdPetId = testPet.Id;
        }

        [Theory]
        [MemberData(nameof(GetPetByStatusTestCases))]
        public async Task GetPetByStatusTest(PetsTestCase testCase)
        {
            Pet testPet = await _client.AddPet(testCase.Pet);
            List<Pet> petsList = await _client.GetPetByStatus(testCase.Pet.Status);

            petsList.Count.Should().BeGreaterThan(0);
            foreach (Pet pet in petsList)
                pet.Status.Should().Be(testCase.Pet.Status);

            _createdPetId = testPet.Id;
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

        public async ValueTask DisposeAsync()
        {
            await _client.DeletePet(_createdPetId);
        }

        #endregion
    }
}
