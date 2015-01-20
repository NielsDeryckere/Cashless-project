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
    public class ExistingEmployeeController : ApiController
    {
        public HttpResponseMessage Put(Employee e)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int i = EmployeeDA.NotActiveToActive(e, p.Claims);
            if (i == 0)
            { return new HttpResponseMessage(HttpStatusCode.NoContent); }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
