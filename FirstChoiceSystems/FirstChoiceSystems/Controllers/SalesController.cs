using FirstChoiceSystems.Models;
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
            var user = User.Identity.GetUserId();
            return View(db.Users.Find(user));
        }

        // POST: /Sales/PendingSales
        [HttpGet]
        public ActionResult PendingSales(int purchaseId)
        {
            var purchase = db.Purchases.Find(purchaseId);

            var userId = User.Identity.GetUserId();

            var listOfItemsInPurchasesThatBelongThisUser = purchase.ListOfItems.Where(x => x.Seller.Id == userId && x.DateSold == null).ToList();

            //returns a list of items that must be checked by this user, who is the seller
            return View(listOfItemsInPurchasesThatBelongThisUser);
        }
    }
}