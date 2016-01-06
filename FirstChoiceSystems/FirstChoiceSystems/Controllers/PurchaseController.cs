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

        // GET: /Purchase/PurchaseHistory
        [HttpGet]
        public ActionResult PurchaseHistory()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Users.Find(userId).Purchases.ToList());
        }

        // GET: /Purchase/PurchaseDetail
        [HttpGet]
        public ActionResult PurchaseDetail(int purchaseId)
        {
            var userId = User.Identity.GetUserId();
            return View(db.Users.Find(userId).Purchases.Find(x => x.Id == purchaseId));
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
                var newPurchase = new PurchaseItem()
                {
                    Buyer = user,
                    Item = itemFromDB,
                    PricePerUnitBoughtAt = itemFromDB.PricePerUnit,
                    QuanityBought = itemVM.Quantity,
                    Status = TransactionStatus.Pending,
                };

                newPurchaseRequest.Add(newPurchase);
            }
            user.Purchases.AddRange(newPurchaseRequest);
            db.SaveChanges();
            return RedirectToAction("PurchaseDetail", "Purchase", new { purchaseId =  user.Purchases.Select(x=>x.Id) });
        }

    }
}