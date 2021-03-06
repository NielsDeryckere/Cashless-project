﻿using Microsoft.Owin.Security.OAuth;
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WEB_API_2NMCT1.Models;

namespace WEB_API_2NMCT1.App_Start
{
    public class SimpleAuthorizationServerProvider:OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Organisation o = OrganisationDA.CheckCredentials(context.UserName, context.Password);
            if (o == null)
            {
                context.Rejected();
                return Task.FromResult(0);
            }

            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            id.AddClaim(new Claim("dbname", o.DbName));
            id.AddClaim(new Claim("dblogin", o.DbLogin));
            id.AddClaim(new Claim("dbpass", o.DbPassword));
            id.AddClaim(new Claim("login", o.Login));
            id.AddClaim(new Claim("pass", o.Password));

            context.Validated(id);
            return Task.FromResult(0);
        }

    }
}