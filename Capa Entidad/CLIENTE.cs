﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class CLIENTE
    {
        public int IdCliente { get; set; }
        public string  Documento { get; set; }
        public string  NombreCompleto { get; set; }
        public string  Correo { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }

    }
}
