using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models
{
    public class Inventory : Entity
    {
        public virtual BusinessUser Owner { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}