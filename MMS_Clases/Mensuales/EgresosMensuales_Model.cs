using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases.Mensuales
{
    public class EgresosMensuales_Model
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public Sub_Categorias_Mensuales Nombre { get; set; }
        public Combo_Categorias Categoria { get; set; }
        public Combo_Tipo Tipo { get; set; }
        public string Detalle { get; set; }
        public bool Pagado { get; set; }
        public double Precio { get; set; }
        public bool Recordatorio { get; set; }
        public bool Activo { get; set; }
    }
            
}
