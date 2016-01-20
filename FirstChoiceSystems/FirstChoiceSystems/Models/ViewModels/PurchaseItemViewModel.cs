using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstChoiceSystems.Models.DBModels;

namespace FirstChoiceSystems.Models.ViewModels
{
    public class PurchaseItemViewModel 
    {
        public  string Buyer { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Seller { get; set; }
        public  int QuanityBought { get; set; }
        public  double? Price { get; set; }
        public virtual string Status { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime DatePurchased { get; set; }
    }
}