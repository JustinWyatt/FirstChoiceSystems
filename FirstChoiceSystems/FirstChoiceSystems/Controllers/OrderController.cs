using System;
using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace FirstChoiceSystems.Controllers
{
    public class OrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Order/Order
        [HttpGet]
        public ActionResult Order()
        {
            return View(OrderViewModel.Retrieve());
        }

        // POST: /Order/AddItem
        [HttpPost]
        public ActionResult AddItem(int itemId)
        {
            var currentOrder =  OrderViewModel.Retrieve();
            var dbItem = db.Items.Find(itemId);

            //if (currentOrder.Items.Single(x => x.ItemId == dbItem.Id) == null)
            //{
                var i = new ItemViewModel()
                {
                    ItemId = dbItem.Id,
                    ItemDescription = dbItem.ItemDescription,
                    Price = dbItem.PricePerUnit,
                    Quantity = 1,
                    Seller = dbItem.Seller.CompanyName
                };
                currentOrder.Items.Add(i);
            //}
            //else
            //{
            //    currentOrder.Items.FirstOrDefault(x => x.ItemId == dbItem.Id).Quantity += 1;
            //}

            //if they are asking for more than what is available, cap it to just whats avaiable.
            //i.Quantity = dbItem.UnitsAvailable < i.Quantity ? dbItem.UnitsAvailable : i.Quantity;
            currentOrder.Save();
            return RedirectToAction("Order", "Order");
        }

        // POST: /Order/RemoveItem
        [HttpPost]
        public ActionResult RemoveItem(int itemId)
        {
            var currentOrder = OrderViewModel.Retrieve();
            var i = currentOrder.Items.FirstOrDefault(x => x.ItemId == itemId);
            if (i != null)
            {
                i.Quantity-= 1;
                if (i.Quantity <= 0)
                {
                    currentOrder.Items.Remove(i);
                }
                currentOrder.Save();
            }

            return RedirectToAction("Order", "Order");
        }
    }
}
