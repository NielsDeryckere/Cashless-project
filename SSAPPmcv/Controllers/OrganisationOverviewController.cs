using models;
using SSAPPmcv.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSAPPmcv.Controllers
{
    [Authorize]
    public class OrganisationOverviewController : Controller
    {
        // GET: OrganisationOverview
        public ActionResult ListOrganisations()
        {
            List<Organisation> lstOrganisations = OrganisationDA.GetOrganisations();
            ViewBag.Organisations = lstOrganisations;
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Organisation o)
        {
            if(ModelState.IsValid)
            { 
            OrganisationDA.InsertOrganisation(o);
            return RedirectToAction("ListOrganisations");
            }
            else { return View("New"); }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Organisation o = OrganisationDA.GetOrganisations(id);
            o.Login = Cryptography.Decrypt(o.Login);
            o.Password = Cryptography.Decrypt(o.Password);
            o.DbName = Cryptography.Decrypt(o.DbName);
            o.DbLogin = Cryptography.Decrypt(o.DbLogin);
            o.DbPassword = Cryptography.Decrypt(o.DbPassword);
            return View(o);
        }

        [HttpPost]
        public ActionResult Edit(Organisation o)
        {
            OrganisationDA.EditOrganisation(o);
            return RedirectToAction("ListOrganisations");
        }

      
        public ActionResult Delete(int id, string dbname,string dblogin)
        {
            OrganisationDA.DeleteOrganisation(id,dbname,dblogin);
            return RedirectToAction("ListOrganisations");

        }
    }
}