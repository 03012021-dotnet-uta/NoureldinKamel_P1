using System;
using System.Collections.Generic;
using ToyStore.Models.Interfaces;

namespace ToyStore.Models.DBModels
{
    /// <summary>
    /// The order that a customer chooses
    /// It can be called a cart
    /// has multiple items that a customer wishes to pay for or purchase
    /// or has already purchased previously
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The item dictionary that are included in the order
        /// contains the type of items and their count
        /// the cart is chosen by a customer to purchase it
        /// </summary>
        /// <value></value>
        public Dictionary<Sellable, int> cart { get; set; }

        /// <summary>
        /// The location at which the order is purchased from <br/>
        /// The location 
        /// </summary>
        /// <value></value>
        public Location OrderLocation { get; set; }
        public Customer OrderCustomer { get; set; }
        public DateTime OrderDate { get; set; }
    }
}