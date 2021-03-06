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
           Organisation test=AccountInfoDA.GetAccountInfo(p.Claims);
           return test;
        }

        public HttpResponseMessage Put(int id, string pass)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int i=AccountInfoDA.UpdateOrganisation(id,pass,p.Claims);

            if(i==0)
            { return new HttpResponseMessage(HttpStatusCode.NoContent); }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

