using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_de_Datos;
using Capa_Entidad;

namespace Capa_de_Negocio
{
    public class CN_ROL
    {
        private CD_Rol objRol= new CD_Rol();

       public List<ROL> lista()
        {
            return objRol.Lista();
        }
    }
}
