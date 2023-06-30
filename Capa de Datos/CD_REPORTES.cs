using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using System.Data;
using System.Data.SqlClient;


namespace Capa_de_Datos
{
    public class CD_REPORTES
    {
        public List<REPORTE_COMPRAS> ListarCompras(string finicio, string ffin, int idproveedor)
        {
            string mensaje = "";
            List<REPORTE_COMPRAS> lista = new List<REPORTE_COMPRAS>();
            try
            {

                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    SqlCommand cmd = new SqlCommand("ReporteCompras".ToString(), cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("FechaInicio", finicio);
                    cmd.Parameters.AddWithValue("FechaFin", ffin);
                    cmd.Parameters.AddWithValue("IdProveedor", idproveedor);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new REPORTE_COMPRAS()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumneto = dr["TipoDocumento"].ToString(),
                                NumerDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoProveedor = dr["DocumentoProveedor"].ToString(),
                                RazonSocial = dr["RazonSocial"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Categoria = dr["CategoriaProducto"].ToString(),
                                PrecioCompra = dr["PrecioCompra"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                Subtotal = dr["Subtotal"].ToString(),

                            });


                        }
                    }

                }

            }
            catch (Exception ex)
            {
               mensaje = ex.Message;
                lista = new List<REPORTE_COMPRAS>();
            }
            return lista;
        }



        public List<REPORTE_VENTAS> ListarVentas(string finicio, string ffin)
        {
            List<REPORTE_VENTAS> lista = new List<REPORTE_VENTAS>();

            try
            {

                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    SqlCommand cmd = new SqlCommand("ReporteVentas".ToString(), cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("FechaInicio", finicio);
                    cmd.Parameters.AddWithValue("FechaFin", ffin);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new REPORTE_VENTAS()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumneto = dr["TipoDocumento"].ToString(),
                                NumerDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Categoria = dr["CategoriaProducto"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                Subtotal = dr["Subtotal"].ToString(),

                            });


                        }
                    }

                }

            }
            catch (Exception)
            {

                lista = new List<REPORTE_VENTAS>();
            }
            return lista;
        }
    }
}
