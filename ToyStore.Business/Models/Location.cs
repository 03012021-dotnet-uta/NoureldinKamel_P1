using System.Collections.Generic;
using ToyStore.Business.Interfaces;

namespace ToyStore.Business.Models
{
    /// <summary>
    /// Location in which a branch of the store exists
    /// </summary>
    public class Location
    {
        /// <summary>
        /// The name of the location of the store
        /// </summary>
        /// <value></value>
        public string LocationName { get; set; }

        /// <summary>
        /// The types of the sellable items
        /// and then the number of items that exist at the store
        /// </summary>
        /// <value></value>
        public Dictionary<Sellable, int> LocationInventory { get; set; }
    }
}