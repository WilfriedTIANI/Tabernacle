using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TabernacleManager.Models
{

    
    public class TabernacleManagerView
    {

        public Utilisateur Utilisateur { get; set; } = null;

        public List<Avance> Avances { get; set; } = null;

        public List<Employe> Employes { get; set; } = null;

        public List<Utilisateur> Utilisateurs { get; set; } = null;

    }



}