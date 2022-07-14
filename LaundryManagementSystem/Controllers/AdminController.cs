using LaundryManagementSystem.App_Start;
using LaundryManagementSystem.Business;
using LaundryManagementSystem.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaundryManagementSystem.Controllers
{
    [CustomeFilter]
    public class AdminController : Controller
    {
        PricingImplementation addPrice = new PricingImplementation();
        ContactUsImplementation model = new ContactUsImplementation();
        OrderImplementation order = new OrderImplementation();
        LoginImplementation login = new LoginImplementation();

        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.UserCount = login.AllUsers().ToList().Where(x => x.Type != "Admin").Count();


            var totalorderlist = order.GetAllOrders().ToList();

            ViewBag.TotalNewOrderOrders = totalorderlist.Where(x => x.Status == "NewOrder").Count();
            ViewBag.TotalCompletedOrders = totalorderlist.Where(x => x.Status == "Complete").Count();
            ViewBag.TotalOngoingOrders = totalorderlist.Where(x => x.Status == "Ongoing").Count();
            ViewBag.TotalDeliveredOrders = totalorderlist.Where(x => x.Status == "Delivered").Count();
            ViewBag.CancelledOrders = totalorderlist.Where(x => x.Status == "Cancelled").Count();

            ViewBag.TotalOrders = totalorderlist.Count();

            ViewBag.TodayProfit = ((order.GetTodayProfit(DateTime.Today).Price));
            ViewBag.MonthlyProfit = order.GetMonthlyProfit(DateTime.Today).Price - (totalorderlist.Where(x => x.Status == "Cancelled").Sum(x => x.Price));


            return View();
        }

        public ActionResult Inbox()
        {

            List<ContactusModel> list = model.GetAllInbox().ToList();
            return View(list);
        }

        public ActionResult AddPrice(PriceModel model)
        {
            if (model.Type != null)
            {


                int success = addPrice.Update(model);
                return new JsonResult { Data = success, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            return View();
        }

        public ActionResult GetAPriceListByType(PriceModel model)
        {
            var data = addPrice.GetAllPrices(model);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult Orders()
        {
            var listOfOrders = order.GetAllOrders().ToList();

            //Get All order numbers only
            ViewBag.OrderNo = listOfOrders.Where(x => x.Status != "Cancelled").Select(x => x.OrderId);
            return View(listOfOrders);
        }

        public ActionResult UpdateOrderStatus(OrderModel model)
        {
            int success = order.UpdateStatus(model);
            return new JsonResult { Data = success, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ScheduleDelivery(OrderModel model)
        {
            if (model.OrderId == 0)
            {
                var listOfOrders = order.GetAllOrders().ToList();
                ViewBag.OrderNo = listOfOrders.Where(a => a.Status != "Deliverd" && a.Status!= "Cancelled").Select(x => x.OrderId);
                return View(listOfOrders.Where(a => a.DeliveryDate.ToShortDateString() != "1/1/0001").ToList());
            }

            int success = order.UpdateDeliverDate(model);
            return new JsonResult { Data = success, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public ActionResult Clients()
        {
            var client = login.GetAllUsers().ToList();
            return View(client);
        }

        public ActionResult Reports(string reportname)
        {
            if (reportname == "rpt1")
            {
                return new JsonResult()
                {
                    Data = new { Data = order.GetMonthOrders(DateTime.Today).ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet }
                };
            }

            if (reportname == "rpt2")
            {
                var dt = order.GetAllOrders().Where(x => x.Status == "NewOrder").ToList();
                return new JsonResult()
                {
                    Data = new { Data = dt, JsonRequestBehavior = JsonRequestBehavior.AllowGet }
                };
            }


            if (reportname == "rpt3")
            {
                var dt = order.GetAllOrders().Where(x => x.Status == "Complete").ToList();
                return new JsonResult()
                {
                    Data = new { Data = dt, JsonRequestBehavior = JsonRequestBehavior.AllowGet }
                };
            }

            return View();
        }

        public ActionResult Users()
        {
            return View(login.GetAllUsers().ToList());
        }

        public ActionResult Offers()
        {
            if (TempData["Message"] != null)
                ViewBag.success = TempData["Message"];

            return View();
        }

        public ActionResult AddOffers(OffersModel model)
        {
            if (model.Heading != null)
            {
                OffersImplementation ob = new OffersImplementation();
                TempData["Message"] = ob.Insert(model);

            }
            return RedirectToAction("Offers");
        }


        public ActionResult Logout()
        {
            Session["UserDetails"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
