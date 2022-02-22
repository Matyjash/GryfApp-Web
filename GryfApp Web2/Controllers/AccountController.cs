using Firebase.Auth;
using GryfApp_Web2.Helpers;
using GryfApp_Web2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GryfApp_Web2.Controllers
{
    public class AccountController : Controller
    {
        private static string ApiKey = "AIzaSyDG_Jhltdmbi6qAmbFHusUUNO0dGx3dPDM";
        private static string Bucket = "";
        // GET: Login
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(SignUpModel model)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));

                var a = await auth.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password, model.Name, true);
                ModelState.AddModelError(string.Empty, "Wrong email or password!");
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                   return RedirectToAction("Index","Home");
                }
            }
            catch(Exception e)
            {
                Console.Write(e);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login (LoginModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                    var ab = await auth.SignInWithEmailAndPasswordAsync(model.Email, model.Password);
                    string token = ab.FirebaseToken;
                    var user = ab.User;

                    if(token != "")
                    {
                        this.SignInUser(user.Email, token, false);
                        CurrentUser.userId = user.LocalId;
                        CurrentUser.logged = true;
                        return this.RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Nieprawidłowe dane!");
                    }
                }
            }catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, "Błąd logowania! Sprawdź poprawność danych!");
                Console.Write(e);
            }
            return this.View();
        }

        private void SignInUser(string email, string token, bool isPersistent)
        {
            var claims = new List<Claim>();

            try
            {
                claims.Add(new Claim(ClaimTypes.Email, email));
                claims.Add(new Claim(ClaimTypes.Authentication, token));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;

                authenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
                
               

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private void ClaimIdentities(string userName, bool isPersistent)
        {
            var claims = new List<Claim>();
            try
            {
                claims.Add(new Claim(ClaimTypes.Name, userName));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if(Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return this.RedirectToAction("LogOff", "Account");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            CurrentUser.logged = false;
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
    }
}