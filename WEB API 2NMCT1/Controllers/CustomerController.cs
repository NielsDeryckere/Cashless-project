using models;
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
    [Authorize]
    public class CustomerController : ApiController
    {
        public List<Customer> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return CustomerDA.GetCustomers(p.Claims);
        }

        public HttpResponseMessage Post(Customer c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int id = CustomerDA.InsertCustomer(c, p.Claims);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(id.ToString());
            return message;
        }

        public HttpResponseMessage Put(Customer c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int i=CustomerDA.UpdateCustomer(c, p.Claims);
            if(i==0)
            { return new HttpResponseMessage(HttpStatusCode.NoContent); }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(long id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int i=CustomerDA.DeleteCustomer(id, p.Claims);
            if (i == 0) { return new HttpResponseMessage(HttpStatusCode.NoContent); }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
