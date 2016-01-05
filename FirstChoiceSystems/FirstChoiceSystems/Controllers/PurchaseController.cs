using FirstChoiceSystems.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstChoiceSystems.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: /Purchase/PurchaseForm    
        [HttpGet]
        public ActionResult PurchaseForm()
        {
            return View();
        }

        // POST: /Purchase/SubmitPurchaseRequest
        [HttpPost]
        public ActionResult SubmitPurchaseRequest(OrderViewModel order)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var purchase = new Purchase()
            {
                Amount = order.SubTotal,
                Buyer = user,
                Status = TransactionStatus.Pending,
                ListOfItems = order.Items,
                Sellers = order.Items.Select(x=>x.Seller).ToList()
            };

            if (user.Balance < purchase.Amount)
            {
                //return not enough funds
                return RedirectToAction("");
            }

            user.Purchases.Add(purchase);
            db.SaveChanges();
            return RedirectToAction("PurchaseHistory", "Purchase");
        }

        // GET: /Purchase/PurchaseHistory
        [HttpGet]
        public ActionResult PurchaseHistory()
        {
            var userId = User.Identity.GetUserId();
            //returns transactions for users who have made purchases
            return View(db.Purchases.Where(x=>x.Buyer.Id == userId).ToList());
        }

        // GET: /Purchase/PendingPurchases
        [HttpGet]
        public ActionResult PendingPurchases(int purchaseId)
        {
            var purchase = db.Purchases.Find(purchaseId);

            var userId = User.Identity.GetUserId();

            var listOfItemsInPurchasesThatBelongToUser = purchase.Sellers.Where(x => x.Id == userId).ToList();

            //returns a list of items that must be checked by this user, who is the seller
            return View(listOfItemsInPurchasesThatBelongToUser);
        }

        // POST: /Purchase/ApprovePurchase    
        [HttpPost]
        public ActionResult ApprovePurchase(int purchaseId)
        {
            var purchase = db.Purchases.Find(purchaseId);
            var user = db.Users.Find(User.Identity.GetUserId());

            var usersItems = purchase.ListOfItems.Where(x => x.Seller.Id == user.Id);
            foreach (var item in usersItems)
            {
                user.Balance += item.Price;
            }
            db.SaveChanges();
            return RedirectToAction("PendingPurchases", "Purchase");
        }

        // POST: /Purchase/RejectPurchase 
        [HttpPost]
        public ActionResult RejectPurchase(int purchaseId)
        {
            var transaction = db.Purchases.Find(purchaseId);
            transaction.Status = TransactionStatus.Voided;

            db.SaveChanges();
            return RedirectToAction("PurchaseHistory", "Purchase");
        }
        
    }
}