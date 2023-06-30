using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_de_Datos;
using Capa_Entidad;

namespace Capa_de_Negocio
{
    public class CN_PRODUCTOS
    {
        private CD_PRODUCTO objProducto = new CD_PRODUCTO();

        public List<PRODUCTO> Listar()
        {
            return objProducto.ListarProducto();
        }


        public int Registrar(PRODUCTO obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.Codigo == "") { mensaje += "Es necesario el Codigo del Producto\n"; }
            if (obj.Nombre == "") { mensaje += "Es necesario el Nombre del Producto\n"; }
            if (obj.Descripcion == "") { mensaje += "Es necesario la Descripción del Producto\n"; }
            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objProducto.Registrar(obj, out mensaje);
            }
        }
        public bool Editar(PRODUCTO obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.Codigo == "") { mensaje += "Es necesario el Codigo del Producto\n"; }
            if (obj.Nombre == "") { mensaje += "Es necesario el Nombre del Producto\n"; }
            if (obj.Descripcion == "") { mensaje += "Es necesario la Descripción del Producto\n"; }
            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objProducto.Editar(obj, out mensaje);
            }

        }

        public bool Eliminar(PRODUCTO obj, out string mensaje)
        {
            return objProducto.Eliminar(obj, out mensaje);
        }
    }

}

