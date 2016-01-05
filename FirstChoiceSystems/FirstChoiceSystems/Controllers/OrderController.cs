using FirstChoiceSystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstChoiceSystems.Controllers
{
    public class OrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        OrderViewModel currentOrder = OrderViewModel.OrderInstance.GetOrders();

        // GET: /Order/Order
        [HttpGet]
        public ActionResult ShoppingCart()
        {
            return View(currentOrder);
        }

        // POST: /Order/AddItem
        [HttpPost]
        public ActionResult AddItem(int itemId)
        {
            var item = db.Items.Find(itemId);
            currentOrder.AddItem(item);
            currentOrder.Items.Add(item);
            if (currentOrder.Items.Contains(item))
            {
                foreach (Item i in currentOrder.Items)
                {
                    if (i.Equals(item))
                    {
                        item.Quantity++;
                    }
                }
            }
            else
            {
                item.Quantity = 1;
                currentOrder.Items.Add(item);
            }
            currentOrder.SaveCart(currentOrder);    
            return RedirectToAction("Order", "Order");
        }

        // POST: /Order/RemoveItem
        [HttpPost]
        public ActionResult RemoveItem(int itemId)
        {
            var item = db.Items.Find(itemId);
            currentOrder.RemoveItem(item);
            currentOrder.SaveCart(currentOrder);
            return RedirectToAction("Order", "Order");
        }
    }
}




















//private void AddToCart(int itemId)
//{

//}

//private void SaveCart(OrderViewModel order)
//{
//    HttpContext.Session["currentOrder"] = order;

//}

//private OrderViewModel GetCart()
//{
//    var order = (OrderViewModel)HttpContext.Session["currentOrder"];

//    if (order == null)
//    {
//        order = new OrderViewModel();
//        HttpContext.Session["currentCart"] = order;
//    }

//    return order;
//}