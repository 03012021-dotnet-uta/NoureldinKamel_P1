using System.Collections.Generic;
using ToyStore.Models.Interfaces;

namespace ToyStore.Models.DBModels
{
    public class Product : Sellable
    {
        public string SellableName { get; set; }
        public double SellablePrice { get; set; }
        public string SellableDescription { get; set; }

        public List<Tag> TagList { get; set; }

        public override bool Equals(object obj)
        {
            return this.SellableName.Equals(((Sellable)obj).SellableName);
        }
    }
}