namespace ToyStore.Business.Interfaces
{
    public interface Sellable
    {
        string SellableName { get; set; }
        string SellableDescription { get; set; }
        double SellablePrice { get; set; }
    }
}