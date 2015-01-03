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
    public class RegisterController : ApiController
    {
        public List<RegistersOrganisation> Get(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return RegisterDA.GetRegisters(id, p.Claims);

        }

       
    }
}
