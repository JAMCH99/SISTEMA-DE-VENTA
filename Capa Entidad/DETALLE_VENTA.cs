using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class DETALLE_VENTA
    {
        public int IdDetalle_Venta { get; set; }
        public PRODUCTO Producto { get; set; }

        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public string FechaRegistro { get; set; }
    }
}
