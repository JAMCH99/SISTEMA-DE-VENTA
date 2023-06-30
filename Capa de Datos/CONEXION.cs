using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// se necesita agregar esta referencia para poder utilizar system.configuration
using System.Configuration;


namespace Capa_de_Datos
{
    public class CONEXION
    {
        public static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}
