using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licitaciones.BaseDato;
using Licitaciones.ViewModels;


namespace Licitaciones.Servicio
{
    public class ServicioLicitacion
    {
        public List<licitacionGrillaViewModels> listaLicitaciones
            (ref int recordsTotal, string sortColumn, string sortColumnDir, string searchValue,
            int pageSize = 0, int skip = 0)
        {
            List<licitacionGrillaViewModels> lista = new List<licitacionGrillaViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = from lic in db.PryProyectoes
                                join edo in db.PryEstadoEtapas
                                on lic.GrlTypeState_Id equals edo.Id
                                join pyl in db.PryLicitacions
                                on lic.Id equals pyl.IdPryProyecto
                                    into pylo
                                from pyj in pylo.DefaultIfEmpty()
                                where edo.PryStage_Id == 49
                                select new licitacionGrillaViewModels
                                {
                                    nombreEtapa = edo.Name,
                                    idEtapa = edo.Id,
                                    nombreObra = lic.Nombre,
                                    idObra = lic.Id,
                                    fechaApertura = pyj.FechaApertura,
                                    fechaPublicacion = pyj.FechaPublicacionDesde,
                                    montoObra = pyj.MontoOficial
                                };


                    //if (saldo != 0)
                    //{
                    //    query = query.Where(x => x.saldoFactura <= saldo);
                    //}

                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        if ((sortColumn == "nombreObra") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nombreObra);
                        }
                        if ((sortColumn == "nombreObra") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nombreObra);
                        }
                        //if ((sortColumn == "nombreCliente") && (sortColumnDir == "desc"))
                        //{
                        //    query = query.OrderByDescending(x => x.nombreCliente);
                        //}
                        //if ((sortColumn == "nombreCliente") && (sortColumnDir == "asc"))
                        //{
                        //    query = query.OrderBy(x => x.nombreCliente);
                        //}
                        //if ((sortColumn == "codCliente") && (sortColumnDir == "desc"))
                        //{
                        //    query = query.OrderByDescending(x => x.codCliente);
                        //}
                        //if ((sortColumn == "codCliente") && (sortColumnDir == "asc"))
                        //{
                        //    query = query.OrderBy(x => x.codCliente);
                        //}
                        //if ((sortColumn == "dniCliente") && (sortColumnDir == "asc"))
                        //{
                        //    query = query.OrderBy(x => x.dniCliente);
                        //}
                        //if ((sortColumn == "dniCliente") && (sortColumnDir == "desc"))
                        //{
                        //    query = query.OrderByDescending(x => x.dniCliente);
                        //}
                        //if ((sortColumn == "fechaFacturaString") && (sortColumnDir == "asc"))
                        //{
                        //    query = query.OrderBy(x => x.fechaFactura);
                        //}
                        //if ((sortColumn == "fechaFacturaString") && (sortColumnDir == "desc"))
                        //{
                        //    query = query.OrderByDescending(x => x.fechaFactura);
                        //}
                        //if ((sortColumn == "montoFacturaString") && (sortColumnDir == "asc"))
                        //{
                        //    query = query.OrderBy(x => x.montoFactura);
                        //}
                        //if ((sortColumn == "montoFacturaString") && (sortColumnDir == "desc"))
                        //{
                        //    query = query.OrderByDescending(x => x.montoFactura);
                        //}
                        //if ((sortColumn == "saldoFacturaString") && (sortColumnDir == "asc"))
                        //{
                        //    query = query.OrderBy(x => x.saldoFactura);
                        //}
                        //if ((sortColumn == "saldoFacturaString") && (sortColumnDir == "desc"))
                        //{
                        //    query = query.OrderByDescending(x => x.saldoFactura);
                        //}

                    }
                    else
                    {
                        query = query = query.OrderBy(x => x.nombreObra);
                    }

                    query = query.Take(500);
                    recordsTotal = query.Count();

                    var lst = query.Skip(skip).Take(pageSize);
                    lista = lst.ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {

            }
            return lista;
        }
    }
}
