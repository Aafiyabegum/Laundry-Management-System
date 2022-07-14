using LaundryManagementSystem.DataAccess;
using LaundryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryManagementSystem.Business
{
    public class LoginImplementation
    {

        public int Insert(LoginModel model)
        {
            var query = "INSERT INTO Login (FirstName,LastName,UserName,Password,EmailId,Type,RegisterDate )VALUES ('" + model.FirstName + "','" + model.LastName + "','" + model.UserName + "','" + model.Password + "','" + model.EmailId + "','User',GETDATE())";
            return new DataAccessCls().Execute(query);
        }

        public LoginModel GetAUserByUserName(LoginModel model)
        {
            var query = "select UserId,FirstName,LastName,UserName,EmailId,Type from Login where UserName='" + model.UserName + "' and Password='" + model.Password + "'";
            return new DataAccessCls().Query<LoginModel>(query).FirstOrDefault();
        }

        public IEnumerable<LoginModel> GetAllUsers()
        {
            var query = "select UserId,FirstName,LastName,UserName,EmailId,Type, RegisterDate from Login where Type='User'";
            return new DataAccessCls().Query<LoginModel>(query);
        }

        public IEnumerable<LoginModel> AllUsers()
        {
            var query = "select UserId,FirstName,LastName,UserName,EmailId,Type from Login";
            return new DataAccessCls().Query<LoginModel>(query);
        }
    }
}