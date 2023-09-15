using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Licitacion.Servicios
{
    public class ServicioEnvioMails
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        public async Task<string> SendMail(string toMail, string subjet, string bodyMessage, string toCopiaMail)
        {
            string sMessage;
            string fromMail = "buhogestion@mendoza.gov.ar";
            string userMail = "buhogestion";
            string passwordMail = "ohub2017";

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromMail, passwordMail);
            client.Port = 25;
            client.Host = "smtp.mendoza.gov.ar";
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromMail, fromMail);
                mail.To.Add(new MailAddress(toMail));
                if (!(string.IsNullOrEmpty(toCopiaMail)))
                {
                    var toArray = toCopiaMail.Split(';');
                    foreach (var unaCopia in toArray)
                    {
                        MailAddress to = new MailAddress(unaCopia);
                        mail.To.Add(to);
                    }
                }

                string htmlBody = "<html><body>" + bodyMessage + "</body></html>";
                 AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<div id='divImagen'> <img src=cid:companylogo width='412px' height='94px'><br>" + htmlBody + "<br> <img src=cid:vamos width='150px' height='68px'><br></div>", null, "text/html");
                var aa = System.AppDomain.CurrentDomain.BaseDirectory;

                System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(aa + @"Content\img\logoMini.png");
                System.Net.Mail.LinkedResource imageResource2 = new System.Net.Mail.LinkedResource(aa + @"Content\img\logoBuho.gif");
                //System.Net.Mail.LinkedResource imageResource3 = new System.Net.Mail.LinkedResource(aa + @"Content\img\fondoEmail.png");
                //System.Net.Mail.LinkedResource imageResource3 = new System.Net.Mail.LinkedResource(aa + @"Content\img\mailfoot.png");

                imageResource.ContentId = "companylogo";
                imageResource2.ContentId = "vamos";
                
                htmlView.LinkedResources.Add(imageResource);
                htmlView.LinkedResources.Add(imageResource2);
                mail.AlternateViews.Add(htmlView);

                mail.Subject = subjet;
                mail.Body = bodyMessage;
                mail.IsBodyHtml = true;
                client.Send(mail);
                sMessage = "Ok";
                return sMessage;

            }
            catch (Exception ex)
            {
                return sMessage = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException;
            }
        }
        public async Task<string> SendMailAdjunto(string toMail, string subjet, string bodyMessage, string toCopiaMail, string adjunto)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("SendMailAdjunto");
            string sMessage;
            string fromMail = "buhogestion@mendoza.gov.ar";
            string userMail = "buhogestion";
            string passwordMail = "ohub2017";


            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromMail, passwordMail);
            client.Port = 25;
            client.Host = "smtp.mendoza.gov.ar";

            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromMail, fromMail);
                if (!(string.IsNullOrEmpty(toMail)))
                {
                    log.Info("SendMailAdjunto Mail: " + toMail);
                    //toMail.Split(';').ToList().ForEach(addres => mail.To.Add(new MailAddress(addres)));
                    var toArray = toMail.Split(';');
                    foreach (var unaCopia in toArray)
                    {
                        if (!string.IsNullOrEmpty(unaCopia))
                        {
                            MailAddress to = new MailAddress(unaCopia);
                            mail.To.Add(to);
                            log.Info("SendMailAdjunto Mail: " + unaCopia);
                        }
                    }
                }
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(adjunto);
                mail.Attachments.Add(attachment);
                if (!(string.IsNullOrEmpty(toCopiaMail)))
                {
                    var toArray = toCopiaMail.Split(';');
                    foreach (var unaCopia in toArray)
                    {
                        if (!string.IsNullOrEmpty(unaCopia))
                        {
                            MailAddress to = new MailAddress(unaCopia);
                            mail.CC.Add(to);
                            log.Info("SendMailAdjunto Mail CC: " + unaCopia);
                        }
                    }
                }

                string htmlBody = "<html><body>" + bodyMessage + "</body></html>";
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(" <img src=cid:companylogo>" + htmlBody, null, "text/html");
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<div id='divImagen' style='background-image: cid:fondoEmail;background-repeat: no-repeat;color:#FFFFFF;'> <img src=cid:companylogo><br>" + htmlBody + "<br> <img src=cid:vamos><br></div>", null, "text/html");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<div id='divImagen' style='background-image: url(cid:fondoImagen);background-repeat: no-repeat;'> <img src=cid:companylogo><br>" + htmlBody + "<br> <img src=cid:vamos><br></div>", null, "text/html");
                var aa = System.AppDomain.CurrentDomain.BaseDirectory;

                System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(aa + @"Content\img\logoMini.png");
                System.Net.Mail.LinkedResource imageResource2 = new System.Net.Mail.LinkedResource(aa + @"Content\img\logoBuho.gif");
                //System.Net.Mail.LinkedResource imageResource3 = new System.Net.Mail.LinkedResource(aa + @"Content\img\fondoEmail.png");
                //System.Net.Mail.LinkedResource imageResource3 = new System.Net.Mail.LinkedResource(aa + @"Content\img\mailfoot.png");

                imageResource.ContentId = "companylogo";
                imageResource2.ContentId = "vamos";

                htmlView.LinkedResources.Add(imageResource);
                htmlView.LinkedResources.Add(imageResource2);
                mail.AlternateViews.Add(htmlView);

                mail.Subject = subjet;
                mail.Body = bodyMessage;
                mail.IsBodyHtml = true;
                client.Send(mail);
                sMessage = "Ok";
                return sMessage;

            }
            catch (Exception ex)
            {
                sMessage = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException;
                log.Info("Error SendMailAdjunto " + sMessage);
                return sMessage;
            }
        }
    }
}
