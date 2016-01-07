using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstChoiceSystems.Models.DBModels;

namespace FirstChoiceSystems.Models.ViewModels
{
    public class PurchaseItemViewModel 
    {
        public virtual string Buyer { get; set; }
        public virtual Item Item { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Seller { get; set; }
        public virtual int QuanityBought { get; set; }
        public virtual double Price { get; set; }
        public virtual string Status { get; set; }
        public DateTime? ApprovalDate { get; set; }

    }
}