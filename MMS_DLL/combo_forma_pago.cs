//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MMS_DLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class combo_forma_pago
    {
        public combo_forma_pago()
        {
            this.ingresos = new HashSet<ingresos>();
        }
    
        public int id { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<ingresos> ingresos { get; set; }
    }
}
