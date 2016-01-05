﻿using FirstChoiceSystems.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstChoiceSystems.Controllers
{
    public class InventoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Inventory/ViewInventory
        [HttpGet]
        public ActionResult ViewInventory()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Inventories.Where(x=>x.Business.Id == userId).ToList());
        }
        
        // GET: /Inventory/AddItem
        [HttpGet]
        public ActionResult AddItem(int itemId)
        {
            return RedirectToAction("");
        }                
    }
}