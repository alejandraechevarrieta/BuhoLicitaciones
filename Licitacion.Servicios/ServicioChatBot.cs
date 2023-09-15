using Licitaciones.BaseDato;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitacion.Servicios
{
    public class ServicioChatBot
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";

        public List<ChatBotViewModels> buscarRespuestas(int? idOpcion)
        {
            List<ChatBotViewModels> respuestas = new List<ChatBotViewModels>();
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Buscar Respuestas ChatBot");
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    respuestas = (from opciones in db.LicChatbot
                                  where opciones.HijoDe == idOpcion
                                  select new ChatBotViewModels
                                  {
                                      idChat = opciones.idLicChatbot,
                                      Opcion = opciones.Opcion,
                                      TituloPpal = opciones.TituloOpcion,
                                      MensajePpal = opciones.MensajePpal
                                  }).ToList();

                }
            }
            catch (Exception ex)
            {
                log.Error("Error Respuestas Chatbot " + ex.Message);
            }
            return respuestas;
        }
    }
}
