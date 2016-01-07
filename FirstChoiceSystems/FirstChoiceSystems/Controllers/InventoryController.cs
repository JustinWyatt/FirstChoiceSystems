using FirstChoiceSystems.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstChoiceSystems.Models.DBModels;

namespace FirstChoiceSystems.Controllers
{
    public class InventoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
         
        // GET: /Inventory/ItemsUpForSale
        [HttpGet]
        public ActionResult ItemsUpForSale(string userId)
        {
            var user = db.Users.Find(userId);
            if (userId == User.Identity.GetUserId())
            {
                return View(user.ItemsUpForSale);
            }
            return View(user.ItemsUpForSale);
        }

        // GET: /Inventory/AddItem
        [HttpGet]
        public ActionResult AddItem()
        {
            return View();
        }

        // GET: /Inventory/ItemDetails  
        [HttpGet]
        public ActionResult ItemDetails(int itemId)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            return View(user.ItemsUpForSale.First(x=>x.Id == itemId));
        }

        // POST: /Inventory/PostItem
        [HttpPost]
        public ActionResult PostItem(Item item)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var newItem = new Item()
            {
                ItemDescription = item.ItemDescription,
                PricePerUnit = item.PricePerUnit,
                ItemCategory = item.ItemCategory,
                Seller = user,
                UnitsAvailable = item.UnitsAvailable,
                Images = item.Images
            };
            user.ItemsUpForSale.Add(newItem);
            db.SaveChanges();
            return RedirectToAction("ItemDetails", "Inventory", new { itemId = newItem.Id });
        }
    }   
}