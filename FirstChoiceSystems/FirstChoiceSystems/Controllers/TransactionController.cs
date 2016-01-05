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
    public class TransactionController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        // POST: /Transaction/TransactionForm    
        [HttpGet]
        public ActionResult TransactionForm()
        {
            return View();
        }

        // POST: /Transaction/SubmitTransactionRequest
        [HttpPost]
        public ActionResult SubmitTransactionRequest(OrderViewModel order)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var transaction = new Transaction()
            {
                Amount = order.SubTotal,
                Buyer = user,
                Status = TransactionStatus.Pending,
                ListOfItems = order.Items,
                Sellers = order.Items.Select(x=>x.Seller).ToList()                               
            };

            if (user.Balance < transaction.Amount)
            {
                return RedirectToAction("");
            }

            user.Transactions.Add(transaction);
            db.SaveChanges();
            return RedirectToAction("TransactionHistory", "Transaction");
        }

        // GET: /Transaction/TransactionHistory
        [HttpGet]
        public ActionResult TransactionHistory()
        {
            //returns transactions for users who have made purchases
            return View();
        }

        // GET: /Transaction/PendingTransactions
        [HttpGet]
        public ActionResult PendingTransactions(int transactionId)
        {
            var transaction = db.Transactions.Find(transactionId);

            var userId = User.Identity.GetUserId();

            var listOfItemsInTransactionThatBelongToUser = transaction.Sellers.Where(x => x.Id == userId).ToList();

            //returns a list of items that must be checked by this user, who is the seller
            return View(listOfItemsInTransactionThatBelongToUser);
        }

        // POST: /Transaction/ApproveTransaction    
        [HttpPost]
        public ActionResult ApproveTransaction(int transactionId)
        {
            var transaction = db.Transactions.Find(transactionId);
            var user = db.Users.Find(User.Identity.GetUserId());

            var usersItems = transaction.ListOfItems.Where(x => x.Seller.Id == user.Id);
            foreach (var item in usersItems)
            {
                user.Balance += item.Price;
            }
            db.SaveChanges();
            return RedirectToAction("GetPendingTransactions", "Transaction");
        }

        // POST: /Transaction/RejectTransaction 
        [HttpPost]
        public ActionResult RejectTransaction(int transasctionId)
        {
            var transaction = db.Transactions.Find(transasctionId);
            transaction.Status = TransactionStatus.Voided;

            db.SaveChanges();
            return RedirectToAction("TransactionHistory", "Transaction");
        }
        
    }
}