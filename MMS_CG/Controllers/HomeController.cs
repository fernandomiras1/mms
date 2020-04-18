using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data.Entity;
using MMS_DLL;
using System.Threading.Tasks;
using MMS_Utiles;
using MMS_Clases;
using MMS_Clases.Mensuales;

namespace MMS_Clases.Controllers
{
    public class HomeController : GeneralesController
    {

        // para cuando tenemos que hacer un paginador. 
        //public JsonResult ObtenerListado(int pagina)
        public JsonResult ObtenerListado(int periodo)
        {

            var db = new mmsEntities();
            var lista = new List<ingresos>();
            var AnioCurso = DateTime.Now.Year;
			var AnioAnterior = DateTime.Now.Year - 1;
			var MesCurso = DateTime.Now.Month;
            //if (periodo == 1)
            //{
            //    //Mes en Curso
            //    lista = db.ingresos.Where(x => x.Id_Entidad == ID_Entidad  && x.Activo && x.Fecha.Month == MesCurso && x.Fecha.Year == AnioCurso).OrderByDescending(p => p.id).ToList();
            //}
            //else if (periodo == 2)
            //{
            //    //Año en Curso
            //    lista = db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Activo && x.Fecha.Year == AnioCurso).OrderByDescending(p => p.id).ToList();
            //}

            //else
            //{
            //    lista = db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).OrderByDescending(p => p.id).ToList();
            //}

            lista = periodo == 1 ? db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Activo && x.Fecha.Month == MesCurso && x.Fecha.Year == AnioCurso).OrderByDescending(p => p.id).ToList()
                : periodo == 2 ? db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Activo && x.Fecha.Year == AnioCurso).OrderByDescending(p => p.id).ToList()
				: periodo == 3 ? db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Activo && x.Fecha.Year == AnioAnterior).OrderByDescending(p => p.id).ToList()
			    : db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).OrderByDescending(p => p.id).ToList();

            var resultado = new List<Home_Model>();

            foreach (var item in lista)
            {
                var grafico = lista.GroupBy(x => x.combo_categoria.Nombre).ToList();
                resultado.Add(new Home_Model
                {

                    Id = item.id,
                    Cate = new Combo_Categorias()
                    {
                        Id = item.Id_Categoria != null ? item.Id_Categoria.Value : 0,
                        Nombre = item.combo_categoria.Nombre != null ? item.combo_categoria.Nombre : null,
                        Tipo = item.combo_categoria.combo_tipo.Nombre
                    },
                    SubCate = new Combo_Sub_Categorias()
                    {
                        Id = item.combo_sub_categoria != null ? item.combo_sub_categoria.id : 0,
                        Nombre = item.combo_sub_categoria.Nombre != null ? item.combo_sub_categoria.Nombre : null
                    },
                    Observacion = item.Observación,
                    Tipo = new Combo_Tipo() { id = item.combo_tipo.id, Nombre = item.combo_tipo.Nombre },
                    Forma_Pago = new Combo_FormaPago() { Id = item.combo_forma_pago.id, Nombre = item.combo_forma_pago.Nombre },
                    Precio = item.Precio.Value,
                    Fecha = item.Fecha,

                    Grafico = new Reportes_Model()
                    {

                        MBar = new Morris_Bar()
                        {
                            element = "morris-bar-chart",
                            data = grafico.Select(p => new Datos { Nombre = p.Key, Total = p.Sum(q => q.Precio.Value) }).ToList(),
                            xkey = "Nombre",
                            ykeys = { { "Total" } },
                            hideHover = "auto",
                            resize = true,
                            labels = { "Total" }

                        }
                    }


                });

            }
            // para cuando tenemos que hacer un paginador. 
            //  var res = resultado.Skip((pagina - 1) * 5).Take(5).ToList();
            //  return Json(ResultadoJson<Home_Model>.ResultadoCorrecto(res, resultado.Count));
            var res = Json(ResultadoJson<Home_Model>.ResultadoCorrecto(resultado));
            res.MaxJsonLength = int.MaxValue;
            return res;
        }

        //Obtener Tipos
        public JsonResult ObtenerTipo()
        {

            try
            {
                var db = new mmsEntities();
                var lista = db.combo_tipo.Select(x => new Combo_Tipo
                {
                    id = x.id,
                    Nombre = x.Nombre

                }).OrderBy(p => p.id).ToList();

                return Json(ResultadoJson<Combo_Tipo>.ResultadoCorrecto(lista));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }

        //combo categorias
        public JsonResult ObtenerCategorias()
        {
            var db = new mmsEntities();
            var lista = db.combo_categoria.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).Select(x => new Combo_Categorias
            {
                Id = x.id,
                Nombre = x.Nombre,
                Tipo = x.combo_tipo.Nombre

            }).OrderBy(p => p.Nombre).ToList();

            return Json(ResultadoJson<Combo_Categorias>.ResultadoCorrecto(lista));
        }

        public JsonResult Forma_Pago()
        {
            try
            {
                var db = new mmsEntities();
                var lista = db.combo_forma_pago.Select(x => new Combo_Tipo
                {
                    id = x.id,
                    Nombre = x.Nombre

                }).OrderBy(p => p.id).ToList();

                return Json(ResultadoJson<Combo_Tipo>.ResultadoCorrecto(lista));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }

        //combo Sub_Categorias
        public JsonResult ObtenerSubCategorias()
        {
            var db = new mmsEntities();
            var lista = db.combo_sub_categoria.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).Select(x => new Combo_Sub_Categorias
            {
                Id = x.id,
                Nombre = x.Nombre,
                Tipo = x.combo_categoria.Nombre
            }).OrderBy(p => p.Nombre).ToList();

            return Json(ResultadoJson<Combo_Sub_Categorias>.ResultadoCorrecto(lista));

        }


        public JsonResult Guardar(Home_Model modelo)
        {
            try
            {
                var db = new mmsEntities();
		
                //var existente = db.ingresos.FirstOrDefault(p => p.id != modelo.Id && p.Id_Categoria == modelo.Cate.Id && p.Id_SubCategoria == modelo.SubCate.Id && p.Observación == modelo.Observacion && p.Precio == modelo.Precio);
                //if (existente != null)
                //{
                //    return Json(ResultadoJson.ResultadoInCorrecto("El Registro Ingresado ya existe"));
                //}

                if (modelo.Id == 0)
                {
                    // Guardamos.
                    var objeto_modelo = new MMS_DLL.ingresos
                    {

                        Id_Tipo = modelo.Tipo.id,
                        Id_Categoria = modelo.Cate.Id,
                        Id_Entidad = ID_Entidad,
                        Id_SubCategoria = modelo.SubCate.Id,
                        Observación = modelo.Observacion != null ? modelo.Observacion.ToUpper() : "-",
                        Precio = modelo.Precio,
                        Id_Forma_Pago = modelo.Forma_Pago.Id,
                        Fecha = modelo.Fecha,
                        Activo = true

                    };
                    db.ingresos.Add(objeto_modelo);
                    var res = db.SaveChanges();
                    if (res > 0)
                        return Json(ResultadoJson<Home_Model>.ResultadoCorrecto("Guardado con Exito"));
                    else
                        return Json(ResultadoJson<Home_Model>.ResultadoInCorrecto("Error al Igresar el Registro"));
                }
                else
                {
                    //Modificamos.

                    var Modelo_modificar = db.ingresos.FirstOrDefault(x => x.id == modelo.Id && x.Activo);
                    Modelo_modificar.Id_Tipo = modelo.Tipo.id;
                    Modelo_modificar.Id_Categoria = modelo.Cate.Id;
                    Modelo_modificar.Id_SubCategoria = modelo.SubCate.Id;
                    Modelo_modificar.Precio = modelo.Precio;
                    Modelo_modificar.Fecha = modelo.Fecha;
                    Modelo_modificar.Id_Forma_Pago = modelo.Forma_Pago.Id;
                    Modelo_modificar.Observación = modelo.Observacion.ToUpper();
                    var res = db.SaveChanges();
                    if (res > 0)
                        return Json(ResultadoJson<Home_Model>.ResultadoCorrecto("Modificado con Exito"));
                    else
                        return Json(ResultadoJson<Home_Model>.ResultadoInCorrecto("Error al Modificar el Registro"));
                }

            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<Home_Model>.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }

        public JsonResult Eliminar(int Id)
        {
            try
            {
                var db = new mmsEntities();
                var dato = db.ingresos.FirstOrDefault(x => x.id == Id && x.Activo);
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

        public JsonResult BusquedaGeneral(Busqueda_Model modelo)
        {
            try
            {
                var db = new mmsEntities();
                var lista = new List<ingresos>();
                var Reporte_Bar = new List<ingresos>();
                if (modelo.detallado)
                {
                    lista = db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Fecha >= modelo.Fecha_Desde.Date && x.Fecha <= modelo.Fecha_Hasta.Date && x.Activo).ToList()
                        .Where(x => (modelo.Cate == null || x.Id_Categoria == modelo.Cate.Id) &&
                                     (modelo.Tipo == null || x.Id_Tipo == modelo.Tipo.id) &&
                                     (modelo.SubCate == null || x.Id_SubCategoria == modelo.SubCate.Id)).OrderByDescending(p => p.id).ToList();

                    //lista = lista.Where(x => );
                    //  lista = db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.combo_categoria.Nombre.ToString() == modelo.Cate.Nombre.ToString() && x.combo_tipo.Nombre.ToString() == modelo.Tipo.Nombre.ToString() && x.combo_sub_categoria.Nombre.ToString() == modelo.SubCate.Nombre.ToString()
                    // && x.Fecha >= modelo.Fecha_Desde.Date && x.Fecha <= modelo.Fecha_Hasta.Date && x.Activo).OrderByDescending(p => p.id).ToList();
                }
                else
                {
                    lista = db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Fecha >= modelo.Fecha_Desde.Date && x.Fecha <= modelo.Fecha_Hasta.Date && x.Activo).OrderByDescending(p => p.id).ToList();
                }


                var resultado = new List<Home_Model>();

                foreach (var item in lista)
                {
                    var grafico = lista.GroupBy(x => x.combo_categoria.Nombre).ToList();
                    resultado.Add(new Home_Model

                       {
                           Id = item.id,
                           Cate = new Combo_Categorias()
                           {
                               Id = item.combo_categoria.id != null ? item.combo_categoria.id : 0,
                               Nombre = item.combo_categoria.Nombre != null ? item.combo_categoria.Nombre : null
                           },
                           SubCate = new Combo_Sub_Categorias()
                           {
                               Id = item.combo_sub_categoria.id != null ? item.combo_sub_categoria.id : 0,
                               Nombre = item.combo_sub_categoria.Nombre != null ? item.combo_sub_categoria.Nombre : null
                           },

                           Observacion = item.Observación,
                           Tipo = new Combo_Tipo() { id = item.combo_tipo.id, Nombre = item.combo_tipo.Nombre },
                           Forma_Pago = new Combo_FormaPago() { Id = item.combo_forma_pago.id, Nombre = item.combo_forma_pago.Nombre },
                           Precio = item.Precio.Value,
                           Fecha = item.Fecha,

                           Grafico = new Reportes_Model()
                           {

                               MBar = new Morris_Bar()
                               {
                                   element = "morris-bar-chart",
                                   data = grafico.Select(p => new Datos { Nombre = p.Key, Total = p.Sum(q => Math.Round(q.Precio.Value)) }).ToList(),
                                   xkey = "Nombre",
                                   ykeys = { { "Total" } },
                                   hideHover = "auto",
                                   resize = true,
                                   labels = { "Total" }

                               }

                           }

                       });

                }

                var res = Json(ResultadoJson<Home_Model>.ResultadoCorrecto(resultado));
                res.MaxJsonLength = int.MaxValue;
                return res;
            }

            catch (Exception ex)
            {
                return Json(ResultadoJson<Home_Model>.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }


        public JsonResult Grafico_Area()
        {

            try
            {
                var db = new mmsEntities();
                var lista = db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).GroupBy(x => x.Fecha.Year).ToList();
                var resultado = new List<Morris_Area>();
                var dato = new Morris_Area
                {

                    element = "morris-area-chart",
                    data = lista.Select(p => new Datos_Area
                    {
                        Anio = p.Key.ToString(),
                        Ingreso = p.Where(r => r.Id_Tipo == 1).Sum(r => Math.Round(r.Precio.Value)),
                        Egreso = p.Where(r => r.Id_Tipo == 2).Sum(r => Math.Round(r.Precio.Value)),

                    }).ToList(),
                    xkey = "Anio",
                    ykeys = { { "Ingreso" }, { "Egreso" }, { "Total_Area" } },
                    labels = { "Ingreso", "Egreso", "Total" },
                    hideHover = "auto",
                    pointSize = 2,
                    resize = true

                };

                resultado.Add(dato);
                return Json(ResultadoJson<Morris_Area>.ResultadoCorrecto(resultado));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<Morris_Area>.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }

        public JsonResult Grafico_Donut(Busqueda_Model modelo)
        {

            try
            {
                var db = new mmsEntities();
                var lista = db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Fecha >= modelo.Fecha_Desde.Date && x.Fecha <= modelo.Fecha_Hasta.Date && x.Activo).ToList()
                      .Where(x => (modelo.Cate == null || x.Id_Categoria == modelo.Cate.Id) &&
                                   (modelo.Tipo == null || x.Id_Tipo == modelo.Tipo.id) &&
                                   (modelo.SubCate == null || x.Id_SubCategoria == modelo.SubCate.Id)).OrderByDescending(p => p.id).ToList();

                var GraficoCate = lista.Where(p => (modelo.Cate == null || p.Id_Categoria == modelo.Cate.Id) && (modelo.SubCate == null || p.Id_SubCategoria == modelo.SubCate.Id)).GroupBy(x => x.combo_sub_categoria.Nombre).ToList();
                var resultado = new List<Morris_Donut>();
                var dato = new Morris_Donut
                {

                    element = "morris-donut-chart",
                    data = GraficoCate.Select(p => new Datos_Donut { label = p.Key, value = p.Sum(q => Math.Round(q.Precio.Value)) }).ToList(),
                    resize = true
                };
                resultado.Add(dato);
                return Json(ResultadoJson<Morris_Donut>.ResultadoCorrecto(resultado));

            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<Morris_Donut>.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }

        public JsonResult Recordatorio_Alarmas()
        {
            try
            {
                 var db = new mmsEntities();
                 var listaIngresos = db.ingesos_mensuales.Where(x => x.Activo && x.Recordatorio.Value && x.Id_Entidad == ID_Entidad && x.Fecha_Vencimiento.Value.Day == DateTime.Now.Day && x.Fecha_Vencimiento.Value.Month == DateTime.Now.Month && x.Fecha_Vencimiento.Value.Year == DateTime.Now.Year).Select(p => new Recordatorios_Model
                 { 
                     FechaVencimiento = p.Fecha_Vencimiento.Value,
                     Nombre = p.combo_sub_categoria.Nombre,
                     Observacion = p.Detalle,
                     Tipo = "COBRAR"

                 }).ToList();
                 var listaEgresos = db.egresos_mensuales.Where(x => x.Activo && x.Recordatorio.Value && x.Id_Entidad == ID_Entidad && x.Fecha_Vencimiento.Value.Day == DateTime.Now.Day && x.Fecha_Vencimiento.Value.Month == DateTime.Now.Month && x.Fecha_Vencimiento.Value.Year == DateTime.Now.Year).Select(p => new Recordatorios_Model
                 { 
                     FechaVencimiento = p.Fecha_Vencimiento.Value,
                     Nombre = p.combo_sub_categoria.Nombre,
                     Observacion = p.Detalle,
                     Tipo = "PAGAR" 

                 }).ToList();
                 var lista = listaIngresos.Concat(listaEgresos).ToList();
                 return Json(ResultadoJson<Recordatorios_Model>.ResultadoCorrecto(lista));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }


    }
}
