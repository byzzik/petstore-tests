namespace PetStoreTests.Models
{
    using Newtonsoft.Json;

    public class Category
    {
        #region Properties

        [JsonProperty("id")]
        public ulong? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion
    }
}
