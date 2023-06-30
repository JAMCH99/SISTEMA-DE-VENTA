using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class COMPRA
    {

        public int IdCompra { get; set; }
        public USUARIO Usuario { get; set; }
        public PROVEEDOR Proveedor { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal MontoTotal { get; set; }

        //la siguiente propiedad era de detalle_compra "IdCompra" lo reemplaza
        public List<DETALLE_COMPRA> lista_Detalle_Compra { get; set; }
        public string FechaRegistro { get; set; }

    }
}
