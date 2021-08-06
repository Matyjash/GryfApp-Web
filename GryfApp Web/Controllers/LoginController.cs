using Microsoft.AspNetCore.Mvc;
using System;
using Firebase.Auth;
using GryfApp_Web.Models;
using System.Net.Http;

namespace GryfApp_Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
		{

            return View();
		}

        public ActionResult Login()
		{   
            return View();
        }




    }
}
