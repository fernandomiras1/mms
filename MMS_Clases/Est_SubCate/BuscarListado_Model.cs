using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases.Est_SubCate
{
   public class BuscarListado_Model
    {
       public BuscarListado_Model()
       {
           IdsSubCate = new List<int>();
       }
       public int Anio { get; set;}
       public Combo_Tipo Tipo { get; set;}
       public double Monto { get; set; }
       public Combo_Sub_Categorias SubCate { get; set; }
       public List<int> IdsSubCate { get; set; }
       
      
    }

   public class DatosxMes_BuscarListado_Model
   {
       public DatosxMes_BuscarListado_Model()
       {
           Datos = new List<BuscarListado_Model>();
       }
       public string Mes { get; set; }
       public double Total { get; set; }
       public List<BuscarListado_Model> Datos { get; set; }
       public List<Datos_Donut> Grafico { get; set; }

   }
}
