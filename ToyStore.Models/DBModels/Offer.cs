using System.Collections.Generic;
using ToyStore.Models.Interfaces;

namespace ToyStore.Models.DBModels
{
    /// <summary>
    /// The offer which combines one or more product
    /// extends sellable because it can be sold
    /// </summary>
    public class Offer : Sellable
    {
        /// <summary>
        /// The name of the offer
        /// </summary>
        /// <value></value>
        public string SellableName { get; set; }

        /// <summary>
        /// The total price of the offer
        /// </summary>
        /// <value></value>
        public double SellablePrice { get; set; }

        /// <summary>
        /// The products that are included in the offer
        /// </summary>
        /// <value></value>
        public List<Product> Products { get; set; }

        /// <summary>
        /// The long description describing the offer
        /// </summary>
        /// <value></value>
        public string SellableDescription { get; set; }
        public List<Tag> TagList { get; set; }

        public override bool Equals(object obj)
        {
            return this.SellableName.Equals(((Sellable)obj).SellableName);
        }
    }
}