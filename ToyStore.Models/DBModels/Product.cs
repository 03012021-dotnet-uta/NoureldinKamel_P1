using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToyStore.Models.Abstracts;

namespace ToyStore.Models.DBModels
{
    public class Product : Sellable
    {
        // public Offer CurrentOffer { get; set; }

        public override bool Equals(object obj)
        {
            return this.SellableName.Equals(((Sellable)obj).SellableName);
        }
    }
}