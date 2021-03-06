﻿using models;
using SSAPPmcv.DataAcces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSAPPmcv.Controllers
{
    [Authorize]
    public class RegisterManagementController : Controller
    {
        // GET: RegisterManagement
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Register r)
        {
            if (ModelState.IsValid) { 
            RegisterDA.InsertRegister(r);
            return RedirectToAction("Index");
            }
            return View("New");
        }

        public ActionResult ShowAvailableRegisters()
        {
            
            List<Register> lijst = RegisterDA.GetRegisters();
            if (lijst.Count>0) {
           
            return View(lijst); 
            }
            else
            {

                return View("Error");
            }
          
        }

        [HttpGet]
        public ActionResult AssignRegister(int id)
        {
            List<Organisation> list = OrganisationDA.GetOrganisations();
            foreach(Organisation o in list)
            {
                o.Login = Cryptography.Decrypt(o.Login);
                o.Password = Cryptography.Decrypt(o.Password);
                o.DbLogin = Cryptography.Decrypt(o.DbLogin);
                o.DbName = Cryptography.Decrypt(o.DbName);
                o.DbPassword = Cryptography.Decrypt(o.DbPassword);

            }
            ViewBag.Register = id;
            return View(list);
        }

        [HttpGet]
        public ActionResult AssignToOrganisation(int regid,int orgid)
        {


            RegistersOrganisation ro = new RegistersOrganisation() { RegisterId = regid, Organisationid = orgid, From = DateTime.Now.Date, Untill = DateTime.Now.Date.AddYears(10) };
            Register_OrganisationDA.InsertOrganisation_Register(ro);
            Register_OrganisationDA.ModifyRegisterAssignValue(regid);
            Organisation o=OrganisationDA.GetOrganisations(orgid);
            Register r=RegisterDA.GetRegisters(regid);


            RegisterClientDAcs.ChangeConnectionString("System.Data.SqlClient", "MCT-NIELS"+@"\DATAMANAGEMENT", Cryptography.Decrypt(o.DbName), Cryptography.Decrypt(o.DbLogin), Cryptography.Decrypt(o.DbPassword));
             RegisterClientDAcs.InsertRegister(r);



            return View("Succeeded");
           
        }
        [HttpGet]
        public ActionResult UnAssignRegister(int regid,int orgid)
        {
            Register_OrganisationDA.DeleteRegistersOrganisation(regid,orgid);
            Register_OrganisationDA.ModifyRegisterAssignValueToFalse(regid);
            

            return RedirectToAction("ShowAssignedRegisters");


        }

        [HttpGet]
        public ActionResult ShowAssignedRegisters()
        {
           
                List<Organisation> oList = OrganisationDA.GetOrganisations();
            List<RegistersOrganisation> orList = Register_OrganisationDA.GetOrganisation_Registers();
            List<Register> registers=new List<Register>();




            List<Register>[] rList = new List<Register>[oList.Count]; int i = 0;
            for (i = 0; i < rList.Length; i++)
            {
                rList[i] = new List<Register>();
            }
            i = 0;

            foreach (Organisation o in oList)
            {
                foreach (RegistersOrganisation or in orList)
                    if (or.Organisationid == o.ID)
                        rList[i].Add(RegisterDA.GetRegisters(or.RegisterId));
                i++;
            }

            ViewBag.Organisations = oList;
            ViewBag.Registers = rList;
            return View();
        }

        public ActionResult ViewErrorlog(int regid,int orgid)
        {
            Organisation o = new Organisation();
            o = OrganisationDA.GetOrganisations(orgid);
            ViewBag.register = regid;
            ErrorlogDA.ChangeConnectionString("System.Data.SqlClient", "MCT-NIELS" + @"\DATAMANAGEMENT", Cryptography.Decrypt(o.DbName), Cryptography.Decrypt(o.DbLogin), Cryptography.Decrypt(o.DbPassword));
            List<Errorlog> errorlist = new List<Errorlog>();
            errorlist = ErrorlogDA.GetErrorlogs(regid);

            return View(errorlist);


        }

    }
}