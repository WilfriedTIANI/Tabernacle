using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CibleWebForms.Models;
using System.Web.Security;

namespace CibleWebForms.Controllers
{
    public class NavigationController : Controller
    {
        private CibleRhWebAppEntities db = new CibleRhWebAppEntities();


        // GET: Avances
        public async Task<ActionResult> Index()
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            var avances = db.Avances.Where(a => a.IdEmploye == EmployesController.localEmploye.IdEmploye).Include(a => a.Employe).Include(a => a.RespExploitation).Include(a => a.RespPortefeuille).Include(a => a.Directeur);
            return View(await avances.ToListAsync());
        }

        // GET: Navigation/Details/5
        public async Task<ActionResult> Details(string CodeRequete)
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            if (CodeRequete == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avance avance = await db.Avances.FindAsync(CodeRequete);
            if (avance == null)
            {
                return HttpNotFound();
            }
            return View(avance);
        }
        [AllowAnonymous]
        public ActionResult Deconnexion()
        {
            EmployesController.localEmploye = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Connexion", "Employes");
        }
    }
}
