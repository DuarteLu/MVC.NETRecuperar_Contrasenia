using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recuperacion_Contrasenia.Controllers
{
    public class AccesoController : Controller
    {

        Servicios.AccesoServicios Accesos = new Servicios.AccesoServicios();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EmpezarRecuperacion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmpezarRecuperacion(ViewModels.RecuperarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Accesos.RegistrarToken(model);

            ViewBag.Mensaje = "Se ha enviado un Email para recuperar su contraseña.Por favor,verificar su correo.";
            return View("PaginaEnviar");
        }


        [HttpGet]
        public ActionResult Recuperar(string token)
        {
            ViewModels.RecuperarPasswordViewModel model = new ViewModels.RecuperarPasswordViewModel();
            model.Token = token;

            var valor =Accesos.ValidarToken(model);
            if (valor == 0)
            {
                return View("Index");
            }
            else if(valor == 1)
            {
                ViewBag.Error = "El token de validación ha expirado";
                return View("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Recuperar(ViewModels.RecuperarPasswordViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Accesos.Cambiarpassword(model);

            ViewBag.Mensaje = "Contraseña modificada con exito!";
            return View("Index");
        }
    
    }
}