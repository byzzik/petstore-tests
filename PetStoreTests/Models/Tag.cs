namespace PetStoreTests.Models
{
    using Newtonsoft.Json;

    public class Tag
    {
        #region Properties

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion
    }
}
