using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models.ViewModels
{
    //Todo change name to "Market Place Item"
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string ItemDescription { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public string Seller { get; set; }
        public virtual ICollection<ItemImage> Images { get; set; }
        public string Category { get; set; }
    }
}