namespace PetStoreTests.Models
{
    public class Pet
    {
        #region Properties

        public Category Category { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] PhotoUrls { get; set; }
        public PetStatus Status { get; set; }
        public string[] Tags { get; set; }

        #endregion
    }
}
