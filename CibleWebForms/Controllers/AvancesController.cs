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

namespace CibleWebForms.Controllers
{
    public class AvancesController : Controller
    {
        private CibleRhWebAppEntities db = new CibleRhWebAppEntities();
        private Employe employe;

        // GET: Avances
        public async Task<ActionResult> Index()
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            var avances = db.Avances.Include(a => a.Employe).Include(a => a.RespExploitation).Include(a => a.RespPortefeuille).Include(a => a.Directeur);
            return View(await avances.ToListAsync());
        }

        // GET: Avances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avance avance = await db.Avances.FindAsync(id);
            if (avance == null)
            {
                return HttpNotFound();
            }
            return View(avance);
        }

        // GET: Avances/Create
        public ActionResult Create()
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            ViewBag.IdEmploye = new SelectList(db.Employes, "IdEmploye", "Matricule");
            ViewBag.IdRE = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom");
            ViewBag.IdRP = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom");
            ViewBag.IdDIR = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom");
            return View();
        }

        // POST: Avances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdRequete,CodeRequete,IdEmploye,Montant,Pourcentage,DelaiEnMois,MotifRequete,DateRequete,Statut,MotifAnnulation,MotifRejet,IdRP,TraitementRP,DateTraitementRP,IdRE,TraitementRE,DateTraitementRE,IdDIR,TraitementDIR,DateTraitementDIR,Comptabilise,AvanceEnvoyee")] Avance avance)
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            if (ModelState.IsValid)
            {
                db.Avances.Add(avance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmploye = new SelectList(db.Employes, "IdEmploye", "Matricule", avance.IdEmploye);
            ViewBag.IdRE = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdRE);
            ViewBag.IdRP = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdRP);
            ViewBag.IdDIR = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdDIR);
            return View(avance);
        }

        // GET: Avances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avance avance = await db.Avances.FindAsync(id);
            if (avance == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmploye = new SelectList(db.Employes, "IdEmploye", "Matricule", avance.IdEmploye);
            ViewBag.IdRE = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdRE);
            ViewBag.IdRP = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdRP);
            ViewBag.IdDIR = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdDIR);
            return View(avance);
        }

        // POST: Avances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdRequete,CodeRequete,IdEmploye,Montant,Pourcentage,DelaiEnMois,MotifRequete,DateRequete,Statut,MotifAnnulation,MotifRejet,IdRP,TraitementRP,DateTraitementRP,IdRE,TraitementRE,DateTraitementRE,IdDIR,TraitementDIR,DateTraitementDIR,Comptabilise,AvanceEnvoyee")] Avance avance)
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            if (ModelState.IsValid)
            {
                db.Entry(avance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmploye = new SelectList(db.Employes, "IdEmploye", "Matricule", avance.IdEmploye);
            ViewBag.IdRE = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdRE);
            ViewBag.IdRP = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdRP);
            ViewBag.IdDIR = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdDIR);
            return View(avance);
        }

        // GET: Avances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avance avance = await db.Avances.FindAsync(id);
            if (avance == null)
            {
                return HttpNotFound();
            }
            return View(avance);
        }

        // POST: Avances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            Avance avance = await db.Avances.FindAsync(id);
            db.Avances.Remove(avance);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        #region Modifications Personnelles




        // GET: Avances/Create
        [AllowAnonymous]
        public ActionResult DemanderAvance()
        {
            ViewBag.IdEmploye = new SelectList(db.Employes, "IdEmploye", "Matricule");
            ViewBag.IdRE = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom");
            ViewBag.IdRP = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom");
            ViewBag.IdDIR = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom");
            return View();
        }

        // POST: Avances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DemanderAvance([Bind(Include = "Pourcentage,DelaiEnMois,MotifRequete")] Avance avance)
        {
            if(EmployesController.localEmploye == null)
                return RedirectToAction("Connexion", "Employes");
            if (ModelState.IsValid)
            {
                avance.IdEmploye = EmployesController.localEmploye.IdEmploye;
                avance.CodeRequete = Helpers.AvanceHelper.GenerateCodeAvance(EmployesController.localEmploye.Matricule);
                avance.DateRequete = DateTime.Now;
                avance.Statut = AppConstants.AvanceStatuts.REQUETEENATTENTE;
                avance.Comptabilise = false;
                avance.AvanceEnvoyee = false;
                db.Avances.Add(avance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Navigation");
            }

            ViewBag.IdEmploye = new SelectList(db.Employes, "IdEmploye", "Matricule", avance.IdEmploye);
            ViewBag.IdRE = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdRE);
            ViewBag.IdRP = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdRP);
            ViewBag.IdDIR = new SelectList(db.Utilisateurs, "IdUtilisateur", "Prenom", avance.IdDIR);
            return View(avance);
        }







        #endregion


    }
}
