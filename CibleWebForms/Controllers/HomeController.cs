using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CibleWebForms.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "TABERNACLE est une application web qui permet aux employés payés par CIBLE RH d'obtenir une avance sur salaire en remplissant un simple formulaire web." + Environment.NewLine +
                "Pour utiliser TABERNACLE, vous devez d'abord vous identifier. Si vous n'avez pas de compte, enregistrez-vous maintenant.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Pour obtenir plus d'informations ou de l'aide, vous pouvez nous contacter !!!";

            return View();
        }
    }
}