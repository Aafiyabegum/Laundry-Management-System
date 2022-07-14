using LaundryManagementSystem.App_Start;
using LaundryManagementSystem.Business;
using LaundryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaundryManagementSystem.Controllers
{
    [CustomeFilter]
    public class UserController : Controller
    {
        OrderImplementation order = new OrderImplementation();
        PricingImplementation price = new PricingImplementation();

        // GET: User
        public ActionResult Index()
        {
            var totalorderlist = order.GetAllOrders().ToList();
            ViewBag.NewOrderOrders = totalorderlist.Where(x => x.Status == "NewOrder" & x.UserId== ((LoginModel)System.Web.HttpContext.Current.Session["UserDetails"]).UserId).Count();
            ViewBag.CompletedOrders = totalorderlist.Where(x => x.Status == "Complete" & x.UserId == ((LoginModel)System.Web.HttpContext.Current.Session["UserDetails"]).UserId).Count();
            ViewBag.OngoingOrders = totalorderlist.Where(x => x.Status == "Ongoing" & x.UserId == ((LoginModel)System.Web.HttpContext.Current.Session["UserDetails"]).UserId).Count();
            ViewBag.DeliveredOrders = totalorderlist.Where(x => x.Status == "Delivered" & x.UserId == ((LoginModel)System.Web.HttpContext.Current.Session["UserDetails"]).UserId).Count();
            ViewBag.TotOrders = totalorderlist.Where(x=>x.UserId==((LoginModel)System.Web.HttpContext.Current.Session["UserDetails"]).UserId).Count();
            ViewBag.CancelledOrder= totalorderlist.Where(x => x.Status == "Cancelled" & x.UserId == ((LoginModel)System.Web.HttpContext.Current.Session["UserDetails"]).UserId).Count();

            return View();
        }

        public ActionResult AddOrders(int Id, int userId)
        {
            if (Id > 0)
            {
                PriceModel ob = price.GetAPricesById(Id);

                int success = order.Insert(new OrderModel
                {
                    Price = ob.Price,
                    Type = ob.Type,
                    Status = "NewOrder",
                    UserId = userId

                });

                Session["Orders"] = null;
            }
            return RedirectToAction("Orders");
        }

        public ActionResult Orders()
        {
      
            var listOfOrders = order.GetAllOrdersByUser().ToList();

            return View(listOfOrders);

        }

        public ActionResult Delivery()
        {
            var result = order.GetAllOrdersByUser().Where(x => x.DeliveryDate != null).ToList();
            return View(result);
        }

        public ActionResult Cancel(int id)
        {
            OrderModel md = new OrderModel();
            md.Status = "Cancelled";
            md.OrderId = id;

            order.UpdateStatus(md);
            return RedirectToAction("Orders");
        }


        public ActionResult Logout()
        {
            Session["UserDetails"] = null;
            return RedirectToAction("Index", "Home");
        }


    }
}