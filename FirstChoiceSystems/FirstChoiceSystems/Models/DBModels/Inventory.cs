using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models
{
    public class Inventory : Entity
    {
        public Business Business { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}