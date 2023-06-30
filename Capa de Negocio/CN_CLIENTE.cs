using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_de_Datos;
using Capa_Entidad;

namespace Capa_de_Negocio
{
    public class CN_CLIENTE
    {
        CD_CLIENTE objcliente = new CD_CLIENTE();

        public List<CLIENTE> Listar()
        {
            return objcliente.Listar();
        }

        public int Registrar(CLIENTE obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.Documento == "") { mensaje += "Es necesario el documento del Cliente\n"; }
            if (obj.NombreCompleto == "") { mensaje += "Es necesario el nombre del Cliente\n"; }
            if (obj.Correo == "") { mensaje += "Es necesario el correo del Cliente\n"; }
            if (obj.Telefono == "") { mensaje += "Es necesario el teléfono del Cliente\n"; }
            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcliente.Registrar(obj, out mensaje);
            }
        }
        public bool Editar(CLIENTE obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.Documento == "") { mensaje += "Es necesario el documento del Cliente\n"; }
            if (obj.NombreCompleto == "") { mensaje += "Es necesario el nombre del Cliente\n"; }
            if (obj.Correo == "") { mensaje += "Es necesario la clave del Cliente\n"; }
            if (obj.Telefono == "") { mensaje += "Es necesario el teléfono del Cliente\n"; } 
            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcliente.Editar(obj, out mensaje);
            }

        }

        public bool Eliminar(CLIENTE obj, out string mensaje)
        {
            return objcliente.Eliminar(obj, out mensaje);
        }
    }
}
