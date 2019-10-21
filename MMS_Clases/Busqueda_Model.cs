using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases
{
   public class Busqueda_Model
    {
       public DateTime Fecha_Desde {get; set;}
       public DateTime Fecha_Hasta {get; set;}
       public bool detallado {get; set;}
       public Combo_Tipo Tipo { get; set; }
       public Combo_Categorias Cate { get; set; }
       public Combo_Sub_Categorias SubCate { get; set; }
    }
}
