using FirstChoiceSystems.Models.ViewModels;
using System;
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
        public double Balance { get; set; }
        public string BusinessCategory { get; set; }
        public string DateRegistered { get; set; }
        public IEnumerable<ItemViewModel> ItemsUpForSale { get; set; }
        public IEnumerable<PurchaseItem> Purchases { get; set; }
    }
}