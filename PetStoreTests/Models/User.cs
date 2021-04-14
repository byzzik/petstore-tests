namespace PetStoreTests.Models
{
    using Newtonsoft.Json;

    public class User
    {
        #region Properties

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("id")]
        public ulong? Id { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("userStatus")]
        public ulong? UserStatus { get; set; }

        #endregion
    }
}
