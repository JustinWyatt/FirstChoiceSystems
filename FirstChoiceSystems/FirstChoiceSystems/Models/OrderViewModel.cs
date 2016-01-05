using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models
{
    public class OrderViewModel 
    {
        public int Id { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}