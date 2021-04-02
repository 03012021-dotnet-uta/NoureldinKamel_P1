using System;
using System.Collections.Generic;
using ToyStore.Models.DBModels;

namespace ToyStore.Models.ControllerModels
{
#nullable enable
    public class OrderDeleteModel
    {
        public List<Guid>? removedStacks { get; set; }
        public Token? token { get; set; }

        // public override string ToString()
        // {
        //     string s = "OrderModel:\n";
        //     s += "UpdateStack: " + UpdateStack.SellableStackId + " " + UpdateStack.location.LocationName + " --> " + UpdateStack.Item.SellableName;
        //     s += "UpdateCount: " + UpdateCount;
        //     s += "newOrder: " + newOrder;
        //     s += "removedStacks: " + removedStacks;
        //     s += "addedStacks: " + addedStacks;
        //     s += "countChangedStacks: " + countChangedStacks;
        //     s += "token: " + token.TokenValue;
        //     return "UpdateStack: ";
        // }
    }
}