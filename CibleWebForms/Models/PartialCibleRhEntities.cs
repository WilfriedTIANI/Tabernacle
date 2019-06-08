using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CibleWebForms.Models
{
    [MetadataType(typeof(EmployeMetadata))]
    public partial class Employe
    {

    }

    [MetadataType(typeof(AvanceMetadata))]
    public partial class Avance
    {

    }
}