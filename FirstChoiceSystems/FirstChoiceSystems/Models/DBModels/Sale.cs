using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models.DBModels
{
    public class Sale : Entity
    {
        public BusinessUser Seller { get; set; }
        public ICollection<BusinessUser> Buyers { get; set; }    
        public TransactionStatus Status { get; set;}
        public double SaleAmount { get; set; }
        public Item ItemSold { get; set; }
        public DateTime? DateSold { get; set; }
    }
}