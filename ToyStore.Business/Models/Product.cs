using ToyStore.Business.Interfaces;

namespace ToyStore.Business.Models
{
    public class Product : Sellable
    {
        public string SellableName { get; set; }
        public double SellablePrice { get; set; }
        public string SellableDescription { get; set; }

        public override bool Equals(object obj)
        {
            return this.SellableName.Equals(((Sellable)obj).SellableName);
        }
    }
}