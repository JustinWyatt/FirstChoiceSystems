using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace FirstChoiceSystems.Controllers
{
    public class MarketPlaceController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: MarketPlace/MarketPlace
        [HttpGet]
        public ActionResult MarketPlace()
        {
            return View(db.Items.Where(x => x.AvailableForMarket).Select(x=> new ItemViewModel()
            {
                ItemName = x.ItemName,
                ItemDescription = x.ItemDescription,
                ItemId = x.Id,
                Seller = x.Seller.CompanyName,
                Quantity = x.UnitsAvailable,
                Price = x.PricePerUnit,
                Category = x.ItemCategory.CategoryName
            }).ToList());
        }

        // GET: MarketPlace/MarketPlaceItem
        [HttpGet]
        public ActionResult MarketPlaceItem(int itemId)
        {
            return View();
        }

        // GET: MarketPlace/YourMarketPlaceItems
        [HttpGet]
        public ActionResult YourMarketPlaceItems()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Items.Where(x => x.Seller.Id == userId).Select(x => new ItemViewModel()
            {
                ItemName = x.ItemName,
                ItemDescription = x.ItemDescription,
                ItemId = x.Id,
                Seller = x.Seller.CompanyName,
                Quantity = x.UnitsAvailable,
                Price = x.PricePerUnit,
            }).ToList());
        }

        // POST: MarketPlace/AddMarketPlaceItem
        [HttpPost]
        public ActionResult AddMarketPlaceItem(int itemId)
        {
            var addToMarket = db.Items.Find(itemId);
            addToMarket.AvailableForMarket = true;
            db.SaveChanges();
            return RedirectToAction("YourMarketPlaceItems", "MarketPlace");
        }
        
        // POST: MarketPlace/RemvoeMarketPlaceItem
        [HttpPost]
        public ActionResult RemoveMarketPlaceItem(int itemId)
        {
            var removeMarket = db.Items.Find(itemId);
            removeMarket.AvailableForMarket = false;
            db.SaveChanges();
            return RedirectToAction("YourMarketPlaceItems", "MarketPlace");
        }
    }
}