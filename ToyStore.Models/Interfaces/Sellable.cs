using System.Collections.Generic;
using ToyStore.Models.DBModels;

namespace ToyStore.Models.Interfaces
{
    /// <summary>
    /// A Sellable can be added by a customer to an order
    /// It is an item that can be sold to a customer
    /// </summary>
    public interface Sellable
    {
        /// <value>SellableName: the name of the item to sell</value>
        string SellableName { get; set; }
        /// <value>SellableDescription: the description of the item to sell</value>
        string SellableDescription { get; set; }
        /// <value>SellablePrice: the price of the item to sell</value>
        double SellablePrice { get; set; }

        List<Tag> TagList { get; set; }
    }
}