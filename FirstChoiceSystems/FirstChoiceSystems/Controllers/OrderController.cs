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
        public ActionResult AddItem(int itemId)
        {
            var currentOrder = OrderViewModel.Retrieve();
            var dbItem = db.Items.Find(itemId);

            var i = new MarketPlaceItemViewModel(dbItem);
            currentOrder.Items.Add(i);

            currentOrder.Save();
            return RedirectToAction("Order", "Order");
        }

        // POST: /Order/RemoveItem
        [HttpPost]
        public ActionResult RemoveItem(int itemId)
        {
            var currentOrder = OrderViewModel.Retrieve();
            var i = currentOrder.Items.FirstOrDefault(x => x.ItemId == itemId);
            if (i == null)
                return RedirectToAction("Order", "Order");

            i.Quantity -= 1;
            if (i.Quantity <= 0)
            {
                currentOrder.Items.Remove(i);
            }
            currentOrder.Save();

            return RedirectToAction("Order", "Order");
        }
    }
}