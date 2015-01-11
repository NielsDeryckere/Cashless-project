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
    public class ErrorlogController : ApiController
    {
       

        public HttpResponseMessage Post(Errorlog e)
        {
           
            int id = ErrorlogDA.InsertErrorlog(e);
            if(id==0)
            { return new HttpResponseMessage(HttpStatusCode.NoContent); }
            else { return new HttpResponseMessage(HttpStatusCode.OK);
                }
           
           
            
        }
    }
}
