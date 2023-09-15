using Licitacion.Servicios;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Licitaciones.Areas.Licitacion.Controllers.Api
{
    public class ChatBotController : ApiController
    {
        // GET: Licitacion/ChatBot
        [System.Web.Http.Route("api/ChatBot/buscaRespuestas")]
        [System.Web.Http.ActionName("buscaRespuestas")]
        [System.Web.Http.HttpGet]
        public List<ChatBotViewModels> buscaRespuestas(int? idPadre)
        {
            List<ChatBotViewModels> respuestas = new List<ChatBotViewModels>();
            
            try
            {
                ServicioChatBot servicio = new ServicioChatBot();
                respuestas = servicio.buscarRespuestas(idPadre);
                
            }
            catch (Exception ex)
            {
                //respuesta.codigo = 0;
                //respuesta.mensaje = "Ocurrio un error " + ex.Message;
            }
            return respuestas;
        }
    }
}