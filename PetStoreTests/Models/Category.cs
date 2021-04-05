namespace PetStoreTests.Models
{
    using System.Text.Json.Serialization;

    public class Category
    {
        #region Properties

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        #endregion
    }
}
