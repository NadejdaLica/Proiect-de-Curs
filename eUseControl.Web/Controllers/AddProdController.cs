using System;
using System.Collections.Generic;
using System.Web.Mvc;
using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User;
using eUseControl.Helpers;
using eUseControl.Models;


namespace eUseControl.Controllers
{
    public class AddProdController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        private readonly ISession _session;
        public AddProdController()
        {
            var bl = new BusinessLogic.BussinesLogic();
            _session = bl.GetSessionBL();
        }
        [HttpPost]
        public ActionResult Index(ProductRegister data)
        {
            if (ModelState.IsValid)
            {
                UProductData product = new UProductData()
                {
                    Prod_Desc = data.Prod_Desc,
                    Prod_Name = data.Prod_Name,
                    Prod_Price = data.Prod_Price,
                    LastEditTime = DateTime.Now
                };
                var productRegistration = _session.ProdRegister(product);
                if (productRegistration.Status)
                {
                    ViewBag.Name = "Successful product registration!";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", productRegistration.StatusMsg);
                    return View();
                }
            }
            return View();
        }
    }
}