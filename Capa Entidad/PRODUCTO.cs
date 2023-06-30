﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class PRODUCTO
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public CATEGORIA Categoria { get; set; }
        public int Stock { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }

        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }

    }
}
