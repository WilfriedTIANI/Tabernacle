using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TabernacleManager.Helpers;
using TabernacleManager.Models;

namespace TabernacleManager.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        
        public ActionResult Index()
        {
            ;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = TabernacleManagerViewHelper.GetTmView(HttpContext.User.Identity.Name);
                return View(user);
            }else
                return View();
        }
        
        public ActionResult AvanceDetails(string CodeRequete)
        {
            var user = TabernacleManagerViewHelper.GetTmViewByAvanceId(HttpContext.User.Identity.Name, CodeRequete);
            return View(user);
        }

        // GET: Avances/Delete/5
        public ActionResult RejetterAvance(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (!TabernacleManagerViewHelper.RejecterAvanceById(id, HttpContext.User.Identity.Name))
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        
        public ActionResult AvancesByStatus(string statut)
        {
            ;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = TabernacleManagerViewHelper.GetTmViewByAvanceStatut(HttpContext.User.Identity.Name,statut);
                return View(user);
            }
            else
                return HttpNotFound();
        }
    }
}