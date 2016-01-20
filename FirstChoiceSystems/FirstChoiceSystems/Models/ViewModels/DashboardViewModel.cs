using FirstChoiceSystems.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models.DBModels
{
    public class DashboardViewModel : Entity
    {
        public int AccountNumber { get; set; }
        public string PersonOfContact { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Postal { get; set; }
        public string CompanyWebsite { get; set; }
        public double? Balance { get; set; }
        public string BusinessCategory { get; set; }
        public string CompanyPhoto { get; set; }
        public string RepresentativePhoto { get; set; }
        public string    DateRegistered { get; set; }
        public int? InventoryNumber { get; set; }
        public double? InventoryValue { get; set; }
        public double? SalesFigure { get; set; }
        public int? MembersInArea { get; set; }
        public IEnumerable<PurchaseItemViewModel> PendingSales { get; set; }
        public IEnumerable<MarketPlaceItemViewModel> ItemsUpForSale { get; set; }
        public IEnumerable<PurchaseItemViewModel> Purchases { get; set; }
    }
}