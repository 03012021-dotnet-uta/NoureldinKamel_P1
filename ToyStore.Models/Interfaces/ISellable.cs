using System;
using System.Collections.Generic;
using ToyStore.Models.DBModels;

namespace ToyStore.Models.Interfaces
{
    /// <summary>
    /// A Sellable can be added by a customer to an order
    /// It is an item that can be sold to a customer
    /// </summary>
    public interface ISellable
    {
        /// <summary>
        /// The unique id of the Sellable Item
        /// </summary>
        /// <returns></returns>
        Guid SellableId { get; set; }

        /// <value>SellableName: the name of the item to sell</value>
        string SellableName { get; set; }

        /// <value>SellableDescription: the description of the item to sell</value>
        string SellableDescription { get; set; }

        /// <value>SellablePrice: the price of the item to sell</value>
        double SellablePrice { get; set; }

        /// <summary>
        /// The tag list that this sellable item belongs to
        /// </summary>
        /// <value></value>
        List<Tag> TagList { get; set; }

        /// <summary>
        /// The path for the image to display for the 
        /// sellable item
        /// </summary>
        /// <value></value>
        string SellableImagePath { get; set; }
    }
}