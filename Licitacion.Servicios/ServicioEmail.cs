using Licitaciones.BaseDato;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitacion.Servicios
{
    public class ServicioEmail
    {
        public EmailViewModels buscarMail(int? tipoMail)
        {
            EmailViewModels email = new EmailViewModels();
            using (db_meieEntities db = new db_meieEntities())
            {
                var emailFormato = db.LicEmail
                    .Where(x => x.tipo == tipoMail)
                    .FirstOrDefault();
                if (emailFormato != null)
                {
                    email.asunto = emailFormato.asunto;
                    email.cuerpo = emailFormato.cuerpo;
                }
            }
            return email;
        }
        public async Task enviarEmail(EmailViewModels dataEmail)
        {
            string destino = dataEmail.destino;
            string asunto = dataEmail.asunto;
            string body = dataEmail.cuerpo;
            string copiaMail = dataEmail.copiaMail;
            ServicioEnvioMails sendMail = new ServicioEnvioMails();
            await sendMail.SendMail(destino, asunto, body, copiaMail);
        }
        public async Task enviarEmailAdjunto(EmailViewModels dataEmail, string adjunto)
        {
            string destino = dataEmail.destino;
            string asunto = dataEmail.asunto;
            string body = dataEmail.cuerpo;
            string copiaMail = dataEmail.copiaMail;
            ServicioEnvioMails sendMail = new ServicioEnvioMails();
            await sendMail.SendMailAdjunto(destino, asunto, body, copiaMail, adjunto);
        }
    }
}

