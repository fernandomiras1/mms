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
    
    public partial class login
    {
        public long id { get; set; }
        public Nullable<int> Id_Entidad { get; set; }
        public string Entidad { get; set; }
        public string Usuario { get; set; }
        public string Pass { get; set; }
        public string Nombre_Apellido { get; set; }
        public string Dni { get; set; }
        public string Cargo { get; set; }
        public System.DateTime Fecha_Creacion { get; set; }
        public bool Activo { get; set; }
    
        public virtual entidades entidades { get; set; }
    }
}
