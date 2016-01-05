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
        OrderViewModel currentOrder = OrderViewModel.OrderInstance;

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
            var userId = User.Identity.GetUserId();
            //returns transactions for users who have made purchases
            return View(db.Purchases.Where(x => x.Buyer.Id == userId).ToList());
        }

        // POST: /Purchase/PurchaseRequest
        [HttpPost]
        public ActionResult PurchaseRequest()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var purchaseRequest = new Purchase()
            {
                Amount = currentOrder.SubTotal,
                Buyer = user,
                Status = TransactionStatus.Pending,
                ListOfItems = currentOrder.Items,
                Sellers = currentOrder.Items.Select(x => x.Seller).ToList()
            };

            if (user.Balance < purchaseRequest.Amount)
            {
                //return not enough funds
                return RedirectToAction("");
            }

            foreach (var seller in currentOrder.Items.Select(x => x.Seller).ToList())
            {
                foreach (var individualItem in currentOrder.Items.Where(x => x.Seller.Id == seller.Id).ToList())
                {
                    var sale = new Sale()
                    {
                        Status = TransactionStatus.Pending,
                        ItemSold = individualItem,
                        SaleAmount = individualItem.Price * individualItem.Quantity,
                        Seller = seller                        
                    };

                    sale.Buyers.Add(user);
                    seller.Sales.Add(sale);
                }
            }

            user.Purchases.Add(purchaseRequest);
            db.SaveChanges();
            return RedirectToAction("PurchaseHistory", "Purchase");
        }

    }
}