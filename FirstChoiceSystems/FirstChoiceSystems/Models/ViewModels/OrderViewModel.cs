using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FirstChoiceSystems.Models.ViewModels
{
    public class OrderViewModel
    {
        public static OrderViewModel Retrieve()
        {
            var order = (OrderViewModel)HttpContext.Current.Session["currentOrder"];

            if (order == null)
            {
                order = new OrderViewModel();
                HttpContext.Current.Session["currentOrder"] = order;
                order.Items = new List<MarketPlaceItemViewModel>();
            }

            return order;
        }

        public void Save()
        {
            HttpContext.Current.Session["currentOrder"] = this;
        }

        public ICollection<MarketPlaceItemViewModel> Items { get; set; }

        public double? SubTotal
        {
            get
            {
                return Items.Sum(x => x.Price * x.Quantity);
            }
        }

        public double? Tax => (SubTotal / 9.3).HasValue ? (double?)Math.Round((SubTotal / 9.3).Value, 2) : null;

        public double? BrokerFee => (SubTotal / 7.5).HasValue ? (double?)Math.Round((SubTotal / 7.5).Value, 2)  : null;

        public double? Total => SubTotal + Tax + BrokerFee;
    }
}