using CibleWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CibleWebForms.Helpers
{
    public class CustomMembershipUser : MembershipUser
    {
        public int UserId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Matricule { get; set; }

        public CustomMembershipUser(Employe user) : base("CustomMembership", user.Nom, user.IdEmploye, string.Empty, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.IdEmploye;
            Nom = user.Nom;
            Prenom = user.Prenom;
            Matricule = user.Matricule;
            Telephone = user.Telephone;
        }

    }
}