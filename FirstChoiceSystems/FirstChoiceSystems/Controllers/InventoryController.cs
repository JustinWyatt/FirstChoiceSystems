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
            return View(db.Items.Where(x => x.Seller.Id == userId).ToList());
        }

        [HttpGet]
        // GET: /Inventory/InventoryItem
        public ActionResult InventoryItem(int itemId)
        {
            //user is able to dynamically edit any field in item
            return View(db.Items.Where(x => x.Id == itemId).ToList());
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
                Images = item.Images,
                AvailableForMarket = item.AvailableForMarket

            };
            db.Items.Add(newItem);
            db.SaveChanges();
            return RedirectToAction("Inventory", "Inventory", new { itemId = newItem.Id });
        }

        // POST: Inventory/AddMarketPlaceItem
        [HttpPost]
        public ActionResult AddMarketPlaceItem(int itemId)
        {
            var addToMarket = db.Items.Find(itemId);
            addToMarket.AvailableForMarket = true;
            db.SaveChanges();
            return RedirectToAction("Inventory", "Inventory");
        }

        // POST: Inventory/RemoveMarketPlaceItem
        [HttpPost]
        public ActionResult RemoveMarketPlaceItem(int itemId)
        {
            var removeMarket = db.Items.Find(itemId);
            removeMarket.AvailableForMarket = false;
            db.SaveChanges();
            return RedirectToAction("Inventory", "Inventory");
        }
    }
}