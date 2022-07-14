using LaundryManagementSystem.Business;
using LaundryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaundryManagementSystem.Controllers
{
    public class InvoiceController : Controller
    {
        PricingImplementation prc = new PricingImplementation();


        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerateInvoice(int Id)
        {
            PriceModel ob = prc.GetAPricesById(Id);
            return View(ob);
        }

        public ActionResult Back()
        {
            Session["Orders"] = null;
            return RedirectToAction("Pricing", "Home");
        }

        
    }
}