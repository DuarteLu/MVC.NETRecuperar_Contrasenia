using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Recuperacion_Contrasenia.ViewModels
{
    public class RecuperarPasswordViewModel
    {
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        public string Password2 { get; set; }

    }
}