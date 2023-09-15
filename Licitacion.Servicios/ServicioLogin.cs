using Licitacion.Models;
using Licitacion.Servicios.Utiles;
using Licitaciones.BaseDato;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitacion.Servicios
{
    public class ServicioLogin
    {
        db_meieEntities dataAcces = new db_meieEntities();
        SecurityMd5Models encriptador = new SecurityMd5Models();
        DB_RACOPEntities db = new DB_RACOPEntities();
        ModelBuhoBlancoEntities buhoBlancoEntity = new ModelBuhoBlancoEntities();

        public empresaLoginViewModels Validar(string cuit, string password)
        {
            List<empresaLoginViewModels> list = new List<empresaLoginViewModels>();
            var passEncripted = Encrypt.GetSHA256(password); ;
            //var passEncripted = password;

            var dataLogin = (from u in db.rc_Empresa
                             where u.cuit.Equals(cuit)
                                   && u.password.Equals(passEncripted)
                             select new empresaLoginViewModels
                             {
                                 idEmpresa = u.idEmpresa,
                                 cuit = u.cuit,
                                 razonSocial = u.razonSocial,
                             }).FirstOrDefault();
       
            //list = dataLogin.ToList();
            //dataLogin.FirstOrDefault();

            empresaLoginViewModels unDato = new empresaLoginViewModels();                    

            if (dataLogin != null)
            {
                unDato.razonSocial = dataLogin.razonSocial;
                unDato.cuit = dataLogin.cuit;
                unDato.esValido = true;
                unDato.idEmpresa = dataLogin.idEmpresa;
            }
            else
            {
                unDato.esValido = false;
            }
            return unDato;
        }
        public List<AccessViewModel> ObtenerMenu(int idUsuario)
        {
            string urlBaseBuho = ConfigurationSettings.AppSettings["urlBaseBuhoBlanco"].ToString();

            var list = (from t1 in buhoBlancoEntity.SegUserProfile
                        where t1.User_Id == idUsuario
                        //&& t1.ProfileSelected == 1
                        join t2 in buhoBlancoEntity.SegProfileAccess
                            on t1.Profile_Id equals t2.Profile_Id
                        join t3 in buhoBlancoEntity.SegAccess
                            on t2.Access_Id equals t3.Id
                        where t3.Type_Id != 32
                                && t3.Type_Id != 2
                                && t3.IsDelete == 0
                        select t3).Distinct().OrderBy(x => x.Posicion);
            var listAccessViewModels = new List<AccessViewModel>();
            var usuarioOficina = (from us in buhoBlancoEntity.SegUser
                                  where us.Id == idUsuario
                                  select us);

            var userLoginAccess = usuarioOficina.Select(x => new
            {
                Email = x.Email,
                Name = x.Name,
                FullName = x.FullName,
                Id = x.Id,
                SessionOpen = x.SessionOpen,
                FailLoginCount = x.FailLoginCount,
                LastLogin = x.LastLogin,
                Oficina = x.SegOffice_Id

            }).ToList();
            if(userLoginAccess.Count > 0)
            {
                System.Web.HttpContext.Current.Session["CurrentUser"] = userLoginAccess;
                System.Web.HttpContext.Current.Session["idOficina"] = userLoginAccess.FirstOrDefault().Oficina;
            }
           

            //listAccessViewModels.State = EntityState.Modified;

            foreach (var item in list)
            {
                var avm = new AccessViewModel
                {
                    Id = item.Id,
                    nodeId = item.Id,
                    Parent_Access_Id = item.Parent_Access_Id,
                    text = item.Name,
                    icon = item.Icon,
                    url = item.Url

                };
                if(avm.icon != null)
                {
                    avm.icon = avm.icon.Replace("fa ", "");
                }
                else
                {
                    avm.icon = "fa-tachometer-alt";
                }
                if(avm.url != null)
                {
                    
                    avm.url = avm.url.Replace("~", "");
                    avm.url = urlBaseBuho + avm.url;
                }
                listAccessViewModels.Add(avm);
            }

            var listAccessViewModels2 = (from item in listAccessViewModels
                                         where item.Parent_Access_Id == 17
                                         select ChildrenOf(item, listAccessViewModels)
                                                           ).ToList();

            return listAccessViewModels2;
        }
        private static AccessViewModel ChildrenOf(AccessViewModel role, List<AccessViewModel> roles)
        {

            foreach (AccessViewModel child in roles.Where(x => x.Parent_Access_Id == role.Id))
            {
                role.ChildNodes.Add(ChildrenOf(child, roles));
            }

            return role;
        }
    }
   
}
