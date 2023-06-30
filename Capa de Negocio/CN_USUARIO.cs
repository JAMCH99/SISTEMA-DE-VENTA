using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_de_Datos;
using Capa_Entidad;

namespace Capa_de_Negocio
{
    public class CN_USUARIO
    {
        private CD_USUARIO obj_usuario = new CD_USUARIO();
          public List<USUARIO> Listar()
         {
           return obj_usuario.Listar();
         }

        public int Registrar(USUARIO obj,out string mensaje)   
        {
            mensaje = string.Empty;
            if (obj.Documento == "") { mensaje+="Es necesario el documento del usuario\n"; }
            if (obj.NombreCompleto == "") { mensaje += "Es necesario el nombre del usuario\n"; }
            if (obj.Correo == "") { mensaje+="Es necesario el correo del usuario\n";}
            if (obj.Clave == "") { mensaje+="Es necesario la clave del usuario\n"; }
            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
            return obj_usuario.Registrar(obj, out mensaje); 
            }
        }
        public bool Editar(USUARIO obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.Documento == "") { mensaje += "Es necesario el documento del usuario\n"; }
            if (obj.NombreCompleto == "") { mensaje += "Es necesario el nombre del usuario\n"; }
            if (obj.Clave == "") { mensaje += "Es necesario la clave del usuario\n"; }
            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_usuario.Editar(obj, out mensaje);
            }
            
        }

        public bool Eliminar(USUARIO obj, out string mensaje)
        {
            return obj_usuario.Eliminar(obj, out mensaje);
        }
    }

    
}
