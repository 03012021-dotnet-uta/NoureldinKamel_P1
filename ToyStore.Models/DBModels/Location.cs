using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToyStore.Models.Abstracts;
using ToyStore.Models.Interfaces;

namespace ToyStore.Models.DBModels
{
    /// <summary>
    /// Location in which a branch of the store exists
    /// </summary>
    public class Location
    {
        /// <summary>
        /// The id that uniquely identifies a location
        /// </summary>
        /// <returns></returns>
        [Key]
        public Guid LocationId { get; set; } = new Guid();

        /// <summary>
        /// The name of the location of the store
        /// </summary>
        /// <value></value>
        public string LocationName { get; set; }

        /// <summary>
        /// Stores different types of the sellable items and their count. <br/>
        /// It is a set so that a Sellable cannot be repeated twice in 2 stacks.
        /// </summary>
        /// <value></value>
        public HashSet<SellableStack> LocationInventory { get; set; }
    }
}