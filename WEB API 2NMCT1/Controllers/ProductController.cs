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
    public class ProductController : ApiController
    {
        public List<Product> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return ProductsDA.GetProducts(p.Claims);
        
        }

         public HttpResponseMessage Post(Product c)
         {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int id = ProductsDA.InsertProduct(c, p.Claims);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(id.ToString());
            return message;
         }

        public HttpResponseMessage Delete(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            ProductsDA.DeleteProduct(id, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        public HttpResponseMessage Put(Product c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            ProductsDA.UpdateProduct(c, p.Claims);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }

   
}
