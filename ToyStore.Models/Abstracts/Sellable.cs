using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToyStore.Models.DBModels;
using ToyStore.Models.Interfaces;

namespace ToyStore.Models.Abstracts
{
    /// <summary>
    /// A Sellable can be added by a customer to an order
    /// It is an item that can be sold to a customer
    /// </summary>
    public class Sellable : ISellable
    {
        /// <summary>
        /// The unique id of the Sellable Item
        /// </summary>
        /// <returns></returns>
        [Key]
        public Guid SellableId { get; set; } = new Guid();

        /// <value>SellableName: the name of the item to sell</value>
        public string SellableName { get; set; }

        /// <value>SellableDescription: the description of the item to sell</value>
        public string SellableDescription { get; set; }

        /// <value>SellablePrice: the price of the item to sell</value>
        public double SellablePrice { get; set; }

        /// <summary>
        /// The tag list that this sellable item belongs to
        /// </summary>
        /// <value></value>
        public List<Tag> TagList { get; set; }

        /// <summary>
        /// The path for the image to display for the 
        /// sellable item
        /// </summary>
        /// <value></value>
        public string SellableImagePath { get; set; }
    }
}