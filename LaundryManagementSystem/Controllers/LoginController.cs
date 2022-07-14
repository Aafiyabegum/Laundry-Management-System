using LaundryManagementSystem.Business;
using LaundryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaundryManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        LoginImplementation login = new LoginImplementation();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            LoginModel ob = login.GetAUserByUserName(model);

            if (ob != null)
            {
                System.Web.HttpContext.Current.Session["UserDetails"] = ob;

                var result = new { Result = "Successed", type = ob.Type };
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = false, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult SignUp(LoginModel model)
        {
            if (model.UserName != null)
            {
                int success = login.Insert(model);

                if (success > 0 && Session["Orders"] != null)
                {
                    LoginModel ob = login.GetAUserByUserName(model);
                    System.Web.HttpContext.Current.Session["UserDetails"] = ob;
                    return RedirectToAction("GenerateInvoice", "Invoice", new { Id = Session["Orders"] });
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserDetails"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}