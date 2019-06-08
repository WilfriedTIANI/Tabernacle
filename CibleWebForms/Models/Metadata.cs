using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CibleWebForms.Models
{
    public class EmployeMetadata
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Matricule")]
        public string Matricule { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Prenom")]

        public string Prenom { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }
        
        [StringLength(9)]
        [Required(ErrorMessage = "Please enter Mobile Money No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^6(([579][0-9]{7})|8[0-4][0-9]{6})$", ErrorMessage = "Numero De Telephone invalide.")]
        [Display(Name = "Numéro Mobile Money")]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Société")]
        public int IdSociete { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email invalide.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [Display(Name = "Mot de passe")]
        public string Pwd { get; set; }

        
    }

    public class AvanceMetadata
    {
        //public int Montant { get; set; }



        [Required]
        [Range(0, 100, ErrorMessage = "Les Pourcentages vont de 0 - 100")]
        [Display(Name = "Pourcentage Avance")]
        public int Pourcentage { get; set; }


        [Required]
        [Range(0,3, ErrorMessage = "Les Delais Sont : 0 1 2 3 (mois)")]
        [Display(Name = "Delai En Mois")]
        public int DelaiEnMois { get; set; }


        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Motif De La Demande")]
        public string MotifRequete { get; set; }
        
    }


}