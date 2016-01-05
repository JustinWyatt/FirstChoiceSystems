using FirstChoiceSystems.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstChoiceSystems.Controllers
{
    public class InventoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Inventory/ViewInventory
        [HttpGet]
        public ActionResult ViewInventory()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Inventories.Find(userId));
        }


        private void SaveCart(OrderViewModel order)
        {
            HttpContext.Session["currentOrder"] = order;
            
        }

        private OrderViewModel GetCart()
        {
            var order = (OrderViewModel)HttpContext.Session["currentOrder"];

            if (order == null)
            {
                order = new OrderViewModel();
                HttpContext.Session["currentCart"] = order;
            }

            return order;
        }
        
    }
}