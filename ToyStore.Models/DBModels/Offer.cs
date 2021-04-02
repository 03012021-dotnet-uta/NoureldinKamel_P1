using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToyStore.Models.Abstracts;

namespace ToyStore.Models.DBModels
{
    /// <summary>
    /// The offer which combines one or more product
    /// extends sellable because it can be sold
    /// </summary>
    public class Offer : Sellable
    {
        /// <summary>
        /// The list of products included in this offer
        /// </summary>
        /// <value></value>
        // public List<Product> Products { get; set; }

        public override bool Equals(object obj)
        {
            return this.SellableName.Equals(((Sellable)obj).SellableName);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}