using System;
using System.ComponentModel.DataAnnotations;
using ToyStore.Models.Abstracts;

namespace ToyStore.Models.DBModels
{
    /// <summary>
    /// 
    /// </summary>
    public class SellableTag
    {
        public Tag TagType { get; set; }
        public Sellable SellableItem { get; set; }
    }
}