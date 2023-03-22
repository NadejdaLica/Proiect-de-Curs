using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Helpers;
using eUseControl.Models;


namespace eUseControl.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        private readonly ISession _session;
        public RegisterController()
        {
            var bl = new BusinessLogic.BussinesLogic();
            _session = bl.GetSessionBL();
        }
        [HttpPost]
        public ActionResult Index(UserRegister data)
        {
            if (ModelState.IsValid)
            {
                URegisterData login = new URegisterData
                {
                    Email = data.Email,
                    Username = data.Username,
                    Password = LoginHelper.HashGen(data.Password),
                    RegisterDateTime = DateTime.Now
                };
                var userRegistration = _session.UserRegister(login);
                if (userRegistration.Status)
                {
                    ViewBag.Name = "Successful registration!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userRegistration.StatusMsg);
                    return View();
                }
            }
            return View();
        }
    }
}