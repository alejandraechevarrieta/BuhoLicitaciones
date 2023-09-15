using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.ViewModels
{
    public class ChatBotViewModels
    {
        public int? idChat { get; set; }
        public int? idPadre { get; set; }
        public string TituloPpal { get; set; }
        public string Opcion { get; set; }
        public string MensajePpal { get; set; }
    }
}
