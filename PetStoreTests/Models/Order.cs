namespace PetStoreTests.Models
{
    using System;
    using System.Text.Json.Serialization;

    public class Order
    {
        #region Properties

        [JsonPropertyName("complete")]
        public bool Complete { get; set; }

        [JsonPropertyName("id")]
        public ulong? Id { get; set; }

        [JsonPropertyName("petId")]
        public ulong? PetId { get; set; }

        [JsonPropertyName("quantity")]
        public ulong? Quantity { get; set; }

        [JsonPropertyName("shipDate")]
        public DateTime ShipDate { get; set; }

        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }

        #endregion
    }
}
