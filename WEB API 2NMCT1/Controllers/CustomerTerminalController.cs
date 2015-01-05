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
    public class CustomerTerminalController : ApiController
    {
        public HttpResponseMessage Post(Customer c)
        {
           
            int id = CustomerDA.InsertCustomer(c);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(id.ToString());
            return message;
        }
    }
}
