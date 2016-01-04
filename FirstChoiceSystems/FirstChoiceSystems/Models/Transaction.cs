using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Business Seller { get; set; }
        public Business Buyer { get; set; }
        public int Amount { get; set; }
        public DateTime DateOfTransaction { get; set;}
        public DateTime? DateOfApproval { get; set; }
        public string Description { get; set; }
    }
}