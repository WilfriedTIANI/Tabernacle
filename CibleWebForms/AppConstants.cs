using CibleWebForms.Controllers;
using CibleWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CibleWebForms
{
    
    public static class AppConstants
    {

        public static class EmployeStatuts
        {
            public const string PRETINTERDIT = "PRET INTERDIT";
            public const string PRETENCOURS = "PRET EN COURS";
            public const string LICENCIE = "LICENCIE";
            public const string SALARIE = "SALARIE";

        }

        public static bool UnConnectRedirect(Controller c)
        {
            return (EmployesController.localEmploye != null);
        }

        public static class AvanceStatuts
        {
            public const string REJETTEE = "REJETTEE";
            public const string ANNULEE = "ANNULEE";
            public const string APPROUVEE = "APPROUVEE";
            public const string VALIDATIONEXPLOITATION = "VALIDATION EXPLOITATION";
            public const string VALIDATIONPORTEFEUILLE = "VALIDATION PORTEFEUILLE";
            public const string REQUETEENATTENTE = "REQUETE EN ATTENTE";

        }



    }

}