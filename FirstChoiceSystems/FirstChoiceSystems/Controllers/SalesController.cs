using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.DBModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstChoiceSystems.Models.ViewModels;

namespace FirstChoiceSystems.Controllers
{
    public class SalesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Sales/SalesHistory
        [HttpGet]
        public JsonResult SalesHistory()
        {
            var userId = User.Identity.GetUserId();
            return Json(db.PurchaseItems.Where(x => x.Item.Seller.Id == userId).Select(saleitem => new PurchaseItemViewModel()
            {
                ItemId = saleitem.Item.Id,
                ItemName = saleitem.Item.ItemName,
                Seller = saleitem.Item.Seller.CompanyName,
                Price = saleitem.PricePerUnitBoughtAt * saleitem.QuanityBought,
                QuanityBought = saleitem.QuanityBought,
                Status = saleitem.Status.ToString(),
                ApprovalDate = saleitem.ApprovalDate,
                Buyer = saleitem.Buyer.CompanyName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: /Sales/PendingSales
        [HttpGet]
        public JsonResult PendingSales()
        {
            var userId = User.Identity.GetUserId();
            var model = db.PurchaseItems
                .Where(x => x.Item.Seller.Id == userId && x.Status == TransactionStatus.Pending)
                .Select(pi => new PurchaseItemViewModel()
                {
                    ItemId = pi.Item.Id,
                    ItemName = pi.Item.ItemName,
                    Seller = pi.Item.Seller.CompanyName,
                    Price = pi.PricePerUnitBoughtAt * pi.QuanityBought,
                    QuanityBought = pi.QuanityBought,
                    Status = pi.Status.ToString(),
                    ApprovalDate = pi.ApprovalDate,
                    Buyer = pi.Buyer.CompanyName
                }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // POST: /Sales/ApproveSale
        [HttpPost]
        public JsonResult ApproveSale(int id)
        {
            var purchaseItems = db.PurchaseItems.First(x => x.Item.Id == id);
            purchaseItems.ApprovalDate = DateTime.Now;
            purchaseItems.Item.Seller.Balance += purchaseItems.PricePerUnitBoughtAt * purchaseItems.QuanityBought;
            purchaseItems.Buyer.Balance -= purchaseItems.PricePerUnitBoughtAt * purchaseItems.QuanityBought;
            purchaseItems.Item.UnitsAvailable -= purchaseItems.QuanityBought;
            purchaseItems.Item.RevenueInTradeDollars += purchaseItems.PricePerUnitBoughtAt*purchaseItems.QuanityBought;
            purchaseItems.Status = TransactionStatus.Approved;
            db.SaveChanges();
            return Json("Sale Approved", JsonRequestBehavior.AllowGet);
        }

        // POST: /Sales/RejectSale
        [HttpPost]
        public JsonResult RejectSale(int purchaseItemId)
        {
            var purchaseItems = db.PurchaseItems.Find(purchaseItemId);
            purchaseItems.Status = TransactionStatus.Voided;
            db.SaveChanges();
            return Json ("Sale Rejected", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ItemDetails(int itemId)
        {
            var item = db.Items.First(x => x.Id == itemId);

            var itemDetail = new MarketPlaceItemViewModel()
            {
                ItemDescription = item.ItemDescription,
                ItemName = item.ItemName,
                Seller = item.Seller.CompanyName,
                Price = item.PricePerUnit,
                ItemId = item.Id,
                Quantity = item.UnitsAvailable
            };
            return Json(itemDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddItem(int itemId)
        {
            var currentOrder = OrderViewModel.Retrieve();
            var dbItem = db.Items.Find(itemId);

            var i = currentOrder.Items.FirstOrDefault(x => x.ItemId == itemId);
            if (i == null)
            {
                i = new MarketPlaceItemViewModel()
                {
                    ItemId = dbItem.Id,
                    ItemDescription = dbItem.ItemDescription,
                    Price = dbItem.PricePerUnit,
                    Quantity = 1,
                    Seller = dbItem.Seller.CompanyName
                };

                currentOrder.Items.Add(i);
            }
            else
            {
                i.Quantity += 1;
            }

            //if they are asking for more than what is available, cap it to just whats avaiable.
            i.Quantity = dbItem.UnitsAvailable < i.Quantity ? dbItem.UnitsAvailable : i.Quantity;

            currentOrder.Save();
            return RedirectToAction("Order", "Order");
        }
    }
}