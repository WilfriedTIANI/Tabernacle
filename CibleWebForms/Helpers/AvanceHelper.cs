using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CibleWebForms.Helpers
{
    public static class  AvanceHelper
    {

        public static string GenerateCodeAvance(string matricule)
        {
            return matricule + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

    }
}