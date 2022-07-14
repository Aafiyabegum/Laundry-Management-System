using LaundryManagementSystem.DataAccess;
using LaundryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryManagementSystem.Business
{
    public class OrderImplementation
    {

        public int Insert(OrderModel model)
        {
            var query = "INSERT INTO [Order](Price, Type, UserId, OrderDate,Status,LastUpdatedDate)VALUES('" + model.Price + "','" + model.Type + "','" + model.UserId + "',GETDATE(),'" + model.Status + "',GETDATE())";
            return new DataAccessCls().Execute(query);
        }


        public IEnumerable<OrderModel> GetAllOrdersByUser()
        {
            var query = "Select OrderId,Price, Type, UserId, OrderDate,Status,LastUpdatedDate,DeliveryDate from [Order] where UserId='" + ((LoginModel)System.Web.HttpContext.Current.Session["UserDetails"]).UserId + "' order by OrderDate Desc ";
            return new DataAccessCls().Query<OrderModel>(query);
        }


        public IEnumerable<OrderModel> GetAllOrders()
        {
            var query = "Select OrderId,Price, I.Type, I.UserId, OrderDate,L.FirstName, L.LastName, Status,LastUpdatedDate," +
                "DeliveryDate from [Order] as I,[dbo].[Login] as L where I.UserId=L.UserId order by OrderDate Desc ";
            return new DataAccessCls().Query<OrderModel>(query);
        }

        public int UpdateStatus(OrderModel model)
        {
            var query = "UPDATE [Order] SET Status ='" + model.Status + "' ,LastUpdatedDate=GETDATE()  where OrderId=" + model.OrderId + "";
            return new DataAccessCls().Execute(query);
        }


        public int UpdateDeliverDate(OrderModel model)
        {
            var query = "UPDATE [Order] SET DeliveryDate ='" + model.DeliveryDate + "', LastUpdatedDate=GETDATE()  where OrderId=" + model.OrderId + "";
            return new DataAccessCls().Execute(query);
        }


        public OrderModel GetTodayProfit(DateTime dt)
        {
            var query = "Select SUM(Price) as Price from [Order] where OrderDate between '" + dt.ToShortDateString() + "' and '" + dt.AddDays(1).ToShortDateString() + "' and Status !='Cancelled' ";
            return new DataAccessCls().Query<OrderModel>(query).FirstOrDefault();
        }

        public OrderModel GetMonthlyProfit(DateTime dt)
        {
            var query = "Select SUM(Price) as Price from [Order] where OrderDate between '" + new DateTime(dt.Year,dt.Month,1) + "' and '" + dt.AddDays(30).ToShortDateString() + "' ";
            return new DataAccessCls().Query<OrderModel>(query).FirstOrDefault();
        }

        public IEnumerable<OrderModel> GetMonthOrders(DateTime date)
        {
            var query = "Select OrderId,Price, I.Type, I.UserId, OrderDate,L.FirstName, L.LastName, Status,LastUpdatedDate,DeliveryDate from [Order] as I,[dbo].[Login] as L where I.UserId=L.UserId and OrderDate between '" + date.AddDays(-30).ToShortDateString() + "' and '" + date.ToShortDateString() + "' order by OrderDate Desc ";
            return new DataAccessCls().Query<OrderModel>(query);
        }

    }
}