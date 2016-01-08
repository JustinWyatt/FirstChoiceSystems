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

        // GET: /Inventory/AllItemsUpForSale
        [HttpGet]
        public ActionResult AllItemsUpForSale()
        {
            //For testing purposes, user can view all items, including himself
            return View(db.Items.Select(x => new ItemViewModel()
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
            return RedirectToAction("ItemDetails", "Order", new { itemId = newItem.Id });
        }

        [HttpGet]
        public ActionResult ItemDetails(int itemId)
        {
            var item = db.Items.First(x => x.Id == itemId);

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
    }
}