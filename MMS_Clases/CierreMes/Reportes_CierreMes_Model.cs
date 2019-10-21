using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases.CierreMes
{
    public class Reportes_CierreMes_Model
    {
        //public class Reportes_Model
        //{
        //    public Morris_Bar MBar { get; set; }
        
        //}



        public class Morris_Bar
        {
            public string element { get; set; }
            public List<Datos> data { get; set; }
            public string xkey { get; set; }
            public List<string> ykeys { get; set; }
            public List<string> labels { get; set; }
            public string hideHover { get; set; }
            public bool resize { get; set; }
            public Morris_Bar()
            {
                data = new List<Datos>();
                ykeys = new List<string>();
                labels = new List<string>();
            }

        }
        public class Datos
        {
            public string Anio { get; set; }
            public double Monto { get; set; }

        }



    }
}
