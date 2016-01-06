using FirstChoiceSystems.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models
{
    public class Purchase : Entity
    {
        public List<PurchaseItem> PurchaseItems { get; set; }
    }

    public enum TransactionStatus
    {
        Pending = 0,
        Approved = 1,
        Voided = 2
    }
   

    
}