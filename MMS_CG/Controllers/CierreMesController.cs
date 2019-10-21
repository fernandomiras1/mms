using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data.Entity;
using MMS_DLL;
using MMS_Utiles;
using MMS_Clases;
using MMS_Clases.CierreMes;

namespace MMS_Clases.Controllers
{
    public class CierreMesController : GeneralesController
    {

        public JsonResult ObtenerListado(int pagina, int cant)
        {
            try
            {
                var db = new mmsEntities();
                var lista = db.cierre_mes.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).OrderBy(p => p.Anio.Trim()).OrderByDescending(x=> x.id).ToList();
                var grafico = lista.GroupBy(x => x.Anio).ToList();
                var resultado = new List<CierreMes_Model>();
                foreach (var item in lista)
               // long Id_Mes = db.combo_mes.FirstOrDefault(x => x.meses == item.Mes).idmes;
                {
                    resultado.Add(new CierreMes_Model
                    {
                        
                        Id = item.id,
                        Mes = new ComboMeses()
                        {
                           Id = db.combo_mes.FirstOrDefault(x => x.meses == item.Mes).idmes,
                           Nombre = item.Mes
                        },
                        Anio = item.Anio,
                        Monto = item.Monto.Value,
                        Detalle = item.Detalle,
                        Activo = true,
                        Fecha = item.Fecha_Proceso.Value,
                        Grafico = new Reportes_Model()
                        {
                            MBar = new Morris_Bar()
                            {
                                element = "morris-bar-chart",
                                data = grafico.Select(p => new Datos { Nombre = p.Key, Total = p.Sum(q => q.Monto.Value) }).ToList(),
                                xkey = "Nombre",
                                ykeys = { { "Total" } },
                                hideHover = "auto",
                                resize = true,
                                labels = { "Total" }
                            }

                        }

                    });
                }

                   // 
            //  return Json(ResultadoJson<Home_Model>.ResultadoCorrecto(res, resultado.Count));
                var total = resultado.Count;
                var res = resultado.Skip((pagina - 1) * cant).Take(cant).ToList();
                return Json(ResultadoJson<CierreMes_Model>.ResultadoCorrecto(res, total));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<CierreMes_Model>.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }

        public JsonResult ObtenerMes()
        {
            try
            {
                var db = new mmsEntities();
                var lista = db.combo_mes.OrderBy(x => x.idmes).ToList();
                var resultado = new List<CierreMes.ComboMeses>();
                foreach (var item in lista)
                {
                    resultado.Add(new CierreMes.ComboMeses
                    {
                        Id = item.idmes,
                        Nombre = item.meses

                    });
                }

                return Json(ResultadoJson<CierreMes.ComboMeses>.ResultadoCorrecto(resultado));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<CierreMes_Model>.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }


        public JsonResult GuardarCierreMes(CierreMes_Model Objeto)
        {
            try
            {
                var db = new mmsEntities();
                var Existe = db.cierre_mes.FirstOrDefault(x => x.Anio == Objeto.Anio && x.Id_Entidad == ID_Entidad && x.Mes == Objeto.Mes.Nombre && x.Activo);
                if (Existe != null && Objeto.Id == 0)
                {
                    return Json(ResultadoJson.ResultadoInCorrecto("El Registro Ingresado ya existe"));
                }

                if (Objeto.Id == 0)
                {
                    var GuardarObjeto = new MMS_DLL.cierre_mes
                    {
                        Anio = Objeto.Anio,
                        Mes = Objeto.Mes.Nombre,
                        Id_Entidad = ID_Entidad,
                        Monto = Objeto.Monto,
                        Detalle = Objeto.Detalle != null ? Objeto.Detalle : "-",
                        Activo = true,
                        Fecha_Proceso = DateTime.Now

                    };

                    db.cierre_mes.Add(GuardarObjeto);
                    var res = db.SaveChanges();
                    if (res > 0)
                        return Json(ResultadoJson<CierreMes_Model>.ResultadoCorrecto("Guardado con Exito"));
                    else
                        return Json(ResultadoJson<CierreMes_Model>.ResultadoInCorrecto("Error al Igresar el Registro"));
                }
                else
                {
                    //Modificamos.
                    var ModificarObjeto = db.cierre_mes.FirstOrDefault(x => x.id == Objeto.Id && x.Activo);
                    ModificarObjeto.Anio = Objeto.Anio;
                    ModificarObjeto.Mes = Objeto.Mes.Nombre;
                    ModificarObjeto.Monto = Objeto.Monto;
                    ModificarObjeto.Detalle = Objeto.Detalle;
                    ModificarObjeto.Fecha_Proceso = DateTime.Now;
                    var res = db.SaveChanges();
                    if (res > 0)
                        return Json(ResultadoJson<CierreMes_Model>.ResultadoCorrecto("Modificado con Exito"));
                    else
                        return Json(ResultadoJson<CierreMes_Model>.ResultadoInCorrecto("Error al Modificar el Registro"));

                }



            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<CierreMes_Model>.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }

        public JsonResult EliminarCierreMes(int Id)
        {
            try
            {
                var db = new mmsEntities();
                var dato = db.cierre_mes.FirstOrDefault(x => x.id == Id && x.Activo);
                dato.Activo = false;
                var res = db.SaveChanges();
                if (res > 0)
                    return Json(ResultadoJson.ResultadoCorrecto("Eliminado con Exito"));
                else
                    return Json(ResultadoJson.ResultadoInCorrecto("Error al Eliminar el Registro"));
            }
            catch (Exception ex)
            {
                Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }

      }
}
