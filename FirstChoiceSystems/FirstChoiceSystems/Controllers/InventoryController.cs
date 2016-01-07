using FirstChoiceSystems.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstChoiceSystems.Models.DBModels;
using FirstChoiceSystems.Models.ViewModels;

namespace FirstChoiceSystems.Controllers
{
    public class InventoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Inventory/AllItemsUpForSale
        [HttpGet]
        public ActionResult AllItemsUpForSale()
        {
            //For testing purposes, user can view all items, including himself
            return View(db.Users.Max(x => x.ItemsUpForSale).Select(x => new ItemViewModel()
            {
                ItemId = x.Id,
                ItemDescription = x.ItemDescription,
                ItemName = x.ItemName,
                Seller = x.Seller.CompanyName,
                Quantity = x.UnitsAvailable,
                Price = x.PricePerUnit
            }));
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
            var item = db.Items.FirstOrDefault(x => x.Id == itemId);
            var itemDetail = new ItemViewModel()
            {
                ItemDescription = item.ItemDescription,
                ItemName = item.ItemName,
                Seller = item.Seller.CompanyName,
                Price = item.PricePerUnit,
                ItemId = item.Id,
                Quantity = item.UnitsAvailable
            };
            return View(itemDetail);
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
            db.Items.Add(newItem);
            db.SaveChanges();
            return RedirectToAction("ItemDetails", "Inventory", new { itemId = newItem.Id });
        }
    }
}