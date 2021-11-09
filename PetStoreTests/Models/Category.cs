using Newtonsoft.Json;

namespace PetStoreTests.Models
{
    public class Category
    {
        #region Properties

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        #endregion
    }
}