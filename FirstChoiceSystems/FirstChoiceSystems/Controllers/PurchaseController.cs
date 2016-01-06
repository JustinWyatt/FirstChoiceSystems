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
    [Authorize]
    public class PurchaseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Purchase/PurchaseRequestHistory
        [HttpGet]
        public ActionResult PurchaseRequestHistory()
        {
            //user can only view his own purchases
            var userId = User.Identity.GetUserId();
            return View(db.Users.Find(userId).Purchases);
        }

        // POST: /Purchase/PurchaseRequest
        [HttpPost]
        public ActionResult PurchaseRequest()
        {
            var currentOrder = OrderViewModel.Retrieve();

            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var newPurchaseRequest = new List<PurchaseItem>();
            foreach (var itemVM in currentOrder.Items)
            {
                var itemFromDB = db.Items.Find(itemVM.Id);
                var newPurchaseItem = new PurchaseItem()
                {
                    Buyer = user,
                    Item = itemFromDB,
                    PricePerUnitBoughtAt = itemFromDB.PricePerUnit,
                    QuanityBought = itemVM.Quantity,
                    Status = TransactionStatus.Pending
                };

                newPurchaseRequest.Add(newPurchaseItem);
            }
            user.Purchases.AddRange(newPurchaseRequest);
            db.SaveChanges();
            return RedirectToAction("PurchaseRequestHistory", "Purchase");
        }

    }
}