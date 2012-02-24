using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security.Principal;
using System.Web.Security;
using System.Web.UI;

namespace VinayakSuleyDotCom.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl)
        {
            MembershipProvider membershipProvider = Membership.Provider;

            if (!ValidateLogOnInformation(membershipProvider, userName, password))
            {
                ViewData["rememberMe"] = rememberMe;
                return View();
            }

            // Make sure we have the username with the right capitalization
            // since we do case sensitive checks for OpenID Claimed Identifiers later.
            userName = membershipProvider.GetUser(userName, true).UserName;

            FormsAuthenticationTicket authTicket = new
                            FormsAuthenticationTicket(1, //version
                            userName, // user name
                            DateTime.Now,             //creation
                            DateTime.Now.AddMinutes(30), //Expiration
                            rememberMe, //Persistent
                            userName); //since Classic logins don't have a "Friendly Name"

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool ValidateLogOnInformation(MembershipProvider membershipProvider, string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            if (!membershipProvider.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }

            return ModelState.IsValid;
        }

        private void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        private void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
