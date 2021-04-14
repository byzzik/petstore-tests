namespace PetStoreTests.Models
{
    using Newtonsoft.Json;

    public class ApiResponse
    {
        #region Properties

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        #endregion
    }
}
