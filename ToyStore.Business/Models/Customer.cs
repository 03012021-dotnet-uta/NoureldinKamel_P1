using System.Collections.Generic;
using ToyStore.Business.Interfaces;

namespace ToyStore.Business.Models
{
    /// <summary>
    /// A user which buys from a location
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// First name of the customer
        /// </summary>
        /// <value></value>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the customer
        /// </summary>
        /// <value></value>
        public string LastName { get; set; }

        /// <summary>
        /// Orders checked out or confirmed
        /// by the customer
        /// </summary>
        /// <value></value>
        public List<Order> FinishedOrders { get; set; }
    }
}