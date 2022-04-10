using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace correoService
{
    public class EnvioCorreo
    {
        public bool sendEmail() {
            string servidor = "smtp.gmail.com";
            int puerto = 587;

            string gmailUser = "msalvarado.lopez@gmail.com";
            string gmailPassw = "jymaxsuvhunmpqmd";

            MimeMessage mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("spaceLatino", gmailUser));
            mensaje.To.Add(new MailboxAddress("destino", gmailUser));
            mensaje.Subject = "spaceLatino - pedido";

            BodyBuilder cuerpoMensaje = new BodyBuilder();
            cuerpoMensaje.TextBody = "texto de prueba";
            cuerpoMensaje.HtmlBody = buildBody(); 

            mensaje.Body = cuerpoMensaje.ToMessageBody();

            SmtpClient clienteSMTP = new SmtpClient();
            clienteSMTP.CheckCertificateRevocation = false;
            clienteSMTP.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
            clienteSMTP.Authenticate(gmailUser, gmailPassw);
            clienteSMTP.Send(mensaje);
            clienteSMTP.Disconnect(true);

            return true;
        }

        private string buildBody()
        {
            string strBoady = "";
            string dirHTML = HttpContext.Current.Server.MapPath("~/Views/htmlPedido.html");

            using (StreamReader contenido = new StreamReader(dirHTML)) {
                strBoady = contenido.ReadToEnd();
            }

                return strBoady;
        }
    }
}