using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_Entidad;
using Capa_de_Datos;

namespace Capa_de_Negocio
{
    public  class CN_PERMISO
    {
        private CD_PERMISOS objpermiso = new CD_PERMISOS();
        public List<PERMISO> Lista_Permiso(int idusuario)
        {
            return objpermiso.Ver_Permisos(idusuario);
        }
    }
}
