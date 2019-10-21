using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data.Entity;
using MMS_DLL;
using MMS_Utiles;
using MMS_Clases;
using MMS_Clases.Est_SubCate;

namespace MMS_Clases.Controllers
{
    public class Est_SubCateController : GeneralesController
    {
        public JsonResult BuscarListado(BuscarListado_Model Objeto)
        {
            try
            {
                var db = new mmsEntities();
                if (Objeto.IdsSubCate == null)
                {
                    Objeto.IdsSubCate = new List<int>();
                }
                var lista = db.ingresos.Where(x => x.Id_Entidad == ID_Entidad && x.Activo && x.combo_tipo.id == Objeto.Tipo.id && x.Fecha.Year == Objeto.Anio && Objeto.IdsSubCate.Contains(x.Id_SubCategoria.Value)).GroupBy(p => new { p.Fecha.Month, p.combo_sub_categoria }).ToList();

                var lista_2 = lista.Select(p => new { Mes = p.Key.Month, IdSubCategoria = p.Key.combo_sub_categoria.id, SubCategoria = p.Key.combo_sub_categoria.Nombre, Precio = p.Sum(q => q.Precio.Value) });

                //var Grafico = lista.GroupBy(x => x.combo_sub_categoria.Nombre).ToList();
                var resultado = new List<DatosxMes_BuscarListado_Model>();
                var meses = lista_2.Select(p => p.Mes).Distinct();

                foreach (var mes in meses)
                {
                    var dato = new DatosxMes_BuscarListado_Model();


                    var Español = new System.Globalization.CultureInfo("es-ES");
                    var Num_Mes = Convert.ToInt32(mes.ToString());
                    if (Num_Mes == 0) Num_Mes = 1;
                    var NombreMes = Español.DateTimeFormat.GetMonthName(Num_Mes).ToUpper();
                    dato.Mes = NombreMes;

                    var datosGrilla = lista_2.Where(p => p.Mes == mes)
                                .Select(p => new BuscarListado_Model
                                {
                                    SubCate =  new Combo_Sub_Categorias()
                                    {
                                        Id = p.IdSubCategoria,
                                        Nombre = p.SubCategoria
                                    },
                                    Monto = p.Precio,
                                   
                                });

                    dato.Datos.AddRange(datosGrilla);
                    dato.Total = datosGrilla.Sum(q => q.Monto);
                    dato.Grafico = datosGrilla.Select(p => new Datos_Donut
                    {
                        label = p.SubCate.Nombre,
                        value = p.Monto
                    }).ToList();

                    resultado.Add(dato);
                }

                var res = Json(ResultadoJson<DatosxMes_BuscarListado_Model>.ResultadoCorrecto(resultado));
                res.MaxJsonLength = int.MaxValue;
                return res;


            }
            catch (Exception ex)
            {
                return Json(ResultadoJson.ResultadoInCorrecto(ex.Message));
                throw;
            }

        }


        public JsonResult ObtenerSubCategorias(Combo_Sub_Categorias Tipo)
        {
            try
            {
                var db = new mmsEntities();
                var lista = db.combo_sub_categoria.Where(x => x.Id_Entidad == ID_Entidad && x.Activo && x.combo_categoria.combo_tipo.id == Tipo.Id).ToList();
                var resultado = new List<Sub_Categorias_Mensuales>();

                foreach (var item in lista)
                {
                    var tipo = db.combo_tipo.FirstOrDefault(x => x.id == item.combo_categoria.Id_Tipo);
                    resultado.Add(new Sub_Categorias_Mensuales
                    {
                        Id = item.id,
                        Nombre = item.Nombre,
                        Categoria = new Combo_Categorias() { Id = item.combo_categoria.id, Nombre = item.combo_categoria.Nombre },
                        Tipo = new Combo_Tipo() { id = tipo.id, Nombre = Tipo.Nombre },

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



    }
}
