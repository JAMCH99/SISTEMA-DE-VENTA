using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_de_Datos;
using Capa_Entidad;

namespace Capa_de_Negocio
{
    public class CN_PROVEEDOR
    {
        private CD_PROVEEDOR obj_proveedor = new CD_PROVEEDOR();
        public List<PROVEEDOR> Listar()
        {
            return obj_proveedor.Listar();
        }

        public int Registrar(PROVEEDOR obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.Documento == "") { mensaje += "Es necesario el documento del PROVEEDOR\n"; }
            if (obj.RazonSocial == "") { mensaje += "Es necesario la razón social del PROVEEDOR\n"; }
            if (obj.Telefono == "") { mensaje += "Es necesario el teléfono del PROVEEDOR\n"; }
            if (obj.Correo == "") { mensaje += "Es necesario correo del PROVEEDOR\n"; }
            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return obj_proveedor.Registrar(obj, out mensaje);
            }
        }
        public bool Editar(PROVEEDOR obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.Documento == "") { mensaje += "Es necesario el documento del PROVEEDOR\n"; }
            if (obj.RazonSocial == "") { mensaje += "Es necesario la razón social del PROVEEDOR\n"; }
            if (obj.Telefono == "") { mensaje += "Es necesario el teléfono del PROVEEDOR\n"; }
            if (obj.Correo == "") { mensaje += "Es necesario correo del PROVEEDOR\n"; }
            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_proveedor.Editar(obj, out mensaje);
            }

        }

        public bool Eliminar(PROVEEDOR obj, out string mensaje)
        {
            return obj_proveedor.Eliminar(obj, out mensaje);
        }
    }
}
