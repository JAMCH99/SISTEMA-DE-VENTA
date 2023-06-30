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
    public class CD_VENTA
    {
        public int ObtenerCorrelativo()
        {
            int idcorrelativo = 0;
            using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
            {
                try
                {
                    string query = "select count(*)+1 from VENTA ";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();

                    idcorrelativo = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch
                {
                    idcorrelativo = 0;

                }
            }
            return idcorrelativo;
        }



        public bool Registrar(VENTA obj, DataTable detalleventa, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;

            using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("RegistrarVenta", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.Usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("@DocumentoCliente", obj.DocumentoCliente);
                    cmd.Parameters.AddWithValue("@NombreCliente", obj.NombreCliente);
                    cmd.Parameters.AddWithValue("@MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("@MontoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("@MontoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("@DetalleVenta", detalleventa);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cn.Open();
                    cmd.ExecuteNonQuery();

                    Resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].ToString();



                }
                catch (Exception ex)
                {

                    Resultado = false;
                    Mensaje = ex.Message;
                }
            }
            return Resultado;
        }

        public bool Restar_Stock(int idproducto,int cantidad)
        {
            bool resulatdo = true;
            using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
            {
                try
                {
                    string query = "update PRODUCTO set Stock= Stock - @Cantidad where IdProducto=@IdProducto";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Cantidad",cantidad);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cn.Open();

                    resulatdo = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch 
                {
                    resulatdo = false;
                }
            }
                return resulatdo;
        }
        public bool Sumar_Stock(int idproducto, int cantidad)
        {
            bool resulatdo = true;
            using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
            {
                try
                {
                    string query = "update PRODUCTO set Stock= Stock + @Cantidad where IdProducto=@IdProducto";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cn.Open();

                    resulatdo = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch
                {
                    resulatdo = false;
                }
            }
            return resulatdo;
        }

        public VENTA ObtenerVenta(string numerodocumento)
        {
                VENTA obj = null;
            try
            {

                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("  select v.IdVenta,v.NombreCliente,u.Nombrecompleto,v.DocumentoCliente,v.TipoDocumento,v.NumeroDocumento,");
                    query.AppendLine("v.MontoCambio,v.MontoPago,v.MontoTotal,CONVERT(char(10), v.FechaCreacion, 103)[FechaRegistro] from VENTA v inner join USUARIO u on u.IdUsuario = v.IdUsuario");
                    query.AppendLine("   where v.NumeroDocumento = @numero");

                    SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                    cmd.Parameters.AddWithValue("@numero", numerodocumento);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            obj = new VENTA()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"].ToString()),
                                Usuario = new USUARIO() { NombreCompleto = dr["NombreCompleto"].ToString() },
                                DocumentoCliente = dr["DocumentoCliente"].ToString(), 
                                NombreCliente= dr["NombreCliente"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"].ToString()),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };

                        }
                    }

                }

            }
            catch
            {
                obj = new VENTA();
            }

            return obj;
        }

        public List<DETALLE_VENTA> ObtenerDetalleVenta(int IdVenta)
        {
            List<DETALLE_VENTA> lista = new List<DETALLE_VENTA>();

            try
            {

                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine(" select p.Nombre,dv.PrecioVenta,dv.Cantidad,dv.Subtotal from DETALLE_VENTA dv  ");
                    query.AppendLine(" inner join PRODUCTO p on p.IdProducto=dv.IdProducto where dv.IdVenta=@IdVenta");

                    SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                    cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DETALLE_VENTA()
                            {
                                Producto = new PRODUCTO() { Nombre = dr["Nombre"].ToString() },
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                Subtotal = Convert.ToDecimal(dr["Subtotal"].ToString()),
                            });


                        }
                    }

                }

            }
            catch (Exception)
            {
                lista = new List<DETALLE_VENTA>();
            }

            return lista;
        }

        public List<VENTA> listaNumero_VENTA()
        {
            List<VENTA> lista = new List<VENTA>();

            using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
            {
                try
                {
                    string query = "select IdVenta,NumeroDocumento from VENTA";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new VENTA()
                            {

                                IdVenta = Convert.ToInt32(dr["IdVenta"].ToString()),
                                NumeroDocumento = dr["NumeroDocumento"].ToString()

                            });
                        }
                    }
                }
                catch
                {
                    lista = new List<VENTA>();
                }
            }

            return lista;
        }
    }
}
