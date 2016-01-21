﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.DBModels;
using FirstChoiceSystems.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace FirstChoiceSystems.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Purchase/PurchaseRequestHistory
        [HttpGet]
        public JsonResult PurchaseRequestHistory()
        {
            //user can only view his own purchases
            var userId = User.Identity.GetUserId();
            return
                Json(db.PurchaseItems.Where(x => x.Buyer.Id == userId).Select(purchaseItem => new PurchaseItemViewModel
                {
                    DatePurchased = purchaseItem.DatePurchased,
                    ApprovalDate = purchaseItem.ApprovalDate,
                    ItemName = purchaseItem.Item.ItemName,
                    ItemId = purchaseItem.Item.Id,
                    Status = purchaseItem.Status.ToString(),
                    Seller = purchaseItem.Item.Seller.CompanyName,
                    QuanityBought = purchaseItem.QuanityBought,
                    Price = purchaseItem.QuanityBought*purchaseItem.PricePerUnitBoughtAt
                }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // POST: /Purchase/PurchaseRequest
        [HttpPost]
        public ActionResult PurchaseRequest()
        {
            var currentOrder = OrderViewModel.Retrieve();

            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var newPurchaseRequest = new List<PurchaseItem>();
            var date = DateTime.Now;
            foreach (var itemVm in currentOrder.Items)
            {
                var itemFromDb = db.Items.Find(itemVm.ItemId);
                var newPurchaseItem = new PurchaseItem
                {
                    Buyer = user,
                    Item = itemFromDb,
                    PricePerUnitBoughtAt = itemFromDb.PricePerUnit,
                    QuanityBought = itemVm.Quantity,
                    Status = TransactionStatus.Pending,
                    DatePurchased = date
                };
                newPurchaseRequest.Add(newPurchaseItem);
            }
            db.PurchaseItems.AddRange(newPurchaseRequest);
            db.SaveChanges();

            currentOrder.Save();
            return RedirectToAction("PurchaseRequestHistory", "Purchase");
        }
    }
}