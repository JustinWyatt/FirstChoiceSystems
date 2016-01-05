using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models
{
    public enum TransactionStatus
    {
        Pending =0,
        Approved =1,
        Voided =2,
    }
    public class Transaction : Entity
    {
        public Business Seller { get; set; }
        public Business Buyer { get; set; }
        public int Amount { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Description { get; set; }
        public TransactionStatus Status { get; set; }
    }
}