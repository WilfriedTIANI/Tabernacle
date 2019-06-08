//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CibleWebForms.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Avance
    {
        public int IdRequete { get; set; }
        public string CodeRequete { get; set; }
        public int IdEmploye { get; set; }
        public Nullable<int> Montant { get; set; }
        public int Pourcentage { get; set; }
        public int DelaiEnMois { get; set; }
        public string MotifRequete { get; set; }
        public System.DateTime DateRequete { get; set; }
        public string Statut { get; set; }
        public string MotifAnnulation { get; set; }
        public string MotifRejet { get; set; }
        public Nullable<int> IdRP { get; set; }
        public Nullable<bool> TraitementRP { get; set; }
        public Nullable<System.DateTime> DateTraitementRP { get; set; }
        public Nullable<int> IdRE { get; set; }
        public Nullable<bool> TraitementRE { get; set; }
        public Nullable<System.DateTime> DateTraitementRE { get; set; }
        public Nullable<int> IdDIR { get; set; }
        public Nullable<bool> TraitementDIR { get; set; }
        public Nullable<System.DateTime> DateTraitementDIR { get; set; }
        public bool Comptabilise { get; set; }
        public bool AvanceEnvoyee { get; set; }
    
        public virtual Employe Employe { get; set; }
        public virtual Utilisateur RespExploitation { get; set; }
        public virtual Utilisateur RespPortefeuille { get; set; }
        public virtual Utilisateur Directeur { get; set; }
    }
}
