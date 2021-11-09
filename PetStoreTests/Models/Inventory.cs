using Newtonsoft.Json;

namespace PetStoreTests.Models
{
    public class Inventory
    {
        [JsonProperty("sold")] public long Sold { get; set; }

        [JsonProperty("not")] public long Not { get; set; }

        [JsonProperty("string")] public long String { get; set; }

        [JsonProperty("Nonavailable")] public long Nonavailable { get; set; }

        [JsonProperty("yes")] public long Yes { get; set; }

        [JsonProperty("pending")] public long Pending { get; set; }

        [JsonProperty("available")] public long Available { get; set; }
    }
}