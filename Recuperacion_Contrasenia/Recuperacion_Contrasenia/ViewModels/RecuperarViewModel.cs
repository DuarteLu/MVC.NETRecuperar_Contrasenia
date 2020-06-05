using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Recuperacion_Contrasenia.ViewModels
{
    public class RecuperarViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

    }
}