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
    public class StatisticsController : ApiController
    {
        public List<Sales> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return StatisticsDA.GetSales(p.Claims);

        }

        public Product Get(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return ProductsDA.GetProduct(id,p.Claims);

        }
    }
}
