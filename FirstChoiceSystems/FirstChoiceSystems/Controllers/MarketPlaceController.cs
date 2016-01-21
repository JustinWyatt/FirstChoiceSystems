using System.Linq;
using System.Web.Mvc;
using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace FirstChoiceSystems.Controllers
{
    public class MarketPlaceController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: MarketPlace/MarketPlace
        [HttpGet]
        public JsonResult MarketPlace()
        {
            var model = db.Items.Where(x => x.AvailableForMarket).Select(x => new MarketPlaceItemViewModel(x)).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //GET : MarketPlace/MarketPlaceItem
        [HttpGet]
        public JsonResult MarketPlaceItem(int itemId)
        {
            var dbItem = db.Items.Find(itemId);
            var model = new MarketPlaceItemViewModel(dbItem);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecentlyAddedProducts()
        {
            var model =
                db.Items.OrderByDescending(x => x.CreatedOn)
                    .Select(x => new MarketPlaceItemViewModel(x))
                    .Take(6)
                    .ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: MarketPlace/YourMarketPlaceItems
        [HttpGet]
        public JsonResult YourMarketPlaceItems()
        {
            var userId = User.Identity.GetUserId();
            var model = db.Items.Where(x => x.Seller.Id == userId && x.AvailableForMarket)
                .Select(x => new MarketPlaceItemViewModel(x))
                .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}