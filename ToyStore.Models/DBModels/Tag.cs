using System;
using System.ComponentModel.DataAnnotations;
using ToyStore.Models.Interfaces;

namespace ToyStore.Models.DBModels
{
    /// <summary>
    /// The Tag that categorizes different items.
    /// An Item can have multiple tags that it belongs to
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// The unique Id of the tag
        /// </summary>
        /// <returns></returns>
        [Key]
        public Guid TagId { get; set; } = new Guid();

        /// <summary>
        /// The tag name that would normally identify a tag
        /// this should be unique
        /// </summary>
        public string TagName;
    }
}