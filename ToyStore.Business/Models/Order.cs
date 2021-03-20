using System;
using System.Collections.Generic;
using ToyStore.Business.Interfaces;

namespace ToyStore.Business.Models
{
    public class Order
    {
        public Dictionary<Sellable, int> cart { get; set; }
        public Location OrderLocation { get; set; }
        public Customer OrderCustomer { get; set; }
        public DateTime OrderDate { get; set; }
    }
}