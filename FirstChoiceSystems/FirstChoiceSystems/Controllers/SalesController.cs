using System;
using System.Linq;
using System.Web.Mvc;
using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.DBModels;
using FirstChoiceSystems.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace FirstChoiceSystems.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Sales/SalesHistory
        [HttpGet]
        public JsonResult SalesHistory()
        {
            var userId = User.Identity.GetUserId();
            var model =
                db.PurchaseItems.Where(x => x.Item.Seller.Id == userId)
                                .ToList()
                                .Select(saleitem => new PurchaseItemViewModel
                                {
                                    ItemId = saleitem.Item.Id,
                                    ItemName = saleitem.Item.ItemName,
                                    Seller = saleitem.Item.Seller.CompanyName,
                                    Price = saleitem.PricePerUnitBoughtAt * saleitem.QuanityBought,
                                    QuanityBought = saleitem.QuanityBought,
                                    Status = saleitem.Status.ToString(),
                                    ApprovalDate = saleitem.ApprovalDate,
                                    Buyer = saleitem.Buyer.CompanyName
                                }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: /Sales/PendingSales
        [HttpGet]
        public JsonResult PendingSales()
        {
            var userId = User.Identity.GetUserId();
            var model = db.PurchaseItems
                .Where(x => x.Item.Seller.Id == userId && x.Status == TransactionStatus.Pending)
                .Select(pi => new PurchaseItemViewModel
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
            var totalPrice = purchaseItems.PricePerUnitBoughtAt * purchaseItems.QuanityBought;

            purchaseItems.Item.Seller.Balance += totalPrice;
            purchaseItems.Buyer.Balance -= totalPrice;
            purchaseItems.Item.RevenueInTradeDollars += totalPrice;

            var dbItem = db.Items.Find(id);
            if (dbItem.UnitsAvailable == 0)
            {
                dbItem.UnitsAvailable -= 0;
            }
            else
            {
                dbItem.UnitsAvailable -= purchaseItems.QuanityBought;
            }
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
            return Json("Sale Rejected", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ItemDetails(int itemId)
        {
            var item = db.Items.First(x => x.Id == itemId);

            var itemDetail = new MarketPlaceItemViewModel(item);

            return Json(itemDetail, JsonRequestBehavior.AllowGet);
        }
    }
}