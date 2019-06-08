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
using CibleWebForms.Helpers;

namespace CibleWebForms.Controllers
{
    [Authorize]
    public class EmployesController : Controller
    {
        private CibleRhWebAppEntities db = new CibleRhWebAppEntities();
        public static Employe localEmploye;


        // GET: Employes
        public async Task<ActionResult> Index()
        {
            var employes = db.Employes.Include(e => e.Societe);
            return View(await employes.ToListAsync());
        }

        // GET: Employes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe employe = await db.Employes.FindAsync(id);
            if (employe == null)
            {
                return HttpNotFound();
            }
            return View(employe);
        }

        // GET: Employes/Create
        public ActionResult Create()
        {
            ViewBag.IdSociete = new SelectList(db.Societes, "IdSociete", "RaisonSociale");
            return View();
        }

        // POST: Employes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdEmploye,Matricule,Prenom,Nom,Telephone,IdSociete,Statut")] Employe employe)
        {
            if (ModelState.IsValid)
            {
                db.Employes.Add(employe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdSociete = new SelectList(db.Societes, "IdSociete", "RaisonSociale", employe.IdSociete);
            return View(employe);
        }

        // GET: Employes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe employe = await db.Employes.FindAsync(id);
            if (employe == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSociete = new SelectList(db.Societes, "IdSociete", "RaisonSociale", employe.IdSociete);
            return View(employe);
        }

        // POST: Employes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdEmploye,Matricule,Prenom,Nom,Telephone,IdSociete,Statut")] Employe employe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdSociete = new SelectList(db.Societes, "IdSociete", "RaisonSociale", employe.IdSociete);
            return View(employe);
        }

        // GET: Employes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe employe = await db.Employes.FindAsync(id);
            if (employe == null)
            {
                return HttpNotFound();
            }
            return View(employe);
        }

        // POST: Employes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employe employe = await db.Employes.FindAsync(id);
            db.Employes.Remove(employe);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #region Ajout Personnels


        // GET: Employes/Enregistrement
        [AllowAnonymous]
        public ActionResult Enregistrement()
        {
            ViewBag.IdSociete = new SelectList(db.Societes, "IdSociete", "RaisonSociale");
            return View();
        }

        // POST: Employes/Enregistrement
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enregistrement([Bind(Include = "Matricule,Prenom,Nom,Telephone,IdSociete,Email,Pwd")] Employe employe)
        {
            if (ModelState.IsValid)
            {
                employe.Statut = AppConstants.EmployeStatuts.SALARIE;
                employe.DateEnregistrement = DateTime.Now;
                db.Employes.Add(employe);
                await db.SaveChangesAsync();
                if(employe.IdEmploye > 0)
                    EmployesController.localEmploye = employe;
                return RedirectToAction("Index","Navigation");
            }

            ViewBag.IdSociete = new SelectList(db.Societes, "IdSociete", "RaisonSociale", employe.IdSociete);
            return View(employe);
        }



        // GET: Employes/Connexion
        [AllowAnonymous]
        public ActionResult Connexion()
        {
            return View();
        }


        // POST: Employes/Connexion
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Connexion([Bind(Include = "Matricule,Pwd")] Employe tmp,String ReturnUrl)
        {
            if (String.IsNullOrEmpty(tmp.Matricule)| String.IsNullOrEmpty(tmp.Pwd))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe employe = await db.Employes.FirstOrDefaultAsync(e => e.Matricule == tmp.Matricule && e.Pwd == tmp.Pwd);
            if (employe == null )
            {
                return RedirectToAction("Connexion", "Emplpoyes");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(employe.Nom + " " + employe.Prenom + " (" + employe.Matricule + ")", false);
                localEmploye = employe;
            }
            if (Url.IsLocalUrl(ReturnUrl))
                return Redirect(ReturnUrl);
            else
                return RedirectToAction("Index", "Navigation");
        }


        // Employes/Connexion
        public async Task<ActionResult> DashBoard(Employe employeSession)
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            if (employeSession == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var anvances = await db.Avances.Where(a => a.IdEmploye == employeSession.IdEmploye).ToListAsync();
            return View();
        }

        #endregion


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
