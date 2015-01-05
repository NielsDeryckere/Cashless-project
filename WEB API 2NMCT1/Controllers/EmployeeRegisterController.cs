using modelsProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WEB_API_2NMCT1.Models;

namespace WEB_API_2NMCT1.Controllers
{
  
    public class EmployeeRegisterController : ApiController
    {
        public HttpResponseMessage Post(int RegisterId, long EmployeeId, DateTime from, DateTime untill)
        {
           
            int id = RegisterDA.InsertEmployeeRegister(RegisterId, EmployeeId, from, untill);


            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(id.ToString());
            return message;
        }

        public List<EmployeeRegister> Get(int id)
        {
             ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;

             return RegisterDA.GetRegisterEmployee(id, p.Claims);
        }
    }
}
