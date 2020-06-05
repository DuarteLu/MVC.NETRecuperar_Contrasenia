using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace Recuperacion_Contrasenia.Servicios
{
    public class AccesoServicios
    {
        Entidades.UnTablaEntities ctx = new Entidades.UnTablaEntities();


        public void RegistrarToken(ViewModels.RecuperarViewModel model)
        {
            string Token = Guid.NewGuid().ToString();

              var Usu = ctx.Usuario.Where(d => d.Email == model.Email).FirstOrDefault();
                if (Usu != null)
                {
                    Usu.Token = Token;
                    ctx.Entry(Usu).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();

                    EnviarMail(Usu.Email, Token);
                }
           
        }

        public int ValidarToken(ViewModels.RecuperarPasswordViewModel Model)
        {
           
                if (Model.Token == null || Model.Token.Trim().Equals(""))
                {
                    return 0;
                }
                var User = ctx.Usuario.Where(d => d.Token == Model.Token).FirstOrDefault();

                if (User == null)
                {
                  return 1;               
                }

            return 2;
        }


        public void Cambiarpassword(ViewModels.RecuperarPasswordViewModel model)
        {
               var User = ctx.Usuario.Where(d => d.Token == model.Token).FirstOrDefault();
                if (User != null)
                {
                    User.Password = model.Password;
                    User.Token = null;
                    ctx.Entry(User).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }

            
        }

        private void EnviarMail(string EmailDestino, string Token)
        {
           
            
            string url = "http://localhost:49544/Acceso/Recuperar/?Token=" + Token;

            //MailMessage recibe de donde viene , adonde va, el asunto y el cuerpo del mail
            MailMessage EmailMensaje = new MailMessage(ConfigurationManager.AppSettings["Email"].ToString(), 
                EmailDestino, "Recuperacion de contraseña",
                "<p>Correo de recuperacion de contraseña<p/><br/>" + "<a href=" + url + ">Click para recuperar<a/>");

            EmailMensaje.IsBodyHtml = true;

            SmtpClient Clientesmpt = new SmtpClient("smtp.gmail.com");
            Clientesmpt.EnableSsl = true;
            Clientesmpt.UseDefaultCredentials = false;
            Clientesmpt.Port = 587;
            Clientesmpt.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"].ToString(), 
                ConfigurationManager.AppSettings["Password"].ToString());
            Clientesmpt.Send(EmailMensaje);
            Clientesmpt.Dispose();
        }
    }
}