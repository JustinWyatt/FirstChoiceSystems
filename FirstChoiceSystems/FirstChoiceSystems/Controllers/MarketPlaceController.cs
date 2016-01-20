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
        public JsonResult MarketPlace()
        {
            ViewBag.Categories = db.ItemCategories.ToList();

            return Json(db.Items.Where(x => x.AvailableForMarket).Select(x=> new MarketPlaceItemViewModel()
            {
                ItemName = x.ItemName,
                ItemDescription = x.ItemDescription,
                ItemId = x.Id,
                Seller = x.Seller.CompanyName,
                Quantity = x.UnitsAvailable,
                Price = x.PricePerUnit,
                Category = x.ItemCategory.CategoryName,
                Images = x.Images
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //GET : MarketPlace/MarketPlaceItem
        [HttpGet]
        public JsonResult MarketPlaceItem(int itemId)
        {
            var dbItem = db.Items.First(x => x.Id == itemId);
            var marketPlaceItem = new MarketPlaceItemViewModel()
            {
                ItemName = dbItem.ItemName,
                ItemDescription = dbItem.ItemDescription,
                ItemId = dbItem.Id,
                Seller = dbItem.Seller.CompanyName,
                Quantity = dbItem.UnitsAvailable,
                Price = dbItem.PricePerUnit,
                Category = dbItem.ItemCategory.CategoryName,
                Images = dbItem.Images
            };
            return Json(marketPlaceItem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecentlyAddedProducts()
        {
            var model = db.Items.OrderByDescending(x => x.CreatedOn).Select(x => new MarketPlaceItemViewModel()
            {
                ItemName = x.ItemName,
                ItemDescription = x.ItemDescription,
                ItemId = x.Id,
                Seller = x.Seller.CompanyName,
                Quantity = x.UnitsAvailable,
                Price = x.PricePerUnit,
                Category = x.ItemCategory.CategoryName,
                Images = x.Images
            }).Take(6).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: MarketPlace/YourMarketPlaceItems
        [HttpGet]
        public JsonResult YourMarketPlaceItems()
        {
            var userId = User.Identity.GetUserId();
            return Json(db.Items.Where(x => x.Seller.Id == userId && x.AvailableForMarket).Select(x => new MarketPlaceItemViewModel()
            {
                ItemName = x.ItemName,
                ItemDescription = x.ItemDescription,
                ItemId = x.Id,
                Seller = x.Seller.CompanyName,
                Quantity = x.UnitsAvailable,
                Price = x.PricePerUnit,
                Category = x.ItemCategory.CategoryName,
                Images = x.Images
            }).ToList(), JsonRequestBehavior.AllowGet);

            
        }
    }   
}