using LaundryManagementSystem.DataAccess;
using LaundryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryManagementSystem.Business
{
    public class PricingImplementation
    {
        public int Insert(PriceModel model)
        {
            var query = "INSERT INTO Price (Type, Price, lbs, Day, IsIron, IsPerfume,IsPressing,IsDry )VALUES ('" + model.Type + "','" + model.Price + "','" + model.Lbs + "','" + model.Day + "','" + model.IsIron + "','" + model.Perfume + "','"+model.IsPressing+"','"+model.IsDry+"')";
            return new DataAccessCls().Execute(query);
        }

        public int Update(PriceModel model)
        {
            var query = "UPDATE  Price SET Price ='" + model.Price + "', lbs ='" + model.Lbs + "', Day ='" + model.Day + "', IsIron ='" + model.IsIron  + "', IsPerfume ='" + model.Perfume + "', IsPressing='"+model.IsPressing+ "', IsDry='"+model.IsDry+"' where Type='" + model.Type + "'";
            return new DataAccessCls().Execute(query);
        }


        public PriceModel GetAllPrices(PriceModel model)
        {
            var query = "Select Id,Type, Price, lbs, Day, IsIron, IsPerfume as Perfume, IsPressing, IsDry from Price where Type='" + model.Type + "'";
            return new DataAccessCls().Query<PriceModel>(query).FirstOrDefault();
        }

        public PriceModel GetAPricesById(int id)
        {
            var query = "Select Id,Type, Price, lbs, Day, IsIron, IsPerfume as Perfume,IsPressing, IsDry from Price where Id='" + id + "'";
            return new DataAccessCls().Query<PriceModel>(query).FirstOrDefault();
        }


        public IEnumerable<PriceModel> GetAllPricingDetails()
        {
            var query = "Select Id,Type, Price, lbs, Day, IsIron, IsPerfume as Perfume,IsPressing, IsDry from Price ";
            return new DataAccessCls().Query<PriceModel>(query);
        }
    }
}