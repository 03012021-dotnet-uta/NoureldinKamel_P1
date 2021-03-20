using System.Collections.Generic;
using ToyStore.Business.Interfaces;

namespace ToyStore.Business.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Order> FinishedOrders { get; set; }
    }
}