using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_de_Datos;
using Capa_Entidad;

namespace Capa_de_Negocio
{
    public class CN_COMPRA
    {
        private CD_COMPRA obj_Compra = new CD_COMPRA();

        public int ObtenerCorrelativo()
        {
            return obj_Compra.ObtenerCorrelativo();
        }

        public bool registrar(COMPRA obj, DataTable detalle_compra, out string mensaje)
        {
            return obj_Compra.Registrar(obj, detalle_compra, out mensaje);
        }

        public COMPRA ObtenerCompra(string numerodocumento )
        {
            COMPRA ocompra = obj_Compra.ObtenerCompra(numerodocumento);

           
            if((ocompra.IdCompra != 0))
            {
                List<DETALLE_COMPRA> detalle_compra = obj_Compra.ObtenerDetalleCompra(ocompra.IdCompra);
                ocompra.lista_Detalle_Compra = detalle_compra;
            }
            return ocompra;
        }  
        
        public List<COMPRA> listanuemro_compra()
        {
            return obj_Compra.listaNumero_compra();
        }
    }
}
