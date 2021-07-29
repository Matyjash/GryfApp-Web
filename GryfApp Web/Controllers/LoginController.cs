using Microsoft.AspNetCore.Mvc;
using System;
using Firebase.Auth;
using GryfApp_Web.Models;

namespace GryfApp_Web.Controllers
{
    public class LoginController : Controller
    {
        string email;
        string pss;
        bool isValid = false;


        public ActionResult Index()
		{
           
            return View();
		}

        [HttpPost]
        public ActionResult Login(GryfApp_Web.Models.User user)
		{
            string email = user.email;
            string pss = user.pss;
           

            connectToFireBase(email, pss);

            if (isValid == false)
            {
                System.Diagnostics.Debug.WriteLine("post");
                return View(nameof(Login));
			}
			else
			{
                return View(user);
			}
		}
        
 
       
       


        public void getUserInput()
		{

		}
        private async void connectToFireBase(string email, string pss)
		{
			
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDG_Jhltdmbi6qAmbFHusUUNO0dGx3dPDM"));

                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email,pss);

                isValid = true;
            
		}
    }
}
