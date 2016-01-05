using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models
{
    public class Inventory : Entity
    {
        public virtual Business Business { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}