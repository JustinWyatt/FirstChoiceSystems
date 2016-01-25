using System.Linq;
using System.Web.Mvc;
using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.ViewModels;

namespace FirstChoiceSystems.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Order/Order
        [HttpGet]
        public JsonResult Order()
        {
            var model = OrderViewModel.Retrieve();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // POST: /Order/AddItem
        [HttpPost]
        public void AddItem(int itemId)
        {
            var currentOrder = OrderViewModel.Retrieve();
            var dbItem = db.Items.Find(itemId);
            var i = new MarketPlaceItemViewModel(dbItem)
            {
                Quantity = 1
            };

            var itemInOrder = currentOrder.Items.FirstOrDefault(x => x.ItemId == i.ItemId);

            if (currentOrder.Items.Contains(i) && itemInOrder != null && itemInOrder.Quantity < i.Quantity)
            {
                itemInOrder.Quantity += 1;
            }
            else if (currentOrder.Items.Contains(i) && itemInOrder != null && itemInOrder.Quantity == i.Quantity)
            {
                itemInOrder.Quantity = itemInOrder.Quantity;
            }
            else
            {
                currentOrder.Items.Add(i);
            }
            currentOrder.Save();
        }

        // POST: /Order/RemoveItem
        [HttpPost]
        public void RemoveItem(int itemId)
        {
            var currentOrder = OrderViewModel.Retrieve();
            var i = currentOrder.Items.FirstOrDefault(x => x.ItemId == itemId);
            if (i != null && i.Quantity <= 0)
            {
                currentOrder.Items.Remove(i);
            }
            else if (i != null && i.Quantity > 0)
            {
                i.Quantity -= 1;
            }
            currentOrder.Save();
        }
    }
}