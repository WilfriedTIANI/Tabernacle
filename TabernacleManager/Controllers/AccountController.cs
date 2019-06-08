using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TabernacleManager.Helpers;
using TabernacleManager.Models;

namespace TabernacleManager.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account
        public ActionResult Connexion()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index", "Manager");
            }
            return View();
        }


        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Connexion(string login, string pwd, string returnUrl)
        {
            var user = UtilisateurHelper.Authentifier(login, pwd);
            if (user == null)
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.IdUtilisateur.ToString(), false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Manager");
                }
            }
            return View();
        }
        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Connexion", "Account");
        }
    }



}