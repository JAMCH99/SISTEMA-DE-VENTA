using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_de_Datos;

namespace Capa_de_Negocio
{
    public class CN_CATEGORIA
    {

        private CD_CATEGORIA obj_categoria = new CD_CATEGORIA();

        public List<CATEGORIA> Listar()
        {
            return obj_categoria.Listar();
        }
        public int Registrar(CATEGORIA obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.Descripcion == "") { mensaje += "Es necesario el nombre de la  Categoria\n"; }

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return obj_categoria.Registrar(obj, out mensaje);
            }
        }
        public bool Editar(CATEGORIA obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.Descripcion == "") { mensaje += "Es necesario el nombre de la  Categoria\n"; }
           
            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_categoria.Editar(obj, out mensaje);
            }

        }

        public bool Eliminar(CATEGORIA obj, out string mensaje)
        {
            return obj_categoria.Eliminar(obj, out mensaje);
        }
    }
}
