using MicroLab.Dto;
using MicroLab.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroLab.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session["userId"] = null;
            return RedirectToAction("../Home/Index");
        }
        public JsonResult LoginAPI(UserDto u)
        {
            var res = UserService.Login(u);
            if (res.Success)
            {
                Session["userId"] = res.Result.Id;
            }
            return new JsonResult { Data = res };
        }
        public JsonResult SignUpAPI(UserDto u)
        {
            var res = UserService.SignUp(u);
            if (res.Success)
            {
                Session["userId"] = res.Result.Id;
            }
            return new JsonResult { Data = res };
        }


    }
}