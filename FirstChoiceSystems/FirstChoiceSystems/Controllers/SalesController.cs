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
            return View(db.Users.Find(user));
        }

        // POST: /Sales/ApproveUser
    }
}