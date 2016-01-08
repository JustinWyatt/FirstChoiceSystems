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
                order.Items = new List<ItemViewModel>();
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