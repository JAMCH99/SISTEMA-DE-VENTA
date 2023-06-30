using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class VENTA
    {
        public int IdVenta { get; set; }
        public USUARIO Usuario { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoCambio { get; set; }
        public decimal MontoTotal { get; set; }

        public List<DETALLE_VENTA> list_Detalle_Venta { get; set; }
        public string FechaRegistro { get; set; }
    }
}
