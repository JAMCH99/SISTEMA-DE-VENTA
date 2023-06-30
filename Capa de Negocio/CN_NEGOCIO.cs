using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_de_Datos;
using Capa_Entidad;

namespace Capa_de_Negocio
{
     public class CN_NEGOCIO
    {
        private CD_NEGOCIO obj_NEGOCIO = new CD_NEGOCIO();
        public NEGOCIO Obtener_Datos()
        {
            return obj_NEGOCIO.ObtenerDatos();
        }

        public bool GuardarDatos(NEGOCIO obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.Nombre == "") { mensaje += "Es necesario el documento del NEGOCIO\n"; }
            if (obj.Ruc== "") { mensaje += "Es necesario el nombre del NEGOCIO\n"; }
            if (obj.Direccion == "") { mensaje += "Es necesario la dirección del NEGOCIO\n"; }

            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_NEGOCIO.GuardarDatos(obj, out mensaje);
            }
        }
        public byte[] ObtenerLogo(out bool Resultado)
        {
            return obj_NEGOCIO.ObtenerLogo(out Resultado);

        }

        public bool ActualizarLogo(byte[]imagen, out string mensaje)
        {
            return obj_NEGOCIO.Actualizar_Logo(imagen, out mensaje);
        }
    }
}
