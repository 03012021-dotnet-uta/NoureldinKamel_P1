using System.Collections.Generic;
using ToyStore.Business.Interfaces;

namespace ToyStore.Business.Models
{
    public class Offer : Sellable
    {
        public string SellableName { get; set; }
        public double SellablePrice { get; set; }

        public List<Product> Products { get; set; }
        public string SellableDescription { get; set; }

        public override bool Equals(object obj)
        {
            return this.SellableName.Equals(((Sellable)obj).SellableName);
        }
    }
}