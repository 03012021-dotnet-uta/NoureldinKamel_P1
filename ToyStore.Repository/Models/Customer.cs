using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ToyStory.Repository.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; } = new Guid();
        public string CustomerFName { get; set; }
        public string CustomerLName { get; set; }
        public string CustomerPass { get; set; }
        public string CustomerUsername { get; set; }
        // public List<Order> FinishedOrders { get; set; }

    }
}