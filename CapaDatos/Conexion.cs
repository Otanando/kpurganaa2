using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    public class Conexion
    {

        public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}

//public static string cn = ConfigurationMaanager.ConnectionStrings["cadena"].ToString()