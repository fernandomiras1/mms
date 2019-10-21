using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MMS_DLL;
using System.Threading.Tasks;
using MMS_Utiles;
using MMS_Clases;

namespace MMS_Clases.Controllers
{
    public class GeneralesController : Controller
    {
       
       public int ID_Entidad
        {
            get { return Convert.ToInt32(new CG().Pedir_Valoreas_a_Cookie("MMS-C", "IE").ToString()); }
          
        }

    }
}
