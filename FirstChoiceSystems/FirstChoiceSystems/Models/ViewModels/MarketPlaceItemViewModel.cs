using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using FirstChoiceSystems.Models.DBModels;

namespace FirstChoiceSystems.Models.ViewModels
{
    public class MarketPlaceItemViewModel
    {
        public MarketPlaceItemViewModel(Item x)
        {
            ItemName = x.ItemName;
            ItemDescription = x.ItemDescription;
            ItemId = x.Id;
            Seller = x.Seller.CompanyName;
            Quantity = x.UnitsAvailable;
            Price = x.PricePerUnit;
            Category = x.ItemCategory.CategoryName ?? "";
            Images = x.Images;
        }
        public int ItemId { get; set; }
        public string ItemDescription { get; set; }
        public double? Price { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public string Seller { get; set; }
        public ICollection<ItemImage> Images { get; set; }
        public string Category { get; set; }
    }
}