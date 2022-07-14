using LaundryManagementSystem.DataAccess;
using LaundryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryManagementSystem.Business
{
    public class ContactUsImplementation
    {
        // Insert method to add the details to the contact us page
        public int Insert(ContactusModel model)
        {
            //Insert query to add the deatils in the form
            var query = "INSERT INTO [ContactUs](Name,EmailId,PhoneNo, Subject, Message, Date)" +
                "VALUES('" + model.Name + "','" + model.EmailId + "','"+model.PhoneNo+"','" + 
                model.Subject + "','" + model.Message + "',GETDATE())";
            return new DataAccessCls().Execute(query);
        }

        public IEnumerable<ContactusModel> GetAllInbox()
        {
            var query = "Select  [Id] as ContatcId,[Name],[EmailId],[PhoneNo],[Subject],[Message],[Date] FROM [ContactUs] order by Date desc";
            return new DataAccessCls().Query<ContactusModel>(query);
        }

    }
}