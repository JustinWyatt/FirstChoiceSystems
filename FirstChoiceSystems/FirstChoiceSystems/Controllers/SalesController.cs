using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.DBModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstChoiceSystems.Controllers
{
    public class SalesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Sales/SalesHistory
        [HttpGet]
        public ActionResult SalesHistory()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var salesHistory = new List<PurchaseItem>();
            foreach (var purchaseItem in db.Purchases.Select(x => x.PurchaseItems).ToList())
            {
                salesHistory = purchaseItem.Where(x => x.Item.Seller.Id == userId).ToList();
            }
            return View(salesHistory);
        }

        // GET: /Sales/PendingSales
        [HttpGet]
        public ActionResult PendingSales()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var pendingSales = new List<PurchaseItem>();
            foreach (var purchaseItem in db.Purchases.Select(x => x.PurchaseItems).ToList())
            {
                pendingSales = purchaseItem.Where(x => x.Item.Seller.Id == userId && x.Status == TransactionStatus.Pending).ToList();
            }
            return View(pendingSales);
        }

        // POST: /Sales/ApproveSale
        [HttpPost]
        public ActionResult ApproveSale(int purchaseItemId)
        {
            var purchaseItems = db.PurchaseItems.Find(purchaseItemId);
            purchaseItems.ApprovalDate = DateTime.Now;
            purchaseItems.Item.Seller.Balance += purchaseItems.PricePerUnitBoughtAt * purchaseItems.QuanityBought;
            purchaseItems.Buyer.Balance -= purchaseItems.PricePerUnitBoughtAt * purchaseItems.QuanityBought;
            purchaseItems.Item.UnitsAvailable -= purchaseItems.QuanityBought;
            purchaseItems.Status = TransactionStatus.Approved;
            db.SaveChanges();
            return RedirectToAction("PendingSales", "Sales");
        }

        // POST: /Sales/RejectSale
        [HttpPost]
        public ActionResult RejectSale(int purchaseItemId)
        {
            var purchaseItems = db.PurchaseItems.Find(purchaseItemId);
            purchaseItems.Status = TransactionStatus.Voided;
            db.SaveChanges();
            return RedirectToAction("PendingSales", "Sales");
        }
    }
}