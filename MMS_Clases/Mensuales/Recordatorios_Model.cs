using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases.Mensuales
{
   public class Recordatorios_Model
    {
      public DateTime FechaVencimiento { get; set; }
      public string Nombre { get; set; }
      public string Tipo { get; set;}
      public string Observacion { get; set; }
    }
}
