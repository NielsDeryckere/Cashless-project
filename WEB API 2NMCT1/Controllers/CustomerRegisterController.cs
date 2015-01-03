using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using WEB_API_2NMCT1.Models;

namespace WEB_API_2NMCT1.Controllers
{
    public class CustomerRegisterController : ApiController
    {
        public List<Customer> Get()
        {
          
            return CustomerDA.GetCustomers();
        }
    }
}
