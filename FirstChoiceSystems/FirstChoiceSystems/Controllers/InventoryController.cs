using FirstChoiceSystems.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using FirstChoiceSystems.Models.DBModels;
using FirstChoiceSystems.Models.ViewModels;
using Newtonsoft.Json.Linq;

namespace FirstChoiceSystems.Controllers
{
    public class InventoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        // GET: /Inventory/Inventory
        public JsonResult Inventory()
        {
            //user can only view his own inventory
            var userId = User.Identity.GetUserId();
            var inventory = db.Items.Where(x => x.Seller.Id == userId).Select(x => new InventoryItemViewModel()
            {
                CashCost = x.CashCost,
                CashEquivalentValue = x.CashEquivalentValue,
                RevenueInCash = x.RevenueInCash,
                RevenueInTradeDollars = x.RevenueInTradeDollars,
                ItemDescription = x.ItemDescription,
                ItemName = x.ItemName,
                UnitsAvailable = x.UnitsAvailable,
                PricePerUnit = x.PricePerUnit,
                ItemId = x.Id

            }).ToList();

            return Json(inventory, JsonRequestBehavior.AllowGet);
        }
        // POST: /Inventory/AddItem
        [HttpPost]
        public JsonResult AddItem(ItemInputModel item)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var newItem = new Item()
            {
                ItemDescription = item.ItemDescription,
                PricePerUnit = item.PricePerUnit,
                ItemCategory = db.ItemCategories.Find(item.ItemCategory),
                Seller = user,
                UnitsAvailable = item.UnitsAvailable,
                AvailableForMarket = item.AvailableForMarket
            };
            db.Items.Add(newItem);
            db.SaveChanges();
            var returnItem = new InventoryItemViewModel()
            {
                ItemDescription = newItem.ItemDescription,
                PricePerUnit = newItem.PricePerUnit,
                Seller = user.PersonOfContact,
                UnitsAvailable = newItem.UnitsAvailable,
                AvailableForMarket = newItem.AvailableForMarket
            };
            return Json(returnItem, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Categories()
        {
            return Json(db.BusinessCategories.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void RemoveItem(int id)
        {
            var item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
        }

        // POST: Inventory/AddMarketPlaceItem
        [HttpPost]
        public void AddMarketPlaceItem(int itemId)
        {
            var item = db.Items.Find(itemId);
            item.AvailableForMarket = true;
            db.SaveChanges();
        }

        // POST: Inventory/RemoveMarketPlaceItem
        [HttpPost]
        public void RemoveMarketPlaceItem(int itemId)
        {
            var item = db.Items.Find(itemId);
            item.AvailableForMarket = false;
            db.SaveChanges();
        }
    }
}