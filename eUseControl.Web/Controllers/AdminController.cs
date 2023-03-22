using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Extension;
using eUseControl.Models;
using eUseControl.BusinessLogic.DBModel;


namespace eUseControl.Controllers
{
    public class AdminController : BaseController
    {
        // GET: User
        private UserContext context;
        private ProductContext productcontext;

        public AdminController()
        {
            context = new UserContext();
            productcontext = new ProductContext();
        }

        [HttpPost]
        public void delete_by_id(int id)
        {
            context.Users.Where(x => x.Id == id).DeleteFromQuery();
        }

        [HttpPost]
        public void delete_product_by_id(int id)
        {
            productcontext.Products.Where(x => x.Prod_Id == id).DeleteFromQuery();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            delete_by_id(id);
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public ActionResult Delete_Prod(int id)
        {
            delete_product_by_id(id);
            return RedirectToAction("Index", "Admin");
        }


        public ActionResult Index()
        {

            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();
            UserData u = new UserData
            {
                Username = user.Username,
                Email = user.Email,
                Id = user.Id,
                Level = user.Level

            };


            return View(u);
        }

    }
}