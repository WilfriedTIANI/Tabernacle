using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TabernacleManager.Models;

namespace TabernacleManager.Helpers
{
    
    public static class TabernacleManagerViewHelper
    {
        
        public static TabernacleManagerView GetTmView(string id)
        {
            if (String.IsNullOrEmpty(id))
                return null;
            Utilisateur user = UtilisateurHelper.ObtenirUtilisateur(id);
            if(user == null)
                return null;

            TabernacleManagerView tmView = new TabernacleManagerView();
            tmView.Utilisateur = user;
            if(user.Habilitation == "PAIE" | user.Habilitation == "COMPTABILITE")
            {
                using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
                {
                    tmView.Avances = bdd.Avances.Where(a => a.Statut == "APPROUVEE").Include(a => a.Employe).Include(a => a.RespExploitation).Include(a => a.RespPortefeuille).Include(a => a.Directeur).ToList();
                }
                
            }else if (user.Habilitation == "RESPONSABLE PORTEFEUILLE")
            {
                using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
                {
                    user = bdd.Utilisateurs.Where(a => a.IdUtilisateur == user.IdUtilisateur).Include(a => a.Societes).FirstOrDefault();
                    List<int> idSocietes = new List<int>();
                    foreach (var societe in user.Societes)
                        idSocietes.Add(societe.IdSociete);
                    var tmp = bdd.Avances.Where(a => a.Statut == "REQUETE EN ATTENTE").Include(a => a.Employe).Include(a => a.RespExploitation).Include(a => a.RespPortefeuille).Include(a => a.Directeur).ToList();
                    tmView.Avances = tmp.Where(e => idSocietes.Contains(e.Employe.IdSociete)).ToList();
                }
            }
            else if (user.Habilitation == "RESPONSABLE EXPLOITATION")
            {
                using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
                {
                    tmView.Avances = bdd.Avances.Where(a => a.Statut == "VALIDATION PORTEFEUILLE").Include(a => a.Employe).Include(a => a.RespExploitation).Include(a => a.RespPortefeuille).Include(a => a.Directeur).ToList();
                }
            }
            else if (user.Habilitation == "DIRECTION" && !user.EstAdmin)
            {
                using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
                {
                    tmView.Avances = bdd.Avances.Include(a => a.Employe).Include(a => a.RespExploitation).Include(a => a.RespPortefeuille).Include(a => a.Directeur).ToList();
                    tmView.Employes = bdd.Employes.ToList();
                    tmView.Utilisateurs = bdd.Utilisateurs.ToList();
                }
            }
            else if (user.Habilitation == "DIRECTION" && user.EstAdmin)
            {
                using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
                {
                    tmView.Avances = bdd.Avances.Include(a => a.Employe).Include(a => a.RespExploitation).Include(a => a.RespPortefeuille).Include(a => a.Directeur).ToList();
                    tmView.Employes = bdd.Employes.ToList();
                    tmView.Utilisateurs = bdd.Utilisateurs.ToList();
                }
            }
            return tmView;

        }


        public static TabernacleManagerView GetTmViewByAvanceId(string idUtilisateur, string CodeRequete)
        {
            if (String.IsNullOrEmpty(idUtilisateur))
                return null;
            Utilisateur user = UtilisateurHelper.ObtenirUtilisateur(idUtilisateur);
            if (user == null)
                return null;

            TabernacleManagerView tmView = new TabernacleManagerView();
            tmView.Utilisateur = user;
            using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
            {
                tmView.Avances = bdd.Avances.Where(a => a.CodeRequete == CodeRequete).Include(a => a.Employe).Include(a => a.RespExploitation).Include(a => a.RespPortefeuille).Include(a => a.Directeur).ToList();
            }
            return tmView;

        }

        public static bool RejecterAvanceById(int? idAvance , string idUtilisateur)
        {
            try
            {
                Avance avance;
                Utilisateur user = UtilisateurHelper.ObtenirUtilisateur(idUtilisateur);
                using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
                {
                    avance = bdd.Avances.Find(idAvance);
                    avance.Statut = "REJETTEE";
                    if (user.Habilitation == "RESPONSABLE PORTEFEUILLE")
                    {
                        avance.TraitementRP = false;
                        avance.IdRP = user.IdUtilisateur;
                        avance.DateTraitementRP = DateTime.Now;
                    }else if (user.Habilitation == "RESPONSABLE EXPLOITATION")
                    {
                        avance.TraitementRE = false;
                        avance.IdRE = user.IdUtilisateur;
                        avance.DateTraitementRE = DateTime.Now;
                    }
                    else if (user.Habilitation == "DIRECTION")
                    {
                        avance.TraitementDIR = false;
                        avance.IdDIR = user.IdUtilisateur;
                        avance.DateTraitementDIR = DateTime.Now;
                    }
                    else 
                    {
                        return false;
                    }
                    bdd.SaveChanges();
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static TabernacleManagerView GetTmViewByAvanceStatut(string idUtilisateur, string statut)
        {
            if (String.IsNullOrEmpty(idUtilisateur))
                return null;
            Utilisateur user = UtilisateurHelper.ObtenirUtilisateur(idUtilisateur);
            if (user == null)
                return null;

            TabernacleManagerView tmView = new TabernacleManagerView();
            tmView.Utilisateur = user;
            using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
            {
                if (user.Habilitation == "RESPONSABLE PORTEFEUILLE")
                {
                    user = bdd.Utilisateurs.Where(a => a.IdUtilisateur == user.IdUtilisateur).Include(a => a.Societes).FirstOrDefault();
                    List<int> idSocietes = new List<int>();
                    foreach (var societe in user.Societes)
                        idSocietes.Add(societe.IdSociete);
                    var tmp = bdd.Avances.Include(a => a.Employe).Include(a => a.RespExploitation).Include(a => a.RespPortefeuille).Include(a => a.Directeur).Where(a => a.Statut == statut ).ToList();
                    tmView.Avances = tmp.Where(e => idSocietes.Contains(e.Employe.IdSociete)).ToList();
                }
                else
                {
                    tmView.Avances = bdd.Avances.Where(a => a.Statut == statut ).Include(a => a.Employe).Include(a => a.RespExploitation).Include(a => a.RespPortefeuille).Include(a => a.Directeur).ToList();
                }
            }
                return tmView;

        }


        public static bool ValierAvanceById(string idUtilisateur, int idAvance)
        {
            try
            {
                Avance avance;
                Utilisateur user = UtilisateurHelper.ObtenirUtilisateur(idUtilisateur);
                using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
                {
                    avance = bdd.Avances.Find(idAvance);
                    if (user.Habilitation == "RESPONSABLE PORTEFEUILLE")
                    {
                        avance.TraitementRP = false;
                        avance.IdRP = user.IdUtilisateur;
                        avance.DateTraitementRP = DateTime.Now;

                        avance.Statut = "VALIDATION PORTEFEUILLE";
                    }
                    else if (user.Habilitation == "RESPONSABLE EXPLOITATION")
                    {
                        avance.TraitementRE = false;
                        avance.IdRE = user.IdUtilisateur;
                        avance.DateTraitementRE = DateTime.Now;
                        if(avance.Pourcentage<30)
                            avance.Statut = "VALIDATION EXPLOITATION";
                        else
                            avance.Statut = "APPROUVEE";
                    }
                    else if (user.Habilitation == "DIRECTION")
                    {
                        avance.TraitementDIR = false;
                        avance.IdDIR = user.IdUtilisateur;
                        avance.DateTraitementDIR = DateTime.Now;

                        avance.Statut = "APPROUVEE";
                    }
                    else
                    {
                        return false;
                    }
                    bdd.SaveChanges();
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}