using FirstChoiceSystems.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceSystems.Models
{
    public class OrderViewModel
    {
        public static OrderViewModel Retrieve()
        {
            var order = (OrderViewModel)HttpContext.Current.Session["currentOrder"];

            if (order == null)
            {
                order = new OrderViewModel();
                HttpContext.Current.Session["currentCart"] = order;
            }

            return order;
        }

        public void Save()
        {
            HttpContext.Current.Session["currentOrder"] = this;
        }


        public ICollection<ItemViewModel> Items { get; set; }
        public double SubTotal
        {
            get
            {
                return Items.Sum(x => x.Price * x.Quantity);
            }
        }


    }


}