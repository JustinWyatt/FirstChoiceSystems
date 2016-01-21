using System.Linq;
using System.Web.Mvc;
using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.DBModels;
using FirstChoiceSystems.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace FirstChoiceSystems.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private BusinessUser _currentUser;
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = User.Identity.GetUserId();
            _currentUser = db.Users.Find(userId);
            base.OnActionExecuting(filterContext);
        }

        // GET: /Inventory/Inventory
        [HttpGet]
        public JsonResult Inventory()
        {
            //user can only view his own inventory
            var inventory = _currentUser.ItemsUpForSale.Select(x => new InventoryItemViewModel
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
        public JsonResult AddItem(InventoryItemViewModel item)
        {
            var newItem = new Item
            {
                ItemDescription = item.ItemDescription,
                PricePerUnit = item.PricePerUnit,
                ItemCategory = db.ItemCategories.Find(item.ItemCategory),
                Seller = _currentUser,
                UnitsAvailable = item.UnitsAvailable,
                AvailableForMarket = item.AvailableForMarket,
                ItemName = item.ItemName
            };

            db.Items.Add(newItem);
            db.SaveChanges();


            item.ItemId = newItem.Id;
            item.Seller = newItem.Seller.PersonOfContact;


            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Categories()
        {
            return Json(db.BusinessCategories.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveItem(int id)
        {
            var item = db.Items.Find(id);
            if (item != null)
            {
                db.Items.Remove(item);
                db.SaveChanges();
            }
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        // POST: Inventory/AddMarketPlaceItem
        [HttpPost]
        public JsonResult AddMarketPlaceItem(int itemId)
        {
            var item = db.Items.Find(itemId);
            item.AvailableForMarket = true;
            db.SaveChanges();

            return Json("Ok");
        }

        // POST: Inventory/RemoveMarketPlaceItem
        [HttpPost]
        public JsonResult RemoveMarketPlaceItem(int itemId)
        {
            var item = db.Items.Find(itemId);
            item.AvailableForMarket = false;
            db.SaveChanges();

            return Json("Ok");
        }
    }
}