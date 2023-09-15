using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.ViewModels
{
    public class EmpresaViewModels
    {
    }
    public class obraFavoritaViewModels
    {
        public int? idObra { get; set; }
        public int? idEmpresa { get; set; }
        public int? idFavorito { get; set; }
    }
    public class empresaOfertaViewModels
    {
        public int? idObra { get; set; }
        public int? idEmpresa { get; set; }
    }
    public class empresaLoginViewModels
    {
        public int? idEmpresa { get; set; }
        public string cuit { get; set; }
        public string razonSocial { get; set; }
        public bool? esValido { get; set; }
    }
}
