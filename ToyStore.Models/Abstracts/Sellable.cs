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

        // /// <summary>
        // /// The SellableTag list that this sellable item belongs to this Item. <br/>
        // /// The Sellable Tag object is a junction entity that connects this item to a tag.
        // /// </summary>
        // /// <value></value>
        // public List<SellableTag> SellableTags { get; set; }

        /// <summary>
        /// The tags that this sellable belongs to
        /// </summary>
        /// <value></value>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// The path for the image to display for the 
        /// sellable item
        /// </summary>
        /// <value></value>
        public string SellableImagePath { get; set; }
    }
}