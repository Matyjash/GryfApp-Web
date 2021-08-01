using Microsoft.AspNetCore.Mvc;
using System;
using Firebase.Auth;
using GryfApp_Web.Models;
using System.Net.Http;

namespace GryfApp_Web.Controllers
{
    public class LoginController : Controller
    {
        private static string ApiKey = "AIzaSyDG_Jhltdmbi6qAmbFHusUUNO0dGx3dPDM";
        private static string Bucket = "https://ehhapp-5467e.firebaseio.com/";
        // GET: Account

        public ActionResult Index()
		{
            return View();
		}

        [HttpPost]
        public ActionResult Login(GryfApp_Web.Models.User user)
		{
            string email = user.email;
            string pss = user.pss;

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDG_Jhltdmbi6qAmbFHusUUNO0dGx3dPDM"));

            var auth = authProvider.SignInWithEmailAndPasswordAsync(email, pss);
            try
            {
                if (auth.Result.User.IsEmailVerified)
                {
                    return View("Login", user);
                }
            } catch (FirebaseAuthException e)
            {
                return View("Index");
            } catch (HttpRequestException e)
			{
                return View("Index");
            }catch(System.AggregateException e)
			{
                return View("Index");
			}

            return View();
			

        }




    }
}
