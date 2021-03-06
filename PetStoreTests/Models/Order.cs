using System;
using Newtonsoft.Json;

namespace PetStoreTests.Models
{
    public class Order
    {
        #region Properties

        [JsonProperty("complete")] public bool Complete { get; set; }

        [JsonProperty("id")] public ulong? Id { get; set; }

        [JsonProperty("petId")] public ulong? PetId { get; set; }

        [JsonProperty("quantity")] public long Quantity { get; set; }

        [JsonProperty("shipDate")] public DateTimeOffset ShipDate { get; set; }

        [JsonProperty("status")] public OrderStatus Status { get; set; }

        #endregion
    }
}