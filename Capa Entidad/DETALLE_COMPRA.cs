using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class DETALLE_COMPRA
    {
        public int IdDetalleCompra { get; set; }

        //IdCompra se encuentra en Compra convertida en lista
        public PRODUCTO Producto { get; set; }
        public PROVEEDOR Proveedor { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Montototal { get; set; }

        public string FechaRegistro { get; set; }
    }
}
