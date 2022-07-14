using LaundryManagementSystem.DataAccess;
using LaundryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryManagementSystem.Business
{
    public class OffersImplementation
    {

        public int Insert(OffersModel model)
        {
            var query = "INSERT INTO Offers_News (Heading,Description,Date)VALUES ('" + model.Heading + "','" + model.Description + "',GETDATE())";
            return new DataAccessCls().Execute(query);
        }

        public List<OffersModel> GetAllOffers()
        {
            var query = "select Heading,Description,Date from Offers_News order by Date desc";
            return new DataAccessCls().Query<OffersModel>(query).ToList();
        }



    }
}