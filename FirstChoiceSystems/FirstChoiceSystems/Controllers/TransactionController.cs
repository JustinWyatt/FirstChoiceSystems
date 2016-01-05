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
            var newTransaction = new Transaction()
            {
                Amount = order.Items.Sum(x => x.Price),
                Buyer = user,
                Status = TransactionStatus.Pending,
            };

            user.Transactions.Add(newTransaction);
            db.SaveChanges();
            if (newTransaction.Status == TransactionStatus.Pending)
            {
                return Json(newTransaction, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("TransactionHistory", "Transaction");
        }

        // GET: /Transaction/TransactionHistory
        [HttpGet]
        public ActionResult TransactionHistory()
        {
            return View();
        }

        // POST: /Transaction/ApproveTransaction    
        [HttpPost]
        public ActionResult ApproveTransaction(int transactionId)
        {
            var transaction = db.Transactions.Find(transactionId);

            //don't update the transaction if its not pending.
            if (transaction.Status != TransactionStatus.Pending)
            {
                return RedirectToAction("TransactionHistory", "Transaction");
            }

            transaction.ApprovalDate = DateTime.Now;
            transaction.Status = TransactionStatus.Approved;

            return RedirectToAction("TransactionHistory", "Transaction");
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