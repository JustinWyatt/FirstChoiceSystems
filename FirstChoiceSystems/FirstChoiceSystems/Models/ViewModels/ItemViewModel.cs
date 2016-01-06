using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string ItemDescription { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}