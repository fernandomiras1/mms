using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases.Mensuales
{
    public class Estado_Pagado_Model
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Combo_Tipo Tipo { get; set; }
        public Combo_Sub_Categorias Nombre { get; set; }
        public Combo_Categorias Categoria { get; set; }
        public string Observacion { get; set; }
        public double Precio { get; set; }
        public bool Recordatorio { get; set; }
        public Combo_FormaPago Forma_Pago { get; set; }
    }


}
