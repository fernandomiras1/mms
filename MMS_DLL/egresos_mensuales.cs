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
    
    public partial class egresos_mensuales
    {
        public long id { get; set; }
        public Nullable<int> Id_Entidad { get; set; }
        public Nullable<System.DateTime> Fecha_Insert { get; set; }
        public Nullable<System.DateTime> Fecha_Vencimiento { get; set; }
        public Nullable<int> Id_SubCategoria { get; set; }
        public string Detalle { get; set; }
        public Nullable<double> Precio { get; set; }
        public Nullable<bool> Pagado { get; set; }
        public Nullable<bool> Recordatorio { get; set; }
        public bool Activo { get; set; }
    
        public virtual combo_sub_categoria combo_sub_categoria { get; set; }
        public virtual entidades entidades { get; set; }
    }
}
