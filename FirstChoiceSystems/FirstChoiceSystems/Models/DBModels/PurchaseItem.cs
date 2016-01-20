using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models.DBModels
{
    public class PurchaseItem 
    {
        public int Id { get; set; }
        public virtual BusinessUser Buyer { get; set; }
        public virtual Item Item { get; set; }
        public virtual int QuanityBought { get; set; }
        public virtual double? PricePerUnitBoughtAt { get; set; }
        public virtual TransactionStatus Status { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime DatePurchased { get; set; }
    }
}