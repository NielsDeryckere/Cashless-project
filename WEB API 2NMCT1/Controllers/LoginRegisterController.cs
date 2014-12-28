using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEB_API_2NMCT1.Models;

namespace WEB_API_2NMCT1.Controllers
{
    public class LoginRegisterController : ApiController
    {
        public List<Employee>Get()
        {
            return EmployeeDA.GetEmployees();
           
        }
        
    }
}
