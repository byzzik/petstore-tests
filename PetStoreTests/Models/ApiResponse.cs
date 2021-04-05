namespace PetStoreTests.Models
{
    using System.Text.Json.Serialization;

    public class ApiResponse
    {
        #region Properties

        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        #endregion
    }
}
