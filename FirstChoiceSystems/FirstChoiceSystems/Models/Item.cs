using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemDescription { get; set; }
        public int Price { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateSold { get; set; }
        public ICollection<ItemImage> Images { get; set; }
        public ItemCategory ItemCategory { get; set; }
    }
}