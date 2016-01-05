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
        public static OrderViewModel OrderInstance { get; set; }

        static OrderViewModel()
        {
            if (HttpContext.Current.Session["currentOrder"] == null)
            {
                OrderInstance = new OrderViewModel();
                OrderInstance.Items = new List<Item>();
                HttpContext.Current.Session["currentOrder"] = OrderInstance;
            }
            else
            {
                OrderInstance = (OrderViewModel)HttpContext.Current.Session["currentOrder"];
            }
        }

        protected OrderViewModel() { }
        
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public void SaveCart(OrderViewModel order)
        {
            HttpContext.Current.Session["currentOrder"] = order;
        }

        public OrderViewModel GetOrders()
        {
            var order = (OrderViewModel)HttpContext.Current.Session["currentOrder"];

            if (order == null)
            {
                order = new OrderViewModel();
                HttpContext.Current.Session["currentCart"] = order;
            }

            return order;
        }

        public decimal GetSubTotal()
        {
            decimal subTotal = 0;
            foreach (Item item in Items)
                subTotal += item.Price;

            return subTotal;
        }
    }
}