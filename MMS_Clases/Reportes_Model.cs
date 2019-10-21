using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases
{
    public class Reportes_Model
    {
        public Morris_Bar MBar { get; set; }
        public Morris_Area MArea { get; set; }
        public Morris_Donut MDonut { get; set;}

    }


    
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
        public string Nombre { get; set; }
        public double Total { get; set; }

    }


    // Area caht

    public class Morris_Area
    {
        public string element { get; set; }
        public List<Datos_Area> data { get; set; }
        public string xkey { get; set; }
        public List<string> ykeys { get; set; }
        public List<string> labels { get; set; }
        public string hideHover { get; set; }
        public int pointSize { get; set;}
        public bool resize { get; set; }
        public Morris_Area()
        {
            data = new List<Datos_Area>();
            ykeys = new List<string>();
            labels = new List<string>();
        }

    }
    public class Datos_Area
    {
        public string Anio { get; set; }
        public double Ingreso { get; set; }
        public double Egreso { get; set; }
        public double Total_Area { get { return Ingreso - Egreso; } }

    }

    
    
    // morris-donut-chart (El circulo.)

    public class Morris_Donut
    {
        public string element { get; set; }
        public List<Datos_Donut> data { get; set;}
        public bool resize { get; set;}
    }
    public class Datos_Donut
    {
        public string label { get; set;}
        public double value { get; set; }
    }


}
