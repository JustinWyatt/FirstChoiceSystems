using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.DBModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstChoiceSystems.Models.ViewModels;

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
            return View(db.PurchaseItems.Where(x => x.Item.Seller.Id == userId).Select(saleitem => new PurchaseItemViewModel()
            {
                ItemId = saleitem.Item.Id,
                ItemName = saleitem.Item.ItemName,
                Seller = saleitem.Item.Seller.CompanyName,
                Price = saleitem.PricePerUnitBoughtAt * saleitem.QuanityBought,
                QuanityBought = saleitem.QuanityBought,
                Status = saleitem.Status.ToString(),
                ApprovalDate = saleitem.ApprovalDate,
                Buyer = saleitem.Buyer.CompanyName
            }).ToList());
        }

        // GET: /Sales/PendingSales
        [HttpGet]
        public ActionResult PendingSales()
        {
            var userId = User.Identity.GetUserId();
            return View(db.PurchaseItems.Where(x => x.Item.Seller.Id == userId && x.Status == TransactionStatus.Pending).Select(saleitem => new PurchaseItemViewModel()
            {
                ItemId = saleitem.Item.Id,
                ItemName = saleitem.Item.ItemName,
                Seller = saleitem.Item.Seller.CompanyName,
                Price = saleitem.PricePerUnitBoughtAt * saleitem.QuanityBought,
                QuanityBought = saleitem.QuanityBought,
                Status = saleitem.Status.ToString(),
                ApprovalDate = saleitem.ApprovalDate,
                Buyer = saleitem.Buyer.CompanyName
            }).ToList());
        }

        // POST: /Sales/ApproveSale
        [HttpPost]
        public ActionResult ApproveSale(int purchaseItemVmId)
        {
            var purchaseItems = db.PurchaseItems.First(x=>x.Id == purchaseItemVmId);
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

        [HttpGet]
        public ActionResult ItemDetails(int itemId)
        {
            var item = db.Items.First(x => x.Id == itemId);

            var itemDetail = new ItemViewModel()
            {
                ItemDescription = item.ItemDescription,
                ItemName = item.ItemName,
                Seller = item.Seller.CompanyName,
                Price = item.PricePerUnit,
                ItemId = item.Id,
                Quantity = item.UnitsAvailable
            };
            return View(itemDetail);
        }

        [HttpPost]
        public ActionResult AddItem(int itemId)
        {
            var currentOrder = OrderViewModel.Retrieve();
            var dbItem = db.Items.Find(itemId);

            var i = currentOrder.Items.FirstOrDefault(x => x.ItemId == itemId);
            if (i == null)
            {
                i = new ItemViewModel()
                {
                    ItemId = dbItem.Id,
                    ItemDescription = dbItem.ItemDescription,
                    Price = dbItem.PricePerUnit,
                    Quantity = 1,
                    Seller = dbItem.Seller.CompanyName
                };

                currentOrder.Items.Add(i);
            }
            else
            {
                i.Quantity += 1;
            }

            //if they are asking for more than what is available, cap it to just whats avaiable.
            i.Quantity = dbItem.UnitsAvailable < i.Quantity ? dbItem.UnitsAvailable : i.Quantity;

            currentOrder.Save();
            return RedirectToAction("Order", "Order");
        }
    }
}