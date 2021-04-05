namespace PetStoreTests.Models
{
    using System;

    public class Order
    {
        #region Properties

        public bool Complete { get; set; }
        public int Id { get; set; }
        public int PetId { get; set; }
        public int Quantity { get; set; }
        public DateTime ShipDate { get; set; }
        public OrderStatus Status { get; set; }

        #endregion
    }
}
