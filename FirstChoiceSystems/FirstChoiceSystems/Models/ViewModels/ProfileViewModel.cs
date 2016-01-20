using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using FirstChoiceSystems.Models.DBModels;

namespace FirstChoiceSystems.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string PersonOfContact { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Postal { get; set; }
        public string CompanyWebsite { get; set; }
        public double Balance { get; set; }
        public virtual string BusinessCategory { get; set; }
        public int AccountNumber { get; set; }
        public string DateRegistered { get; set; }
        public virtual ICollection<MarketPlaceItemViewModel> ItemsUpForSale { get; set; } = new Collection<MarketPlaceItemViewModel>();
        public string Photo { get; set; }
    }
}