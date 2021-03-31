using System.Collections.Generic;
using ToyStore.Models.DBModels;

namespace ToyStore.Models.ControllerModels
{
    public class OrderModel
    {
        public SellableStack UpdateStack { get; set; }
        public int UpdateCount { get; set; }
        public Order newOrder { get; set; }
        public List<SellableStack> removedStacks { get; set; }
        public List<SellableStack> addedStacks { get; set; }
        public List<SellableStack> countChangedStacks { get; set; }
        public Token token { get; set; }

        public override string ToString()
        {
            string s = "OrderModel:\n";
            s += "UpdateStack: " + UpdateStack.SellableStackId + " " + UpdateStack.location.LocationName + " --> " + UpdateStack.Item.SellableName;
            s += "UpdateCount: " + UpdateCount;
            s += "newOrder: " + newOrder;
            s += "removedStacks: " + removedStacks;
            s += "addedStacks: " + addedStacks;
            s += "countChangedStacks: " + countChangedStacks;
            s += "token: " + token.TokenValue;
            return "UpdateStack: ";
        }
    }
}