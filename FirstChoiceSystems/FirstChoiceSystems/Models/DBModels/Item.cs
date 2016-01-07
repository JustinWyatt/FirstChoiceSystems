using System.Collections.Generic;

namespace FirstChoiceSystems.Models.DBModels
{
    public class Item : Entity
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double PricePerUnit { get; set; }
        public virtual ICollection<ItemImage> Images { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public int UnitsAvailable { get; set; }
        public virtual BusinessUser Seller { get; set; }

        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}