using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class PERMISO
    {
        public int IdPermiso { get; set; }
        public ROL Rol { get; set; }
        public string NombreMenu { get; set; }
        public string FechaCreacion { get; set; }
    }
}
