using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data.Entity;
using MMS_DLL;
using MMS_Utiles;
using MMS_Clases;
using MMS_Clases.Mensuales;


namespace MMS_Clases.Controllers
{
    public class MensualesController : GeneralesController
    {

        public JsonResult ListadoIngreso()
        {
            try
            {
                var db = new mmsEntities();
                var Español = new System.Globalization.CultureInfo("es-ES");
                var Num_Mes = DateTime.Now.Month - 1;
                if (Num_Mes == 0) Num_Mes = DateTime.Now.Month;
                var NombreMes = Español.DateTimeFormat.GetMonthName(Num_Mes).ToUpper();

                var listaIngreso = db.ingesos_mensuales.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).OrderBy(p => p.combo_sub_categoria.Nombre).ToList();
                var resultado = new List<IngresosMensuales_Model>();
                foreach (var item in listaIngreso)
                {
                    //var Tipo = db.combo_tipo.FirstOrDefault(x => x.id == item.combo_sub_categoria.combo_categoria.Id_Tipo);
                    resultado.Add(new IngresosMensuales_Model
                    {
                        Id = item.id,
                        Nombre = new Sub_Categorias_Mensuales() { Id = item.combo_sub_categoria.id, Nombre = item.combo_sub_categoria.Nombre },
                        Categoria = new Combo_Categorias() { Id = item.combo_sub_categoria.combo_categoria.id, Nombre = item.combo_sub_categoria.combo_categoria.Nombre },
                        Tipo = new Combo_Tipo() { id = item.combo_sub_categoria.combo_categoria.combo_tipo.id, Nombre = item.combo_sub_categoria.combo_categoria.combo_tipo.Nombre },
                        Detalle = item.Detalle,
                        FechaVencimiento = item.Fecha_Vencimiento.Value,
                        Precio = item.Precio.Value,
                        Pagado = item.Pagado.Value,
                        Activo = item.Activo,
                        Recordatorio = item.Recordatorio.Value,
                        Num_MesAnterior = NombreMes

                    });
                }
                return Json(ResultadoJson<IngresosMensuales_Model>.ResultadoCorrecto(resultado));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }

        public JsonResult ObtenerSubCategorias()
        {
            try
            {
                var db = new mmsEntities();
                var lista = db.combo_sub_categoria.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).ToList();
                var resultado = new List<Sub_Categorias_Mensuales>();

                foreach (var item in lista)
                {
                    var Tipo = db.combo_tipo.FirstOrDefault(x => x.id == item.combo_categoria.Id_Tipo);
                    resultado.Add(new Sub_Categorias_Mensuales
                        {
                            Id = item.id,
                            Nombre = item.Nombre,
                            Categoria = new Combo_Categorias() { Id = item.combo_categoria.id, Nombre = item.combo_categoria.Nombre },
                            Tipo = new Combo_Tipo() { id = Tipo.id, Nombre = Tipo.Nombre },

                        });

                }
                return Json(ResultadoJson<Sub_Categorias_Mensuales>.ResultadoCorrecto(resultado));
            }

            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }


        public JsonResult GuardarIngreso(IngresosMensuales_Model Objeto)
        {
            try
            {
                var db = new mmsEntities();
                if (Objeto.Id == 0)
                {
                    // Guardamos.
                    var resultado = new MMS_DLL.ingesos_mensuales
                    {
                        Fecha_Insert = DateTime.Now,
                        Fecha_Vencimiento = Objeto.FechaVencimiento,
                        Id_Entidad = ID_Entidad,
                        Id_SubCategoria = Objeto.Nombre.Id,
                        Detalle = Objeto.Detalle != null ? Objeto.Detalle : "-",
                        Precio = Objeto.Precio,
                        Pagado = Objeto.Pagado,
                        Recordatorio = Objeto.Recordatorio,
                        Activo = true
                    };
                    db.ingesos_mensuales.Add(resultado);
                    var resu = db.SaveChanges();
                    if (resu > 0)
                        return Json(ResultadoJson<IngresosMensuales_Model>.ResultadoCorrecto("El Ingreso fue guardado con Exito"));
                    else
                        return Json(ResultadoJson<IngresosMensuales_Model>.ResultadoInCorrecto("Error al guardar el Ingreso"));
                }
                else
                {
                    //Modificamos.
                    var Objeto_Modificar = db.ingesos_mensuales.FirstOrDefault(x => x.id == Objeto.Id && x.Id_Entidad == ID_Entidad && x.Activo);
                    Objeto_Modificar.Id_SubCategoria = Objeto.Nombre.Id;
                    Objeto_Modificar.Fecha_Vencimiento = Objeto.FechaVencimiento;
                    Objeto_Modificar.Precio = Objeto.Precio;
                    Objeto_Modificar.Pagado = Objeto.Pagado;
                    Objeto_Modificar.Recordatorio = Objeto.Recordatorio;
                    Objeto_Modificar.Detalle = Objeto.Detalle != null ? Objeto.Detalle : "-";
                    var res = db.SaveChanges();
                    if (res > 0)
                        return Json(ResultadoJson<IngresosMensuales_Model>.ResultadoCorrecto("El Ingreso fue modificado con exito"));
                    else
                        return Json(ResultadoJson<IngresosMensuales_Model>.ResultadoInCorrecto("Error al modificar el Ingreso"));

                }

            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }

        public JsonResult EliminarMensual_Ingreso(IngresosMensuales_Model Objeto)
        {

            try
            {
                var db = new mmsEntities();
                var ObjetoEliminar = db.ingesos_mensuales.FirstOrDefault(x => x.id == Objeto.Id && x.Id_Entidad == ID_Entidad);
                ObjetoEliminar.Activo = false;
                var res = db.SaveChanges();
                if (res > 0)
                    return Json(ResultadoJson<IngresosMensuales_Model>.ResultadoCorrecto("El Ingreso fue eliminado con exito"));
                else
                    return Json(ResultadoJson<IngresosMensuales_Model>.ResultadoInCorrecto("Error al eliminar el Ingreso"));

            }
            catch (Exception ex)
            {

                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }


        }


        public JsonResult EstadoPagado(Estado_Pagado_Model Objeto)
        {

            try
            {
                var db = new mmsEntities();
                var existente = db.ingresos.FirstOrDefault(p => p.Activo && p.Observación == Objeto.Observacion && p.Precio == Objeto.Precio && p.Fecha.Day == Objeto.Fecha.Day && p.Fecha.Month == Objeto.Fecha.Month && p.Fecha.Year == Objeto.Fecha.Year);
                if (existente != null)
                {
                    return Json(ResultadoJson.ResultadoInCorrecto("El Registro Ingresado ya existe"));
                }
             
                // Guardamos.
                var objeto_modelo = new MMS_DLL.ingresos
                {

                    Id_Tipo = Objeto.Tipo.id,
                    Id_Categoria = Objeto.Categoria.Id,
                    Id_Entidad = ID_Entidad,
                    Id_SubCategoria = Objeto.Nombre.Id,
                    Observación = Objeto.Observacion != null ? Objeto.Observacion : "-",
                    Precio = Objeto.Precio,
                    Id_Forma_Pago = Objeto.Forma_Pago.Id,
                    Fecha = Objeto.Fecha,
                    
                    Activo = true

                };
                db.ingresos.Add(objeto_modelo);

                //Pasamos en Estado Pagado el IngresoMensual.
                var IngresoMensual = db.ingesos_mensuales.FirstOrDefault(x => x.Id_Entidad == ID_Entidad && x.Activo && x.id == Objeto.Id);
                IngresoMensual.Pagado = true;
                var res = db.SaveChanges();
                if (res > 0)
                    return Json(ResultadoJson<Home_Model>.ResultadoCorrecto("Guardado con Exito"));
                else
                    return Json(ResultadoJson<Home_Model>.ResultadoInCorrecto("Error al Igresar el Registro"));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<Home_Model>.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }

        /////// EGRESOS ///////////

        public JsonResult Obtener_ListadoEgreso()
        {
            try
            {
                var db = new mmsEntities();
                var Lista = db.egresos_mensuales.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).OrderBy(p => p.combo_sub_categoria.Nombre).ToList();
                var resultado = new List<EgresosMensuales_Model>();
                foreach (var item in Lista)
                {
                    resultado.Add(new EgresosMensuales_Model
                    {
                        Id = item.id,
                        Nombre = new Sub_Categorias_Mensuales() { Id = item.combo_sub_categoria.id, Nombre = item.combo_sub_categoria.Nombre },
                        Categoria = new Combo_Categorias() { Id = item.combo_sub_categoria.combo_categoria.id, Nombre = item.combo_sub_categoria.combo_categoria.Nombre },
                        Tipo = new Combo_Tipo() { id = item.combo_sub_categoria.combo_categoria.combo_tipo.id, Nombre = item.combo_sub_categoria.combo_categoria.combo_tipo.Nombre },
                        Detalle = item.Detalle,
                        FechaVencimiento = item.Fecha_Vencimiento.Value,
                        Precio = item.Precio.Value,
                        Pagado = item.Pagado.Value,
                        Activo = item.Activo,
                        Recordatorio = item.Recordatorio.Value

                    });
                }
                return Json(ResultadoJson<EgresosMensuales_Model>.ResultadoCorrecto(resultado));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }

        public JsonResult GuardarEgreso(EgresosMensuales_Model Objeto)
        {
            try
            {
                var db = new mmsEntities();
                if(Objeto.Id == 0)
                {
                    //Guardamos
                    var resultado = new MMS_DLL.egresos_mensuales
                    {
                        Fecha_Insert = DateTime.Now,
                        Fecha_Vencimiento = Objeto.FechaVencimiento,
                        Id_Entidad = ID_Entidad,
                        Id_SubCategoria = Objeto.Nombre.Id,
                        Detalle = Objeto.Detalle != null ? Objeto.Detalle : "-",
                        Precio = Objeto.Precio,
                        Pagado = Objeto.Pagado,
                        Recordatorio = Objeto.Recordatorio,
                        Activo = true

                    };
                    db.egresos_mensuales.Add(resultado);
                    var resu = db.SaveChanges();
                    if (resu > 0)
                        return Json(ResultadoJson<EgresosMensuales_Model>.ResultadoCorrecto("El Egreso fue guardado con Exito"));
                    else
                        return Json(ResultadoJson<EgresosMensuales_Model>.ResultadoInCorrecto("Error al guardar el Egreso"));
              }

                else
                {
                    //Modificamos.
                    var IdEgreso = db.egresos_mensuales.FirstOrDefault(x => x.Activo && x.Id_Entidad == ID_Entidad && x.id == Objeto.Id);
                    IdEgreso.Id_SubCategoria = Objeto.Nombre.Id;
                    IdEgreso.Fecha_Vencimiento = Objeto.FechaVencimiento;
                    IdEgreso.Precio = Objeto.Precio;
                    IdEgreso.Pagado = Objeto.Pagado;
                    IdEgreso.Recordatorio = Objeto.Recordatorio;
                    IdEgreso.Detalle = Objeto.Detalle != null ? Objeto.Detalle : "-";
                    var res = db.SaveChanges();
                    if (res > 0)
                        return Json(ResultadoJson<EgresosMensuales_Model>.ResultadoCorrecto("El Egreso fue modificado con exito"));
                    else
                        return Json(ResultadoJson<EgresosMensuales_Model>.ResultadoInCorrecto("Error al modificar el Egreso"));

                }

            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }


        public JsonResult EliminarMensual_Egreso(EgresosMensuales_Model Objeto)
        {
            try
            {
                var db = new mmsEntities();
                var ObjetoEliminar = db.egresos_mensuales.FirstOrDefault(x => x.id == Objeto.Id && x.Id_Entidad == ID_Entidad);
                ObjetoEliminar.Activo = false;
                var res = db.SaveChanges();
                if (res > 0)
                    return Json(ResultadoJson<EgresosMensuales_Model>.ResultadoCorrecto("El Egreso fue eliminado con exito"));
                else
                    return Json(ResultadoJson<EgresosMensuales_Model>.ResultadoInCorrecto("Error al eliminar el Egreso"));

            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }


        }

        public JsonResult EstadoPagadoEgreso(Estado_Pagado_Model Objeto)
        {

            try
            {
                var db = new mmsEntities();
                var existente = db.ingresos.FirstOrDefault(p => p.Activo && p.Observación == Objeto.Observacion && p.Precio == Objeto.Precio && p.Fecha.Day == Objeto.Fecha.Day && p.Fecha.Month == Objeto.Fecha.Month && p.Fecha.Year == Objeto.Fecha.Year);
                if (existente != null)
                {
                    return Json(ResultadoJson.ResultadoInCorrecto("El Registro Ingresado ya existe"));
                }
                // Guardamos.
                var objeto_modelo = new MMS_DLL.ingresos
                {

                    Id_Tipo = Objeto.Tipo.id,
                    Id_Categoria = Objeto.Categoria.Id,
                    Id_Entidad = ID_Entidad,
                    Id_SubCategoria = Objeto.Nombre.Id,
                    Observación = Objeto.Observacion != null ? Objeto.Observacion : "-",
                    Precio = Objeto.Precio,
                    Id_Forma_Pago = Objeto.Forma_Pago.Id,
                    Fecha = Objeto.Fecha,
                    Activo = true

                };
                db.ingresos.Add(objeto_modelo);

                //Pasamos en Estado Pagado el IngresoMensual.
                var EgresoMensual = db.egresos_mensuales.FirstOrDefault(x => x.Id_Entidad == ID_Entidad && x.Activo && x.id == Objeto.Id);
                EgresoMensual.Pagado = true;
                var res = db.SaveChanges();
                if (res > 0)
                    return Json(ResultadoJson<Home_Model>.ResultadoCorrecto("Guardado con Exito"));
                else
                    return Json(ResultadoJson<Home_Model>.ResultadoInCorrecto("Error al Igresar el Registro"));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<Home_Model>.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }


    }

}
