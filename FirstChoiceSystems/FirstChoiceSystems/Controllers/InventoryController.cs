using FirstChoiceSystems.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using FirstChoiceSystems.Models.DBModels;
using FirstChoiceSystems.Models.ViewModels;

namespace FirstChoiceSystems.Controllers
{
    public class InventoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        // GET: /Inventory/Inventory
        public ActionResult Inventory()
        {
            //user can only view his own inventory
            var userId = User.Identity.GetUserId();
            return View(db.Items.Where(x => x.Seller.Id == userId).Select(x=> new MarketPlaceItem()
            {
                ItemId = x.Id,
                ItemName = x.ItemName,
                
            }).ToList());
        }

        [HttpGet]
        // GET: /Inventory/InventoryItem
        public ActionResult InventoryItem(int itemId)
        {
            //user is able to dynamically edit any field in item
            return View(db.Items.Where(x => x.Id == itemId).ToList());
        }
        
        // GET: /Inventory/AllItemsUpForSale
        [HttpGet]
        public ActionResult AllItemsUpForSale()
        {
            //For testing purposes, user can view all items, including himself
            return View(db.Items.Select(x => new MarketPlaceItem()
            {
                ItemId = x.Id,
                ItemDescription = x.ItemDescription,
                ItemName = x.ItemName,
                Seller = x.Seller.CompanyName,
                Quantity = x.UnitsAvailable,
                Price = x.PricePerUnit,
                Images = x.Images
            }));
        }

        // GET: /Inventory/AddItem
        [HttpGet]
        public ActionResult AddItem()
        {
            return View();
        }

        // POST: /Inventory/AddItem
        [HttpPost]
        public ActionResult AddItem(Item item)
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
            return RedirectToAction("Inventory", "Inventory", new { itemId = newItem.Id });
        }

        [HttpGet]
        public ActionResult ItemDetails(int itemId)
        {
            var item = db.Items.First(x => x.Id == itemId);

            var itemDetail = new MarketPlaceItem()
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
    }
}