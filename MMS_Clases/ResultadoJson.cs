using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS_Clases
{
    public class ResultadoJson<T> where T :class
    {
        public T Obejto { get; set; }
        public List<T> Lista { get; set; }
        public string Mensaje { get; set; }
        public bool Ok { get; set; }

        public int Total { get; set; }

        public static ResultadoJson<T> ResultadoCorrecto()
        {
            return new ResultadoJson<T> { Ok = true };

        }

        public static ResultadoJson<T> ResultadoCorrecto(string msj)
        {
            return new ResultadoJson<T> { Ok = true, Mensaje = msj };

        }

        public static ResultadoJson<T> ResultadoCorrecto(T obj)
        {
            return new ResultadoJson<T> { Ok = true, Obejto = obj };

        }

        public static ResultadoJson<T> ResultadoCorrecto(List<T> list)
        {
            return new ResultadoJson<T> { Ok = true, Lista = list };

        }

        public static ResultadoJson<T> ResultadoCorrecto(T obj, string mens)
        {
            return new ResultadoJson<T> { Ok = true, Obejto = obj, Mensaje = mens };

        }

        public static ResultadoJson<T> ResultadoCorrecto(List<T> list, string mens)
        {
            return new ResultadoJson<T> { Ok = true, Lista = list , Mensaje = mens};

        }


        public static ResultadoJson<T> ResultadoCorrecto(List<T> list, int total)
        {
            return new ResultadoJson<T> { Ok = true, Lista = list, Total = total };

        }
        public static ResultadoJson<T> ResultadoCorrecto(List<T> list, int total, string mens)
        {
            return new ResultadoJson<T> { Ok = true, Lista = list, Mensaje = mens, Total = total };

        }



        public static ResultadoJson<T> ResultadoInCorrecto()
        {
            return new ResultadoJson<T> { Ok = false };

        }

        public static ResultadoJson<T> ResultadoInCorrecto(T obj)
        {
            return new ResultadoJson<T> { Ok = false, Obejto = obj };

        }

        public static ResultadoJson<T> ResultadoInCorrecto(List<T> list)
        {
            return new ResultadoJson<T> { Ok = false, Lista = list };

        }

        public static ResultadoJson<T> ResultadoInCorrecto(T obj, string mens)
        {
            return new ResultadoJson<T> { Ok = false, Obejto = obj, Mensaje = mens };

        }

        public static ResultadoJson<T> ResultadoInCorrecto(List<T> list, string mens)
        {
            return new ResultadoJson<T> { Ok = false, Lista = list, Mensaje = mens };

        }

        public static ResultadoJson<T> ResultadoInCorrecto(string msj)
        {
            return new ResultadoJson<T> { Ok = false, Mensaje = msj };

        }
    }


    public class ResultadoJson
    {

        public string Mensaje { get; set; }
        public bool Ok { get; set; }
        public static ResultadoJson ResultadoCorrecto(string msj)
        {
            return new ResultadoJson { Ok = true, Mensaje = msj };

        }
        public static ResultadoJson ResultadoInCorrecto(string msj)
        {
            return new ResultadoJson { Ok = false, Mensaje = msj };

        }
    }
}
