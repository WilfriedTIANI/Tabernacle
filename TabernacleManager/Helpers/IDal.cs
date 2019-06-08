using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TabernacleManager.Models;

namespace TabernacleManager.Helpers
{
    public interface IDal : IDisposable
    {
        int AjouterUtilisateur(string nom, string motDePasse);
        Utilisateur Authentifier(string nom, string motDePasse);
        Utilisateur ObtenirUtilisateur(int id);
        Utilisateur ObtenirUtilisateur(string idStr);



    }

}