using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data.Entity;
using MMS_DLL;
using MMS_Utiles;
using MMS_Clases;

namespace MMS_Clases.Controllers
{
    public class ConfiguracionController : GeneralesController
    {

        public JsonResult Obtener_ListadoCategorias()
        {
            try
            {

                var db = new mmsEntities();
                var lista = db.combo_categoria.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).OrderBy(p => p.Nombre).ToList();
                var resultado = new List<Categorias_Model>();
                foreach (var item in lista)
                {
                    resultado.Add(new Categorias_Model
                    {
                        Id = item.id,
                        Nombre = item.Nombre,
                        Tipo = new Combo_Tipo() { id = item.combo_tipo.id, Nombre = item.combo_tipo.Nombre },
                        Fecha = item.Fecha_Proceso.Value

                    });

                }

                return Json(ResultadoJson<Categorias_Model>.ResultadoCorrecto(resultado));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<Categorias_Model>.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }


        public JsonResult GuardarCategoria(string Nombre, int id_Tipo)
        {
            try
            {
                var db = new mmsEntities();
                var existe = db.combo_categoria.FirstOrDefault(x => x.Id_Entidad == ID_Entidad && x.Activo && x.Nombre.ToLower() == Nombre.ToLower() && x.Id_Tipo == id_Tipo);
                if (existe != null)
                {
                    return Json(ResultadoJson.ResultadoInCorrecto("El Registro Ingresado ya existe"));
                }

                var ObjetoGuardar = new MMS_DLL.combo_categoria
                {
                    Nombre = Nombre,
                    Id_Tipo = id_Tipo,
                    Id_Entidad = ID_Entidad,
                    Fecha_Proceso = DateTime.Now,
                    Activo = true

                };
                db.combo_categoria.Add(ObjetoGuardar);
                var res = db.SaveChanges();
                if (res > 0)
                    return Json(ResultadoJson<Categorias_Model>.ResultadoCorrecto("Guardado con Exito"));
                else
                    return Json(ResultadoJson<Categorias_Model>.ResultadoInCorrecto("Error al Guardar el Registro"));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }


        public JsonResult ObtenerListado_SubCate()
        {
            try
            {

                var db = new mmsEntities();
                var lista = db.combo_sub_categoria.Where(x => x.Id_Entidad == ID_Entidad && x.Activo).OrderBy(p => p.Nombre).ToList();
                var resultado = new List<SubCategorias_Model>();
                foreach (var item in lista)
                {
                    resultado.Add(new SubCategorias_Model
                    {
                        Id = item.id,
                        Categoria = new Combo_Categorias()
                        {
                            Id = item.combo_categoria != null ? item.combo_categoria.id : 0,
                            Nombre = item.combo_categoria != null ? item.combo_categoria.Nombre : null,
                            Tipo = item.combo_categoria != null ? item.combo_categoria.combo_tipo.Nombre : null
                        },
                        Nombre = item.Nombre,
                        Fecha = item.Fecha_Proceso.Value

                    });
                }
                return Json(ResultadoJson<SubCategorias_Model>.ResultadoCorrecto(resultado));

            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<SubCategorias_Model>.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }

        public JsonResult ModificarCate(Categorias_Model modelo)
        {
            try
            {
                var db = new mmsEntities();
                var Modelo_Modificar = db.combo_categoria.FirstOrDefault(x => x.id == modelo.Id && x.Activo);
                Modelo_Modificar.Id_Tipo = modelo.Tipo.id;
                Modelo_Modificar.Nombre = modelo.Nombre;

                var Modifico_Ingresos = db.ingresos.Where(i => i.Id_Entidad == ID_Entidad && i.Activo && i.Id_Categoria == modelo.Id).ToList();
                foreach (var item in Modifico_Ingresos)
                {
                    item.Id_Categoria = modelo.Id;
                    item.Id_Tipo = modelo.Tipo.id;
                }
                var Modifico_SubCate = db.combo_sub_categoria.Where(i => i.Id_Entidad == ID_Entidad && i.Activo && i.Id_Categoria == modelo.Id).ToList();
                Modifico_SubCate.ForEach(x => x.Id_Categoria = modelo.Id);
                var res = db.SaveChanges();
                if (res > 0)
                    return Json(ResultadoJson<Categorias_Model>.ResultadoCorrecto("Categoría Modificada con exito"));
                else
                    return Json(ResultadoJson<Categorias_Model>.ResultadoInCorrecto("Error al Modificar la Categoría"));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson<Categorias_Model>.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }

        public JsonResult EliminarCate(Categorias_Model modelo)
        {

            try
            {
                var db = new mmsEntities();
                var Modelo_Modificar = db.combo_categoria.FirstOrDefault(x => x.id == modelo.Id && x.Activo);
                Modelo_Modificar.Activo = false;
                var Modifico_Ingresos = db.ingresos.Where(i => i.Id_Entidad == ID_Entidad && i.Activo && i.Id_Categoria == modelo.Id).ToList();
                Modifico_Ingresos.ForEach(x => x.Activo = false);
              
                var Modifico_SubCate = db.combo_sub_categoria.Where(i => i.Id_Entidad == ID_Entidad && i.Activo && i.Id_Categoria == modelo.Id).ToList();
                Modifico_SubCate.ForEach(x => x.Activo = false);
                var res = db.SaveChanges();
                if (res > 0)
                    return Json(ResultadoJson.ResultadoCorrecto("Eliminado con Exito"));
                else
                    return Json(ResultadoJson.ResultadoInCorrecto("Error al Eliminar el Registro"));
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

        public JsonResult GuardarSubCate(int Id_Cate, string Nombre)
        {
            try
            {
                var db = new mmsEntities();
                var Existe = db.combo_sub_categoria.FirstOrDefault(x => x.Id_Entidad == ID_Entidad && x.Activo && x.Id_Categoria == Id_Cate && x.Nombre.ToLower() == Nombre.ToLower());
                if (Existe != null)
                {
                    return Json(ResultadoJson.ResultadoInCorrecto("La Sub Categoría ya existe"));
                }
                var Resultado = new MMS_DLL.combo_sub_categoria
                {
                    Id_Categoria = Id_Cate,
                    Activo = true,
                    Pagado = true,
                    Fecha_Proceso = DateTime.Now,
                    Id_Entidad = ID_Entidad,
                    Nombre = Nombre
                };
                db.combo_sub_categoria.Add(Resultado);
                var res = db.SaveChanges();
                if (res > 0)
                {
                    return Json(ResultadoJson.ResultadoCorrecto("La Sub Categoría fue guardada con exito"));
                }
                else return Json(ResultadoJson.ResultadoInCorrecto("Error al Guardar el Registro"));


            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }


        public JsonResult ModificarSubCate(SubCategorias_Model modelo)
        {

            try
            {
                var db = new mmsEntities();
                var Modelo_Modificar = db.combo_sub_categoria.FirstOrDefault(x => x.id == modelo.Id && x.Activo);
                Modelo_Modificar.Id_Categoria = modelo.Categoria.Id;
                Modelo_Modificar.Nombre = modelo.Nombre;

                var Modifico_Ingresos = db.ingresos.Where(i => i.Id_Entidad == ID_Entidad && i.Activo && i.Id_SubCategoria == modelo.Id).ToList();
                var TipoCate = db.combo_tipo.FirstOrDefault(x => x.Nombre == modelo.Categoria.Tipo);
                foreach (var item in Modifico_Ingresos)
                {
                    item.Id_Categoria = modelo.Categoria.Id;
                    item.Id_SubCategoria = modelo.Id;
                    item.Id_Tipo = TipoCate.id;
                }
           
                var res = db.SaveChanges();
                if (res > 0)
                {
                    return Json(ResultadoJson.ResultadoCorrecto("Sub Categoría Modificada con exito"));
                }
                else
                {
                    return Json(ResultadoJson.ResultadoInCorrecto("Error al Modificar la Categoría"));
                }

            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }

        public JsonResult EliminarSubCate(SubCategorias_Model Objeto)
        {
            try
            {
                var db = new mmsEntities();
                var Modelo_Modificar = db.combo_sub_categoria.FirstOrDefault(x => x.id == Objeto.Id && x.Activo);
                Modelo_Modificar.Activo = false;
                var Modifico_Ingresos = db.ingresos.Where(i => i.Id_Entidad == ID_Entidad && i.Activo && i.Id_SubCategoria == Objeto.Id).ToList();
                Modifico_Ingresos.ForEach(x => x.Activo = false);
                var res = db.SaveChanges();
                if (res > 0)
                    return Json(ResultadoJson.ResultadoCorrecto("Sub Categoría Eliminado con exito"));
                else
                    return Json(ResultadoJson.ResultadoInCorrecto("Error al Eliminar la Sub Categoría"));
            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }
        }


    }
}
