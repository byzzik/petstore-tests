namespace PetStoreTests.Models
{
    using System.Text.Json.Serialization;

    public class Pet
    {
        #region Properties

        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("id")]
        public ulong? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("photoUrls")]
        public string[] PhotoUrls { get; set; }

        [JsonPropertyName("status")]
        public PetStatus Status { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

        #endregion
    }
}
