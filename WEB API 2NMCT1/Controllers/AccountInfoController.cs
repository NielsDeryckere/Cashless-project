﻿using models;
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
    public class AccountInfoController : ApiController
    {
        public Organisation Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return AccountInfoDA.GetAccountInfo(p.Claims);
        }

        public HttpResponseMessage Put(Organisation o, string pass)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            AccountInfoDA.UpdateOrganisation(o,pass, p.Claims);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

