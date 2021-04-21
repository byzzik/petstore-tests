namespace PetStoreTests.Configuration
{
    public class PetStoreClientConfiguration
    {
        #region Properties

        public string ApiKey { get; set; }
        public string ApiVersion { get; set; }
        public string BaseUrl { get; set; }
        public string PetRoute { get; set; }
        public string OrderRoute { get; set; }
        public string StoreRoute { get; set; }
        public string InventoryRoute { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        #endregion
    }
}
