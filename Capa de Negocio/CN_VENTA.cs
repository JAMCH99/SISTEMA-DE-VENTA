using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_de_Datos;
using Capa_Entidad;
using System.Data;

namespace Capa_de_Negocio
{
    public class CN_VENTA
    {
        private CD_VENTA obj_Venta = new CD_VENTA();

        public int ObtenerCorrelativo()
        {
            return obj_Venta.ObtenerCorrelativo();
        }

        public bool registrar(VENTA obj, DataTable detalle_venta, out string mensaje)
        {
            return obj_Venta.Registrar(obj, detalle_venta, out mensaje);
        }
        public bool Restar_Stock(int idproducto, int cantidad)
        {
            return obj_Venta.Restar_Stock(idproducto, cantidad);
        }
        public bool Sumar_Stock(int idproducto, int cantidad)
        {
            return obj_Venta.Sumar_Stock(idproducto, cantidad);
        }

        public VENTA Obtener_Venta(string numero)
        {
               VENTA venta = obj_Venta.ObtenerVenta(numero);

            if(venta.IdVenta!= 0)
            {
                List<DETALLE_VENTA> lista = obj_Venta.ObtenerDetalleVenta(venta.IdVenta);
                venta.list_Detalle_Venta = lista;
            }
            return venta;
                
        }
       
        public List<VENTA> Obtener_numero()
        {
            return obj_Venta.listaNumero_VENTA();
        }
    }
}
