using MMS_Clases.CierreMes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases
{
    public class CierreMes_Model
    {
        public long Id { get; set;}
        public ComboMeses Mes { get; set; }
        public string Anio { get; set; }
        public double Monto { get; set; }
        public string Detalle { get; set; }
        public bool Activo { get; set; }
        public DateTime Fecha { get; set; }
        public Reportes_Model Grafico { get; set; }
    }
}
