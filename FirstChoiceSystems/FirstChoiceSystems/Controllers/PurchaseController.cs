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

        // GET: /Purchase/PurchaseForm    
        [HttpGet]
        public ActionResult PurchaseForm()
        {
            return View();
        }

        // GET: /Purchase/PurchaseHistory
        [HttpGet]
        public ActionResult PurchaseHistory()
        {
            var model = db.PurchaseItems.AsNoTracking().Where(x => x.CreatedOn > DateTime.Parse("1/1/2015"));
            var userId = User.Identity.GetUserId();
            //returns transactions for users who have made purchases
            return View(db.Users.Find(userId).Purchases.ToList());
        }

        // POST: /Purchase/PurchaseRequest
        [HttpPost]
        public ActionResult PurchaseRequest()
        {
            var currentOrder = OrderViewModel.Retrieve();

            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var purchases = new List<PurchaseItem>();
            foreach(var itemVM in currentOrder.Items)
            {
                var itemFromDB = db.Items.Find(itemVM.Id);
                var newPurchase = new PurchaseItem()
                {
                    Buyer = user,
                    Item = itemFromDB,
                    PricePerUnitBoughtAt = itemFromDB.PricePerUnit,
                    QuanityBought = itemVM.Quantity,
                    Status = TransactionStatus.Pending
                };

                purchases.Add(newPurchase);
            }

            db.PurchaseItems.AddRange(purchases);
            db.SaveChanges();
         
            return RedirectToAction("PurchaseHistory", "Purchase");
        }

    }
}