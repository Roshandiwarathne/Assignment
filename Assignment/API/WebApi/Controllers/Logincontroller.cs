using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class Logincontroller : ApiController
    {
        [Route("Api/Login/UserLogin")]
        [HttpPost]
        public Response Login(Login login)
        {
            Students_DBEntities3 stdentities = new Students_DBEntities3();
            var Obj = stdentities.SP_Login(login.UserName, login.Password).ToList<SP_Login_Result>().FirstOrDefault();
            if (Obj.Status != 0) return new Response
            {
                Status = "Invalid",
                Message = "Invalid User."
            };
            if (Obj.Status == -1) return new Response
            {
                Status = "Inactive",
                Message = "User Inactive."
            };
            else return new Response
            {
                Status = "Success",
                Message = login.UserName
            };
        }
        //For new user Registration  
        [Route("Api/Login/UserRegistration")]
        [HttpPost]
        public object createcontact(Registration reg)
        {
            try
            {
                Students_DBEntities3 db = new Students_DBEntities3();
                StudentMaster em = new StudentMaster();
                if (em.Id == 0)
                {
                    em.UserName = reg.UserName;
                    em.LoginName = reg.LoginName;
                    em.Password = reg.Password;
                    em.Email = reg.Email;
                    em.ContactNo = reg.ContactNo;
                    em.Address = reg.Address;
                    em.IsApporved = reg.IsApporved;
                    em.Status = reg.Status;
                    db.StudentMasters.Add(em);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "SuccessFully Saved."
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return new Response
            {
                Status = "Error",
                Message = "Invalid Data."
            };
        }
    }
}
