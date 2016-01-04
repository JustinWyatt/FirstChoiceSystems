using FirstChoiceSystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstChoiceSystems.Controllers
{

    public class TransactionController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult MakeTransaction()
        {
            return View();
        }
    }
}