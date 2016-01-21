using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FirstChoiceSystems.Models;
using FirstChoiceSystems.Models.DBModels;
using System.Collections.Generic;
using FirstChoiceSystems;
using FirstChoiceSystems.Models.ViewModels;

namespace FirstChoiceSystems.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public JsonResult BusinessDirectory()
        {
            var businesses = db.Users.Select(x => new ProfileViewModel()
            {
                PersonOfContact = x.PersonOfContact,
                CompanyName = x.CompanyName,
                CompanyWebsite = x.CompanyWebsite,
                Photo = x.CompanyPhoto,
                ItemsUpForSale = x.ItemsUpForSale.Select(item => new MarketPlaceItemViewModel(item)).ToList()
            }).ToList();
            return Json(businesses, JsonRequestBehavior.AllowGet);
        }

        // GET: /Account/Portal
        [HttpGet]
        [Authorize]
        public ActionResult Portal()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var dashboard = new DashboardViewModel()
            {
                AccountNumber = user.AccountNumber,
                Balance = user.Balance,
                DateRegistered = user.DateRegistered.ToString(),
                CompanyAddress = user.CompanyAddress,
                CompanyName = user.CompanyName,
                CompanyWebsite = user.CompanyName,
                PersonOfContact = user.PersonOfContact,
                BusinessCategory = user.BusinessCategory.CategoryName,
                City = user.City,
                State = user.State,
                Postal = user.Postal,
                CompanyPhoto = user.CompanyPhoto,
                RepresentativePhoto = user.RepresentativePhoto,
                SalesFigure = 0,
                MembersInArea = db.Users.Count(x => x.State == user.State),
                InventoryValue = 0,
                InventoryCount = 0
            };
            return View(dashboard);
        }

        [HttpGet]
        public JsonResult LatestOrders()
        {
            var userId = User.Identity.GetUserId();
            var latestOrders = db.PurchaseItems.Where(x => x.Buyer.Id == userId)
                                               .OrderByDescending(x => x.DatePurchased)
                                               .Take(7)
                                               .Select(x => new PurchaseItemViewModel()
                                               {
                                                   ItemId = x.Id,
                                                   ItemName = x.Item.ItemName,
                                                   Status = x.Status.ToString()
                                               });
            return Json(latestOrders, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RecentlyAddedProducts()
        {
            var userId = User.Identity.GetUserId();
            var recentlyAdded = db.Items.Where(x => x.Seller.Id == userId)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Take(7)
                                        .Select(x => new InventoryItemViewModel()
                                        {
                                            ItemId = x.Id,
                                            ItemName = x.ItemName,
                                            //ItemImage = x.Images.Take(1).ToString(),
                                            ItemDescription = x.ItemDescription
                                        });
            return Json(recentlyAdded, JsonRequestBehavior.AllowGet);
        }

        // GET: /Account/Dashboard
        [HttpGet]
        public JsonResult Dashboard()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var dashboard = new DashboardViewModel()
            {
                AccountNumber = user.AccountNumber,
                Balance = user.Balance,
                DateRegistered = user.DateRegistered.ToString(),
                CompanyAddress = user.CompanyAddress,
                CompanyName = user.CompanyName,
                CompanyWebsite = user.CompanyName,
                PersonOfContact = user.PersonOfContact,
                BusinessCategory = user.BusinessCategory.CategoryName,
                City = user.City,
                State = user.State,
                Postal = user.Postal,
                CompanyPhoto = user.CompanyPhoto,
                RepresentativePhoto = user.RepresentativePhoto,
                MembersInArea = db.Users.Count(x => x.State == user.State),
                InventoryValue = db.Items.Where(x => x.Seller.Id == userId).Sum(x => x.CashCost * x.UnitsAvailable),
                InventoryCount = db.Items.Count(x => x.Seller.Id == userId),
                SalesFigure = db.PurchaseItems.Where(x => x.Item.Seller.Id == userId).Sum(x => x.QuanityBought * x.PricePerUnitBoughtAt),
            };
            return Json(dashboard, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavingsCalculation()
        {
            // get the cost of the inventory
            var userId = User.Identity.GetUserId();
            var inventoryCashCost = db.Items.Where(x => x.Seller.Id == userId).Sum(x => x.CashCost * x.UnitsAvailable);

            // get the trade revenue of the inventory
            var inventorySalesInTradeDollars = db.PurchaseItems.Where(x => x.Item.Seller.Id == userId).Sum(x => x.Item.RevenueInTradeDollars);

            // get all trade expenses
            var purchasesInTradeDollars = db.PurchaseItems.Where(x => x.Buyer.Id == userId);
            var purchaseExpenses = purchasesInTradeDollars.Sum(x => x.PricePerUnitBoughtAt * x.QuanityBought);

            // convert the value of trade expenses into cash and subtract from trade expenses
            var purchaseValuePoints = purchasesInTradeDollars.Sum(x => x.Item.CashEquivalentValue * x.QuanityBought) - purchaseExpenses;

            // how much it cost me to produce inventory minus the inventory sales plus the total purchase value
            var purchasingPower = (inventoryCashCost - inventorySalesInTradeDollars) + purchaseValuePoints;

            return Json(purchasingPower, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult MyProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var profile = new ProfileViewModel()
            {
                DateRegistered = user.DateRegistered.ToString(),
                CompanyName = user.CompanyName,
                PersonOfContact = user.PersonOfContact,
                CompanyAddress = user.CompanyAddress,
                State = user.State,
                City = user.City,
                CompanyWebsite = user.CompanyWebsite,
            };
            return Json(profile, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UserProfile(string id)
        {
            var user = db.Users.Where(x => x.Id == id).Select(x => new ProfileViewModel()
            {
                PersonOfContact = x.PersonOfContact,
                CompanyName = x.CompanyName,
                CompanyAddress = x.CompanyAddress,
                CompanyWebsite = x.CompanyWebsite,
                State = x.State,
                City = x.City,
                Postal = x.Postal,
                DateRegistered = x.DateRegistered.ToString(),
                AccountNumber = x.AccountNumber,
                BusinessCategory = x.BusinessCategory.ToString(),
                ItemsUpForSale = x.ItemsUpForSale.Select(item => new MarketPlaceItemViewModel(item)).Take(10).ToList()
            });
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult NumberOfNewMembersInYourArea()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var numberOfNewMembers = db.Users.Where(x => x.State == user.State).OrderByDescending(x => x.DateRegistered).ToList().Count();
            return Json(numberOfNewMembers, JsonRequestBehavior.AllowGet);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Portal", "Account");
                //return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            ViewBag.BusinessCategories = new SelectList(db.BusinessCategories.Select(x => new { x.Id, x.CategoryName }), "Id", "CategoryName");

            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            ViewBag.BusinessCategories = new SelectList(db.BusinessCategories.Select(x => new { x.Id, x.CategoryName }), "Id", "CategoryName");

            if (ModelState.IsValid)
            {

                var business = new BusinessUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PersonOfContact = model.PersonOfContact,
                    City = model.City,
                    CompanyAddress = model.CompanyAddress,
                    CompanyName = model.CompanyName,
                    CompanyWebsite = model.CompanyWebsite,
                    Postal = model.Postal,
                    PhoneNumber = model.PhoneNumber,
                    DateRegistered = DateTime.Now,
                    BusinessCategory = db.BusinessCategories.Find(model.BusinessCategory),
                    State = model.State,
                    Balance = 500

                };

                var result = await UserManager.CreateAsync(business, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(business, isPersistent: false, rememberBrowser: false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Portal", "Account");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new BusinessUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}