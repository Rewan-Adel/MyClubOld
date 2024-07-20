using System;
using System.Web.Mvc;
using SecurityLib.Repositoty;
namespace MyClub.UI.Controllers
{
    //Check auth with jsonResult
    public class AccountController : Controller
    {
        private readonly SecurityRepository _security;

        public AccountController()
        {
            _security = new SecurityRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signup(int? userId, string PersonName, string password, string Gender,  string MobileNumber, string HomePhoneNumber,
                                 string Email, string Address, string Nationality)
        {
            try
            {
                _security.Register(userId, PersonName, password, Gender, MobileNumber, HomePhoneNumber,
                                  Email, Address, Nationality);

                Session["Auth"] = PersonName;
                
                return RedirectToAction("Index", "Home");
               // return Json(new { success = true, message = "Signup successfully." }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }















    }
}