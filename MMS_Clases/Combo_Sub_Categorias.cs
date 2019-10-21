using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases
{
   public class Combo_Sub_Categorias
    {
       public int Id {get; set;}
       public string Tipo { get; set; }
       public string Nombre {get; set;}
       public string Borrar {get; set;}
       public DateTime Fecha {get; set;}

    }


   public class Sub_Categorias_Mensuales
   {
       public int Id { get; set; }
       public Combo_Tipo Tipo { get; set; }
       public Combo_Categorias Categoria { get; set; }
       public string Nombre { get; set; }
       public bool Activo { get; set; }
     

   }

}
