using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Clases
{
  public class Home_Model
    {
      public long Id {get;set;}
      public Combo_Tipo Tipo {get;set;}
      public Combo_Categorias Cate { get; set; }
      public Combo_Sub_Categorias SubCate { get; set; }
      public string Observacion {get;set;}
      public double Precio {get;set;}
      public Combo_FormaPago Forma_Pago {get;set;}
      public DateTime Fecha { get; set;}
      public Reportes_Model  Grafico { get; set; }


    }

}
