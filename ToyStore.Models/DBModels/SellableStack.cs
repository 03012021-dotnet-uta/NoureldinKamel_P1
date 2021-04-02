using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToyStore.Models.Abstracts;

#nullable enable
namespace ToyStore.Models.DBModels
{
    /// <summary>
    /// This method is a solution for stacking multiple items. <br/>
    /// Whenever a customer adds an item into his order cart, it is stored as a SellableStack. <br/>
    /// Or for the Location an inventory is made up of SellableStacks. <br/>
    /// And a cart or an inventory would not have the same Sellable Stack more than once.
    /// </summary>
    public class SellableStack
    {
        /// <summary>
        /// The Id of the Sellable stack that uniquely identifies it.
        /// </summary>
        /// <returns></returns>
        [Key]
        public Guid SellableStackId { get; set; } = new Guid();

        /// <summary>
        /// The Item type that this stack consists of.
        /// </summary>
        /// <value></value>
        public Sellable Item { get; set; }

        /// <summary>
        /// The number of the same Sellable items in this stack.
        /// </summary>
        /// <value></value>
        public int Count { get; set; }

        /// <summary>
        /// The location if this stack is an inventory of a store
        /// if not the store is null;
        /// </summary>
        /// <value></value>
        public Location? location { get; set; }
        public Guid? locationId { get; set; }

        /// <summary>
        /// The order that this stack is in if it is in a currentorder
        /// or in a finished order of a customer
        /// </summary>
        /// <value></value>
        public Order? order { get; set; }
        public Guid? orderId { get; set; }

        public bool PurchasedInOrder { get; set; }

        public List<Tag> GetTags()
        {
            return Item.Tags;
        }
    }
}