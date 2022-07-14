using LaundryManagementSystem.Business;
using LaundryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaundryManagementSystem.Controllers
{
    public class HomeController : Controller
    {

      
        PricingImplementation pricing = new PricingImplementation();
        OffersImplementation offers = new OffersImplementation();

        public ActionResult Index()
        {
            var offer = offers.GetAllOffers();
            if (offer.Count() > 0 )
                ViewBag.Offers = offer;
            else
                ViewBag.Offers = null;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact(ContactusModel model)
        {
            if (model.Name != null)
            {
                //creating the object to cantactus 
                ContactUsImplementation contactUs = new ContactUsImplementation();
                //calling the insert method
                int success = contactUs.Insert(model);
                return new JsonResult { Data = success, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return View();
        }

        public ActionResult Pricing()
        {
            List<PriceModel> ob = pricing.GetAllPricingDetails().ToList();
            return View(ob);
        }

        public ActionResult Order(int Id)
        {
            if (Session["UserDetails"] == null)
            {
                System.Web.HttpContext.Current.Session["Orders"] = Id;
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("GenerateInvoice", "Invoice", new { Id = Id });

            }


        }



    }
}