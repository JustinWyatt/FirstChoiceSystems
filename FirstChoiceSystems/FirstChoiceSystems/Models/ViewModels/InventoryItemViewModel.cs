using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models.ViewModels
{
    public class InventoryItemViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double? PricePerUnit { get; set; }
        public string ItemCategory { get; set; }
        public int UnitsAvailable { get; set; }
        public string Seller { get; set; }
        public bool AvailableForMarket { get; set; }
        public double? CashCost { get; set; }
        public double? RevenueInCash { get; set; }
        public double? RevenueInTradeDollars { get; set; }
        public double? CashEquivalentValue { get; set; }
        public string ItemImage { get; set; }
    }
}