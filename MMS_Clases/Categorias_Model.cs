using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases
{
    public class Categorias_Model
    {
        public int Id { get; set; }
        public Combo_Tipo Tipo { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Borrar { get; set; }

    }
}
