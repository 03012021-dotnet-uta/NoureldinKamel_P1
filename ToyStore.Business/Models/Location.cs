using System.Collections.Generic;
using ToyStore.Business.Interfaces;

namespace ToyStore.Business.Models
{
    public class Location
    {
        public string LocationName { get; set; }
        public Dictionary<Sellable, int> LocationInventory { get; set; }
    }
}