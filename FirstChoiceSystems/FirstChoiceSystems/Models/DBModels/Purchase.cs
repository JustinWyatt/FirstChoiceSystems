using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models
{
    public enum TransactionStatus
    {
        Pending = 0,
        Approved = 1,
        Voided = 2
    }
   

    public class PurchaseItem : Entity
    {
        public virtual Business Buyer { get; set; }
        public virtual Item Item { get; set; }
        public virtual int QuanityBought { get; set; }
        public virtual decimal PricePerUnitBoughtAt { get; set; }
        public virtual TransactionStatus Status { get; set; }
        public DateTime? ApprovalDate { get; set; }

    }
}