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
            return View(db.Sales.Where(x => x.Seller.Id == user).ToList());
        }

        // GET: /Sales/PendingSales
        [HttpGet]
        public ActionResult PendingSales(int saleId)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var sales = user.Sales.Where(x => x.Id == saleId && x.Status == TransactionStatus.Pending).ToList();
            return View(sales);
        }

        // POST: /Sales/ApproveSale
        [HttpGet]
        public ActionResult ApproveSale(int saleId)
        {
            var sale = db.Sales.Find(saleId);
            sale.DateSold = DateTime.Now;
            sale.Status = TransactionStatus.Approved;
            db.SaveChanges();
            return RedirectToAction("PendingSales", "Sales");
        }

        // POST: /Sales/RejectSale
        [HttpGet]
        public ActionResult RejectSale(int saleId)
        {
            var sale = db.Sales.Find(saleId);
            sale.Status = TransactionStatus.Voided;
            db.SaveChanges();
            return RedirectToAction("PendingSales", "Sales");
        }
    }
}