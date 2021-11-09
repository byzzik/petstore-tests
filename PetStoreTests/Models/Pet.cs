using Newtonsoft.Json;

namespace PetStoreTests.Models
{
    public class Pet
    {
        #region Properties

        [JsonProperty("category")] public Category Category { get; set; }

        [JsonProperty("id")] public ulong? Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("photoUrls")] public string[] PhotoUrls { get; set; }

        [JsonProperty("status")] public PetStatus Status { get; set; }

        [JsonProperty("tags")] public Tag[] Tags { get; set; }

        #endregion
    }
}