using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models.ViewModels
{
    public class PurchaseRequestViewModel
    {
        public int Id { get; set; }
        public DateTime DateOfPurchaseRequest { get; set; }
        public double Total { get; set; }
    }
}