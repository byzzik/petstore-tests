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

    public class PetStoreTests : IClassFixture<ServiceFixture>
    {
        #region Fields

        public static TheoryData<AddPetTestCase> AddPetTestCases;

        private readonly IPetStoreClient _client;

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
                                                Name = "testpetname",
                                                Status = PetStatus.available,
                                                Category = new Category()
                                                           {
                                                               Name = "testcategoryname"
                                                           }
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

            //actualPet.Name.Should().Be(testCase.Pet.Name);
            //actualPet.Status.Should().Be(testCase.Pet.Status);

            await _client.DeletePet(actualPet.Id);
        }

        #endregion
    }
}
