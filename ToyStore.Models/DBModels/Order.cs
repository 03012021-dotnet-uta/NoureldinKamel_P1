using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToyStore.Models.Abstracts;
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
        /// The unique id of the Order
        /// </summary>
        /// <returns></returns>
        [Key]
        public Guid OrderId { get; set; } = new Guid();

        /// <summary>
        /// The Set of Items that are included in the order <br/>
        /// A stack contains the type of items and their count
        /// the cart is chosen by a customer to purchase it
        /// </summary>
        /// <value></value>
        public HashSet<SellableStack> cart { get; set; }

        /// <summary>
        /// The location at which the order is purchased from <br/>
        /// </summary>
        /// <value></value>
        public Location OrderLocation { get; set; }

        /// <summary>
        /// The last date that an order was updated.
        /// If an order is checkout out, the date should get updated also.
        /// </summary>
        /// <value></value>
        public DateTime OrderDate { get; set; }
    }
}