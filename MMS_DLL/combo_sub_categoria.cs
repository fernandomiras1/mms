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
    
    public partial class combo_sub_categoria
    {
        public combo_sub_categoria()
        {
            this.ingesos_mensuales = new HashSet<ingesos_mensuales>();
            this.ingresos = new HashSet<ingresos>();
            this.egresos_mensuales = new HashSet<egresos_mensuales>();
        }
    
        public int id { get; set; }
        public Nullable<int> Id_Entidad { get; set; }
        public Nullable<int> Id_Categoria { get; set; }
        public Nullable<System.DateTime> Fecha_Proceso { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public Nullable<bool> Pagado { get; set; }
    
        public virtual combo_categoria combo_categoria { get; set; }
        public virtual entidades entidades { get; set; }
        public virtual ICollection<ingesos_mensuales> ingesos_mensuales { get; set; }
        public virtual ICollection<ingresos> ingresos { get; set; }
        public virtual ICollection<egresos_mensuales> egresos_mensuales { get; set; }
    }
}
