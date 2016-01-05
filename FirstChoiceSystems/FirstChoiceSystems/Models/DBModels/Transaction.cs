﻿using System;
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
        public virtual Business Seller { get; set; }
        public virtual Business Buyer { get; set; }
        public double Amount { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Description { get; set; }
        public virtual TransactionStatus Status { get; set; }
    }
}