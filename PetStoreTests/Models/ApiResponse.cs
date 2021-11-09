using Newtonsoft.Json;

namespace PetStoreTests.Models
{
    public class ApiResponse
    {
        #region Properties

        [JsonProperty("code")] public long Code { get; set; }

        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        #endregion
    }
}