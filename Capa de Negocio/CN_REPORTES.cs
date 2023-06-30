using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_Entidad;
using Capa_de_Datos;

namespace Capa_de_Negocio
{
    public class CN_REPORTES
    {
        private CD_REPORTES obj_reportes = new CD_REPORTES();


        public List<REPORTE_COMPRAS> listarCompras(string finicio, string ffin, int idproveedor)
        {
            return obj_reportes.ListarCompras(finicio, ffin, idproveedor);
        }

        public List<REPORTE_VENTAS> listarVentas(string finicio, string ffin)
        {
            return obj_reportes.ListarVentas(finicio,ffin);
        }
    }
}
