using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TabernacleManager.Models;

namespace TabernacleManager.Helpers
{
    public static class UtilisateurHelper
    {

        public static int AjouterUtilisateur(string nom, string motDePasse)
        {
            Utilisateur utilisateur = new Utilisateur { Prenom = nom, Pwd = motDePasse };
            using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
            {
                bdd.Utilisateurs.Add(utilisateur);
                bdd.SaveChanges();
                return utilisateur.IdUtilisateur;
            }
        }

        public static Utilisateur Authentifier(string nom, string motDePasse)
        {
            using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
            {
                return bdd.Utilisateurs.FirstOrDefault(u => u.Login == nom && u.Pwd == motDePasse);
            }
        }

        public static Utilisateur ObtenirUtilisateur(int id)
        {
            using (CibleRhWebAppEntities bdd = new CibleRhWebAppEntities())
            {
                return bdd.Utilisateurs.FirstOrDefault(u => u.IdUtilisateur == id);
            }
            
        }

        public static Utilisateur ObtenirUtilisateur(string idString)
        {
            int id;
            if (int.TryParse(idString, out id))
                return ObtenirUtilisateur(id);
            return null;
        }

    }
}