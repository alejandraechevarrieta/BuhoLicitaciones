using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.ViewModels
{
    public class AccessViewModel
    {
        public AccessViewModel()
        {
            this.ChildNodes = new List<AccessViewModel>();
        }

        public string url { get; set; }

        public string text { get; set; }

        public string icon { get; set; }
        public int Id { get; set; }
        public int nodeId { get; set; }
        [JsonIgnore]
        public int? Parent_Access_Id { get; set; }

        [JsonProperty("nodes")]
        public List<AccessViewModel> ChildNodes { get; set; }

        public EstadoNodo state { get; set; }
        public int order { get; set; }
    }
    public class EstadoNodo
    {
        public bool disabled { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }
        [JsonProperty("checked")]
        public bool seleccionado { get; set; }

    }
}
